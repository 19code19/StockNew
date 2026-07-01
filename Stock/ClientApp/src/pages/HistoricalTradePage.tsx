import { useSummaryData } from '../context/SummaryDataContext';
import HistoricalTradePanel from '../components/HistoricalTradePanel';

const HistoricalTradePage = () => {
  const { rows, loading, error } = useSummaryData();

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="rounded-2xl border border-slate-800 bg-slate-900/80 p-4 shadow-2xl">
        <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Historical trade</p>
        <h2 className="mt-2 text-xl font-semibold text-white">Shared grid data is available across tabs</h2>
        <p className="mt-3 text-sm leading-6 text-slate-300">
          The yearwise summary rows are fetched once and shared across pages. This page focuses on historical range loading while keeping the same summary data accessible.
        </p>
        <div className="mt-4 flex flex-wrap items-center gap-3 text-sm text-slate-300">
          <span>Summary rows loaded: {rows.length}</span>
          {loading && <span className="rounded-full bg-slate-800 px-3 py-1">Loading summary</span>}
          {error && <span className="rounded-full bg-rose-900 px-3 py-1 text-rose-300">Summary error</span>}
        </div>
      </section>

      <section className="flex-1 rounded-2xl border border-slate-800 bg-slate-900/80 p-4 shadow-2xl">
        <HistoricalTradePanel rowsCount={rows.length} />
      </section>
    </div>
  );
};

export default HistoricalTradePage;
