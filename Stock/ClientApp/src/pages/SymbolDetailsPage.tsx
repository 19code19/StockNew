import { useMemo } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useSummaryData } from '../context/SummaryDataContext';

const SymbolDetailsPage = () => {
  const navigate = useNavigate();
  const { symbol, companySlug } = useParams();
  const { rows, loading } = useSummaryData();

  const row = useMemo(
    () => rows.find((item) => item.symbol.toUpperCase() === symbol?.toUpperCase()),
    [rows, symbol],
  );

  const companyName = row?.companyName ?? companySlug?.replace(/-/g, ' ') ?? 'Unknown Company';
  const displaySymbol = symbol?.toUpperCase() ?? 'UNKNOWN';

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="rounded-2xl border border-slate-800 bg-slate-900/80 p-6 shadow-2xl">
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Symbol details</p>
            <h2 className="mt-2 text-2xl font-semibold text-white">{displaySymbol}</h2>
            <p className="mt-1 text-sm text-slate-300">{companyName}</p>
          </div>

          <div className="flex flex-wrap items-center gap-3">
            <button
              type="button"
              onClick={() => navigate(-1)}
              className="rounded-2xl border border-slate-700 bg-slate-950 px-4 py-2 text-sm font-semibold text-slate-100 transition hover:bg-slate-800"
            >
              Back
            </button>
            <a
              href={`https://www.nseindia.com/get-quotes/equity?symbol=${displaySymbol}`}
              target="_blank"
              rel="noreferrer"
              className="rounded-2xl bg-sky-500 px-4 py-2 text-sm font-semibold text-slate-950 transition hover:bg-sky-400"
            >
              Open NSE quote
            </a>
          </div>
        </div>

        <div className="mt-6 grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Sector</p>
            <p className="mt-2 text-lg font-semibold text-white">{row?.sector ?? 'N/A'}</p>
          </div>
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Industry</p>
            <p className="mt-2 text-lg font-semibold text-white">{row?.basicIndustry ?? 'N/A'}</p>
          </div>
          <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
            <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Index</p>
            <p className="mt-2 text-lg font-semibold text-white">{row?.indexName ?? 'N/A'}</p>
          </div>
        </div>
      </section>

      <section className="rounded-2xl border border-slate-800 bg-slate-900/80 p-6 shadow-2xl">
        <h3 className="text-lg font-semibold text-white">Key metrics</h3>
        {loading ? (
          <div className="mt-4 text-slate-300">Loading summary data…</div>
        ) : row ? (
          <div className="mt-4 grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
            <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
              <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Market Cap</p>
              <p className="mt-2 text-lg font-semibold text-white">{row.totalMarketCap?.toLocaleString('en-IN') ?? 'N/A'}</p>
            </div>
            <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
              <p className="text-xs uppercase tracking-[0.2em] text-slate-500">1Y Change</p>
              <p className="mt-2 text-lg font-semibold text-white">{row.oneYearChangePercent?.toFixed(2)}%</p>
            </div>
            <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
              <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Year High</p>
              <p className="mt-2 text-lg font-semibold text-white">₹{row.yearHigh?.toLocaleString('en-IN')}</p>
            </div>
            <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
              <p className="text-xs uppercase tracking-[0.2em] text-slate-500">Year Low</p>
              <p className="mt-2 text-lg font-semibold text-white">₹{row.yearLow?.toLocaleString('en-IN')}</p>
            </div>
          </div>
        ) : (
          <div className="mt-4 text-slate-300">Summary data not available for this symbol.</div>
        )}
      </section>
    </div>
  );
};

export default SymbolDetailsPage;
