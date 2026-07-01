import { useCallback, useEffect, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent, type ICellRendererParams } from '@ag-grid-community/core';
import { createSlug, defaultGridOptions } from '../components/agGridHelpers';

ModuleRegistry.registerModules([ClientSideRowModelModule]);

type FavoriteSymbol = {
  id: string;
  symbol: string;
  companyName: string;
  addedAt: string;
};

const FavoritesPage = () => {
  const [favorites, setFavorites] = useState<FavoriteSymbol[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [gridApi, setGridApi] = useState<GridApi | null>(null);

  const fetchFavorites = useCallback(async () => {
    setLoading(true);
    setError('');

    try {
      const response = await fetch('/api/nse/favorites');
      if (!response.ok) {
        throw new Error('Unable to load favorites');
      }

      const data = (await response.json()) as FavoriteSymbol[];
      setFavorites(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load favorites');
      setFavorites([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const removeFavorite = useCallback(async (symbol: string) => {
    try {
      const response = await fetch(`/api/nse/favorites?symbol=${encodeURIComponent(symbol)}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Unable to remove favorite');
      }
      await fetchFavorites();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to remove favorite');
    }
  }, [fetchFavorites]);

  const renderDetailsLink = (params: ICellRendererParams): JSX.Element | null => {
    const symbol = params.value as string;
    if (!symbol) return null;
    const company = (params.data as FavoriteSymbol)?.companyName ?? '';
    const slug = company
      .trim()
      .replace(/[^a-zA-Z0-9]+/g, '-')
      .replace(/^-+|-+$/g, '')
      .toLowerCase();

    return (
      <Link className="text-sky-300 underline hover:text-sky-100" to={`/details/${symbol}/${slug}`}>
        Details
      </Link>
    );
  };

  const renderViewLink = (params: ICellRendererParams): JSX.Element | null => {
    const symbol = params.value as string;
    if (!symbol) return null;

    const company = (params.data as FavoriteSymbol)?.companyName ?? '';
    const slug = createSlug(company);

    return (
      <a
        className="text-sky-300 underline hover:text-sky-100"
        href={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
        target="_blank"
        rel="noreferrer"
      >
        View
      </a>
    );
  };

  const renderFavoriteButton = (params: ICellRendererParams): JSX.Element | null => {
    const symbol = params.data?.symbol as string;
    if (!symbol) return null;

    return (
      <button
        type="button"
        onClick={() => void removeFavorite(symbol)}
        className="rounded-full bg-amber-500 px-3 py-1 text-xs font-semibold text-slate-950 transition hover:bg-amber-400"
        aria-label="Remove favorite"
      >
        ★
      </button>
    );
  };

  const columnDefs = useMemo<ColDef[]>(
    () => [
      {
        field: 'symbol',
        headerName: 'Symbol',
        minWidth: 120,
        pinned: 'left',
      },
      {
        field: 'companyName',
        headerName: 'Company',
        minWidth: 260,
      },
      {
        field: 'addedAt',
        headerName: 'Added',
        minWidth: 180,
        valueFormatter: (params) => new Date(params.value as string).toLocaleString(),
      },
      {
        field: 'symbol',
        colId: 'view',
        headerName: 'View',
        width: 90,
        cellRenderer: renderViewLink,
        sortable: false,
        filter: false,
      },
      {
        field: 'symbol',
        colId: 'details',
        headerName: 'Details',
        width: 100,
        cellRenderer: renderDetailsLink,
        sortable: false,
        filter: false,
      },
      {
        field: 'symbol',
        colId: 'favorite',
        headerName: 'Favorite',
        width: 90,
        cellRenderer: renderFavoriteButton,
        sortable: false,
        filter: false,
      },
    ],
    [removeFavorite],
  );

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
  }), []);

  const onGridReady = useCallback((params: GridReadyEvent) => {
    setGridApi(params.api);
  }, []);

  useEffect(() => {
    if (!gridApi) {
      return;
    }

    if (loading) {
      gridApi.showLoadingOverlay();
    } else if (favorites.length === 0) {
      gridApi.showNoRowsOverlay();
    } else {
      gridApi.hideOverlay();
    }
  }, [favorites.length, gridApi, loading]);

  return (
    <div className="flex h-full w-full flex-col gap-4">
      <section className="rounded-2xl border border-slate-800 bg-slate-900/80 p-6 shadow-2xl">
        <div className="flex items-center justify-between gap-4">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Favorites</p>
            <h2 className="mt-2 text-2xl font-semibold text-white">Favorites list</h2>
          </div>
          <div className="text-slate-300">{loading ? 'Loading favorites...' : `${favorites.length} favorite${favorites.length === 1 ? '' : 's'}`}</div>
        </div>
        {error ? <div className="mt-4 rounded-2xl border border-rose-700 bg-rose-950/20 p-4 text-rose-200">{error}</div> : null}
      </section>

      <section className="flex-1 rounded-2xl border border-slate-800 bg-slate-950/80 p-4 shadow-2xl">
        <div className="ag-theme-alpine h-full w-full">
          <AgGridReact<FavoriteSymbol>
            rowData={favorites}
            columnDefs={columnDefs}
            gridOptions={gridOptions}
            onGridReady={onGridReady}
            defaultColDef={gridOptions.defaultColDef}
            animateRows={true}
            pagination={true}
            paginationPageSize={20}
          />
        </div>
      </section>
    </div>
  );
};

export default FavoritesPage;
