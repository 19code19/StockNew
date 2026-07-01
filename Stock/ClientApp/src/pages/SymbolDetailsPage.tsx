import { useParams } from 'react-router-dom';
import HistoricalTradePanel from '../components/HistoricalTradePanel';

const SymbolDetailsPage = () => {
  const { symbol } = useParams();
  const displaySymbol = symbol?.toUpperCase() ?? 'UNKNOWN';

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="rounded-2xl border border-slate-800 bg-slate-900/80 p-6 shadow-2xl">
        <h2 className="text-xl font-semibold text-white">Historical trades for {displaySymbol}</h2>
        <div className="mt-4">
          <HistoricalTradePanel symbol={displaySymbol} />
        </div>
      </section>
    </div>
  );
};

export default SymbolDetailsPage;
