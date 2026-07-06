import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import { YearwiseStockSummary } from '../models/YearwiseStockSummary';

type SummaryDataContextValue = {
  rows: YearwiseStockSummary[];
  loading: boolean;
  error: string;
  refresh: () => Promise<void>;
};

const SummaryDataContext = createContext<SummaryDataContextValue | undefined>(undefined);

async function fetchSummaryRows(): Promise<YearwiseStockSummary[]> {
  const response = await fetch('/api/nse/yearwise-summary');
  if (!response.ok) {
    throw new Error('Failed to load summary data');
  }

  return (await response.json()) as YearwiseStockSummary[];
}

export const SummaryDataProvider = ({ children }: { children: ReactNode }) => {
  const [rows, setRows] = useState<YearwiseStockSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const refresh = async () => {
    setLoading(true);
    setError('');

    try {
      const data = await fetchSummaryRows();
      setRows(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load summary data');
      setRows([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void refresh();
  }, []);

  return (
    <SummaryDataContext.Provider value={{ rows, loading, error, refresh }}>
      {children}
    </SummaryDataContext.Provider>
  );
};

export const useSummaryData = (): SummaryDataContextValue => {
  const context = useContext(SummaryDataContext);
  if (!context) {
    throw new Error('useSummaryData must be used within SummaryDataProvider');
  }

  return context;
};
