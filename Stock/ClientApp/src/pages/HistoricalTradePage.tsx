import { useSummaryData } from '../context/SummaryDataContext';
import HistoricalTradePanel from '../components/HistoricalTradePanel';

const HistoricalTradePage = () => {
  const { rows } = useSummaryData();

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="flex-1 rounded-2xl border border-slate-800 bg-slate-900/80 p-4 shadow-2xl">
        <HistoricalTradePanel rowsCount={rows.length} />
      </section>
    </div>
  );
};

export default HistoricalTradePage;
