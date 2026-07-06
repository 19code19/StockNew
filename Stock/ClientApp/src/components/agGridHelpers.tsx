import { type ColDef, type GridOptions, type ICellRendererParams } from '@ag-grid-community/core';
import { useCallback, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

export const createSlug = (company: string) =>
  company
    .trim()
    .replace(/[^a-zA-Z0-9]+/g, '-')
    .replace(/^-+|-+$/g, '')
    .toLowerCase();

export const renderExternalViewLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as { companyName?: string })?.companyName ?? '';
  const slug = createSlug(company);
  return (
    <Link
      className="text-sky-300 hover:text-sky-100 underline"
      to={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
      target="_blank"
      rel="noreferrer"
    >
      NSE
    </Link>
  );
};

export const renderDetailsLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as { companyName?: string })?.companyName ?? '';
  const slug = createSlug(company);
  return (
    <Link className="text-sky-300 underline hover:text-sky-100" to={`/details/${symbol}/${slug}`}>
      Details
    </Link>
  );
};

export const defaultGridOptions: GridOptions = {
  defaultColDef: {
    sortable: true,
    resizable: true,
    filter: true,
    floatingFilter: true,
    minWidth: 100,
  },
  animateRows: true,
  rowHeight: 42,
  pagination: true,
  paginationPageSize: 20,
  loading: false,
  rowSelection: 'single',
  suppressRowClickSelection: false,
  rowClassRules: {
    'ag-row-selected-highlight': (params) => Boolean(params.node.isSelected()),
  },
  overlayLoadingTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="mb-3 h-10 w-10 rounded-full border-4 border-slate-700 border-t-sky-300 animate-spin"></div><div class="text-sm font-medium">Loading data...</div></div></div>',
  overlayNoRowsTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="text-sm font-medium">No records found</div></div></div>',
};

type CommonSymbolColumnOptions = {
  field?: string;
  headerName?: string;
  pinned?: 'left' | 'right';
  includeFavorite?: boolean;
  renderFavorite?: (params: ICellRendererParams) => JSX.Element | null;
  showNse?: boolean;
  showDetails?: boolean;
};

type FavoriteSymbolRow = { symbol?: string | null };

const normalizeSymbol = (symbol?: string | null) => symbol?.trim().toUpperCase() ?? '';

export const useFavoriteGridState = () => {
  const [favoriteSymbols, setFavoriteSymbols] = useState<Set<string>>(new Set());

  const fetchFavorites = useCallback(async () => {
    try {
      const response = await fetch('/api/favorites');
      if (!response.ok) {
        setFavoriteSymbols(new Set());
        return;
      }

      const favorites = (await response.json()) as FavoriteSymbolRow[];
      setFavoriteSymbols(new Set(favorites.map((fav) => normalizeSymbol(fav.symbol)).filter(Boolean)));
    } catch {
      setFavoriteSymbols(new Set());
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const toggleFavorite = useCallback(
    async (symbol: string, companyName: string, isFavorite: boolean) => {
      const url = `/api/favorites?symbol=${encodeURIComponent(symbol)}${
        isFavorite ? '' : `&companyName=${encodeURIComponent(companyName)}`
      }`;
      const method = isFavorite ? 'DELETE' : 'POST';

      try {
        const response = await fetch(url, { method });
        if (!response.ok) {
          return;
        }

        await fetchFavorites();
      } catch {
        // Swallow errors here; the parent grid can decide how to surface them if needed.
      }
    },
    [fetchFavorites],
  );

  const renderFavoriteButton = useCallback(
    (params: ICellRendererParams): JSX.Element | null => {
      const symbol = params.data?.symbol as string | undefined;
      const companyName = params.data?.companyName as string | undefined;
      if (!symbol) return null;

      const normalizedSymbol = normalizeSymbol(symbol);
      const isFavorite = favoriteSymbols.has(normalizedSymbol);

      return (
        <button
          type="button"
          onClick={() => void toggleFavorite(symbol, companyName ?? '', isFavorite)}
          className={`rounded-full px-3 py-1 text-xs font-semibold transition ${
            isFavorite ? 'bg-amber-500 text-slate-950 hover:bg-amber-400' : 'bg-slate-700 text-slate-100 hover:bg-slate-600'
          }`}
        >
          {isFavorite ? '★' : '☆'}
        </button>
      );
    },
    [favoriteSymbols, toggleFavorite],
  );

  return { favoriteSymbols, renderFavoriteButton };
};

export const buildCommonSymbolColumns = ({
  field = 'symbol',
  headerName = 'Symbol',
  pinned = 'left',
  includeFavorite = false,
  renderFavorite,
  showNse = true,
  showDetails = true,
}: CommonSymbolColumnOptions = {}): ColDef[] => [
  {
    colId: 'symbol',
    field,
    headerName,
    // Give the column a bit more breathing room when a favorite icon is
    // also rendered, since it now competes with the symbol text for space.
    minWidth: 200,
    maxWidth: 250,
    width:  100,
    pinned,
    sortable: true,
    filter: true,
    flex: 1,
    cellRenderer: (params: ICellRendererParams) => {
      const symbol = (params.value as string | undefined) ?? '';
      const company = (params.data as { companyName?: string })?.companyName ?? '';
      const slug = createSlug(company);

      return (
        // gap-6 replaces the old single-sided `ml-4` so there's guaranteed
        // breathing room between the NSE/View links and the right-hand block,
        // regardless of how much space the right block ends up using.
        <div className="flex min-w-0 items-center gap-4 py-1 text-[11px]">
          <div className="flex shrink-0 items-center gap-3">
            {showNse && symbol ? (
              <Link
                className="shrink-0 text-sky-300 hover:text-sky-100 underline"
                to={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
                target="_blank"
                rel="noreferrer"
              >
                NSE
              </Link>
            ) : null}
            {includeFavorite && renderFavorite ? (
              <span className="shrink-0">{renderFavorite(params)}</span>
            ) : null}
          </div>
          <div className="flex min-w-0 flex-1 items-center pr-1">
            {showDetails && symbol ? (
              <Link className="min-w-0 truncate font-semibold text-sky-300 underline transition-colors hover:text-sky-100" to={`/details/${symbol}/${slug}`}>
                {symbol}
              </Link>
            ) : (
              <span className="min-w-0 truncate font-semibold text-slate-100">{symbol}</span>
            )}
          </div>
        </div>
      );
    },
  },
];