import YearwiseSummaryGrid from '../components/YearwiseSummaryGrid';
import { useSummaryData } from '../context/SummaryDataContext';

const YearwiseStockSummaryPage = () => {
  const { rows, loading } = useSummaryData();

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="flex-1 rounded-2xl border border-slate-800 bg-slate-900/80 p-2 shadow-2xl">
        <YearwiseSummaryGrid data={rows} loading={loading} />
      </section>
    </div>
  );
};

export default YearwiseStockSummaryPage;
