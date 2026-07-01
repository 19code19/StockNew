import { useEffect, useState } from 'react';

type HistoricalTradeRow = {
  chSymbol: string;
  mTimestamp: string;
  chLastTradedPrice: number;
  chTotTradedQty: number;
  chTotTradedVal: number;
};

type HistoricalTradePanelProps = {
  rowsCount: number;
};

const formatDateInput = (date: Date) => date.toISOString().slice(0, 10);

const HistoricalTradePanel = ({ rowsCount }: HistoricalTradePanelProps) => {
  const [fromDate, setFromDate] = useState(() => {
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    return formatDateInput(date);
  });
  const [toDate, setToDate] = useState(() => formatDateInput(new Date()));
  const [rows, setRows] = useState<HistoricalTradeRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [loaded, setLoaded] = useState(false);

  const fetchHistoricalData = async (forceRefresh = false) => {
    setLoading(true);
    setError('');
    try {
      const queryParams = new URLSearchParams({
        fromDate,
        toDate,
        series: 'EQ',
        forceRefresh: forceRefresh ? 'true' : 'false',
      });
      const response = await fetch(`/api/nse/historical-trade-data?${queryParams}`);
      if (!response.ok) {
        throw new Error('Failed to load historical trade data');
      }

      const data = (await response.json()) as HistoricalTradeRow[];
      setRows(data);
      setLoaded(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load historical trade data');
      setRows([]);
      setLoaded(false);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void fetchHistoricalData(false);
  }, []);

  return (
    <div className="space-y-6">
      <div className="grid gap-4 lg:grid-cols-[1fr_auto]">
        <div className="grid gap-3 sm:grid-cols-2 xl:grid-cols-4">
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Summary rows</p>
            <p className="mt-2 text-xl font-semibold text-white">{rowsCount}</p>
          </div>
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">From</p>
            <p className="mt-2 text-white">{new Date(fromDate).toLocaleDateString()}</p>
          </div>
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">To</p>
            <p className="mt-2 text-white">{new Date(toDate).toLocaleDateString()}</p>
          </div>
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Historical rows</p>
            <p className="mt-2 text-xl font-semibold text-white">{rows.length}</p>
          </div>
        </div>

        <div className="rounded-2xl border border-slate-800 bg-slate-900/80 p-4">
          <p className="text-sm text-slate-300">Summary data is shared across tabs, so this section can be extended with more panels and tabs later.</p>
        </div>
      </div>

      <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-[1.4fr_1fr]">
        <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
          <div className="grid gap-4 sm:grid-cols-2">
            <label className="flex flex-col gap-2 text-slate-300">
              <span className="text-xs uppercase tracking-[0.2em] text-slate-500">From</span>
              <input
                type="date"
                value={fromDate}
                onChange={(event) => setFromDate(event.target.value)}
                className="rounded-xl border border-slate-700 bg-slate-950 px-3 py-2 text-slate-100 outline-none focus:border-sky-500"
              />
            </label>
            <label className="flex flex-col gap-2 text-slate-300">
              <span className="text-xs uppercase tracking-[0.2em] text-slate-500">To</span>
              <input
                type="date"
                value={toDate}
                onChange={(event) => setToDate(event.target.value)}
                className="rounded-xl border border-slate-700 bg-slate-950 px-3 py-2 text-slate-100 outline-none focus:border-sky-500"
              />
            </label>
          </div>

          <div className="mt-4 flex flex-wrap gap-3">
            <button
              onClick={() => void fetchHistoricalData(false)}
              disabled={loading}
              className="rounded-2xl bg-sky-500 px-4 py-2 text-sm font-semibold text-slate-950 transition hover:bg-sky-400 disabled:cursor-not-allowed disabled:opacity-60"
            >
              Load range
            </button>
            <button
              onClick={() => void fetchHistoricalData(true)}
              disabled={loading}
              className="rounded-2xl bg-slate-700 px-4 py-2 text-sm font-semibold text-slate-100 transition hover:bg-slate-600 disabled:cursor-not-allowed disabled:opacity-60"
            >
              Refresh and replace
            </button>
          </div>
        </div>

        <div className="rounded-2xl border border-slate-800 bg-slate-900/80 p-4 text-sm text-slate-300">
          <p className="font-medium text-white">How it works</p>
          <p className="mt-2 leading-6">
            If the requested range is already saved, the server returns cached rows. Use "Refresh and replace" to force a fresh backend fetch and overwrite existing records.
          </p>
        </div>
      </div>

      {loading || error ? (
        <div className="rounded-2xl border border-slate-800 bg-slate-900/80 p-4 text-slate-300">
          {loading ? 'Loading summary data...' : null}
          {error ? <div className="text-rose-300">{error}</div> : null}
        </div>
      ) : null}

      <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4 text-slate-300">
        <p className="text-sm uppercase tracking-[0.2em] text-slate-500">Historical trade details</p>
        {loading ? (
          <div className="mt-4 text-slate-300">Loading...</div>
        ) : error ? (
          <div className="mt-4 text-rose-300">{error}</div>
        ) : loaded ? (
          <div className="mt-4 text-slate-100">Loaded {rows.length} historical rows for the selected range.</div>
        ) : (
          <div className="mt-4 text-slate-400">No historical data loaded yet.</div>
        )}
      </div>
    </div>
  );
};

export default HistoricalTradePanel;
