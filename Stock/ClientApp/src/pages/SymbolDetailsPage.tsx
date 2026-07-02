import { useCallback, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import HistoricalTradePanel from '../components/HistoricalTradePanel';

const formatCompanyName = (companySlug?: string) => {
  if (!companySlug) {
    return '';
  }

  return companySlug
    .split('-')
    .filter(Boolean)
    .map((part) => part.charAt(0).toUpperCase() + part.slice(1))
    .join(' ');
};

const SymbolDetailsPage = () => {
  const { symbol, companySlug } = useParams();
  const displaySymbol = symbol?.toUpperCase() ?? 'UNKNOWN';
  const companyName = formatCompanyName(companySlug);
  const nseQuoteUrl = companySlug ? `https://www.nseindia.com/get-quote/equity/${displaySymbol}/${companySlug}` : undefined;
  const [isFavorite, setIsFavorite] = useState(false);
  const [favoriteLoading, setFavoriteLoading] = useState(true);

  const fetchFavoriteState = useCallback(async () => {
    try {
      const response = await fetch('/api/nse/favorites');
      if (!response.ok) {
        return;
      }

      const favorites = (await response.json()) as { symbol?: string }[];
      setIsFavorite(favorites.some((favorite) => favorite.symbol?.toUpperCase() === displaySymbol));
    } catch {
      setIsFavorite(false);
    } finally {
      setFavoriteLoading(false);
    }
  }, [displaySymbol]);

  useEffect(() => {
    setFavoriteLoading(true);
    void fetchFavoriteState();
  }, [fetchFavoriteState]);

  const toggleFavorite = useCallback(async () => {
    const companyName = formatCompanyName(companySlug);
    const url = `/api/nse/favorites?symbol=${encodeURIComponent(displaySymbol)}${isFavorite ? '' : `&companyName=${encodeURIComponent(companyName)}`}`;
    const method = isFavorite ? 'DELETE' : 'POST';

    setFavoriteLoading(true);

    try {
      const response = await fetch(url, { method });
      if (response.ok) {
        setIsFavorite((current) => !current);
      }
    } finally {
      setFavoriteLoading(false);
    }
  }, [companySlug, displaySymbol, isFavorite]);

  return (
    <div className="flex h-full min-h-0 w-full flex-col gap-4">
      <section className="flex min-h-0 flex-1 flex-col rounded-2xl border border-slate-800 bg-slate-900/80 p-6 shadow-2xl">
        <div className="flex flex-wrap items-center justify-between gap-3">
          <div className="space-y-1">
            <h2 className="text-xl font-semibold text-white">Historical trades for {displaySymbol}</h2>
            {companyName && nseQuoteUrl && (
              <a
                href={nseQuoteUrl}
                target="_blank"
                rel="noreferrer"
                className="inline-flex items-center text-sm font-medium text-sky-300 underline transition hover:text-sky-100"
              >
                {companyName}
              </a>
            )}
          </div>
          <button
            type="button"
            onClick={() => void toggleFavorite()}
            disabled={favoriteLoading}
            aria-pressed={isFavorite}
            className={`inline-flex items-center gap-2 rounded-full px-3 py-2 text-sm font-semibold transition ${
              isFavorite
                ? 'bg-rose-600 text-white hover:bg-rose-500'
                : 'bg-slate-700 text-slate-100 hover:bg-slate-600'
            } ${favoriteLoading ? 'cursor-not-allowed opacity-70' : ''}`}
          >
            <span className="text-base">{isFavorite ? '★' : '☆'}</span>
            <span>{favoriteLoading ? 'Saving...' : isFavorite ? 'Favorite' : 'Add to favorites'}</span>
          </button>
        </div>
        <div className="mt-4 flex-1 min-h-0">
          <HistoricalTradePanel symbol={displaySymbol} />
        </div>
      </section>
    </div>
  );
};

export default SymbolDetailsPage;
