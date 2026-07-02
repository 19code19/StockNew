import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent, type ICellRendererParams } from '@ag-grid-community/core';
import { buildCommonSymbolColumns, defaultGridOptions } from '../components/agGridHelpers';

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
      ...buildCommonSymbolColumns({ includeFavorite: true, renderFavorite: renderFavoriteButton }),
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
      gridApi.setGridOption('loading', true);
    } else if (favorites.length === 0) {
      gridApi.showNoRowsOverlay();
      gridApi.setGridOption('loading', false);
    } else {
      gridApi.hideOverlay();
      gridApi.setGridOption('loading', false);
    }
  }, [favorites.length, gridApi, loading]);

  return (
    <div className="flex h-full w-full flex-col gap-4">
      {error ? <div className="rounded-2xl border border-rose-700 bg-rose-950/20 p-4 text-rose-200">{error}</div> : null}

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
