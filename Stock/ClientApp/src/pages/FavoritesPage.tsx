import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent, type ICellRendererParams } from '@ag-grid-community/core';
import { buildCommonSymbolColumns, createSlug } from '../grid/commonSymbolColumns';
import { createFavoritesColumns } from '../grid/columnConfigs';
import { defaultGridOptions } from '../grid/defaultGridOptions';
import { apiUrl } from '../api/api';

ModuleRegistry.registerModules([ClientSideRowModelModule]);

type FavoriteSymbol = {
  id: string;
  symbol: string;
  companyName: string;
  assetType: string;
  addedAt: string;
};

const FavoritesPage = () => {
  const [favorites, setFavorites] = useState<FavoriteSymbol[]>([]);
  const [assetType, setAssetType] = useState<'all' | 'stock' | 'mutualfund'>('all');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [gridApi, setGridApi] = useState<GridApi | null>(null);

  const fetchFavorites = useCallback(async () => {
    setLoading(true);
    setError('');

    try {
      const query = assetType === 'all' ? '' : `?assetType=${encodeURIComponent(assetType)}`;
      const response = await fetch(apiUrl(`/api/favorites${query}`));
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
  }, [assetType]);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const removeFavorite = useCallback(async (symbol: string, assetType: string = 'stock') => {
    try {
      const response = await fetch(
        `/api/favorites?symbol=${encodeURIComponent(symbol)}&assetType=${encodeURIComponent(assetType)}`,
        {
          method: 'DELETE',
        },
      );
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
    const assetType = (params.data as FavoriteSymbol)?.assetType ?? 'stock';
    if (!symbol) return null;

    return (
      <button
        type="button"
        onClick={() => void removeFavorite(symbol, assetType)}
        className="rounded-full bg-amber-500 px-3 py-1 text-xs font-semibold text-slate-950 transition hover:bg-amber-400"
        aria-label="Remove favorite"
      >
        ★
      </button>
    );
  };

  const columnDefs = useMemo<ColDef[]>(
    () => {
      const symbolColumns = buildCommonSymbolColumns({
        includeFavorite: true,
        renderFavorite: renderFavoriteButton,
        showNse: false,
        externalLinkUrl: (symbol, company, params) => {
          const assetType = (params.data as FavoriteSymbol)?.assetType ?? 'stock';
          if (assetType.toLowerCase() === 'mutualfund' || assetType.toLowerCase() === 'mutual-fund') {
            return `https://groww.in/mutual-funds/${symbol}`;
          }

          const slug = createSlug(company ?? symbol);
          return `https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`;
        },
      });

      if (symbolColumns.length > 0) {
        symbolColumns[0] = {
          ...symbolColumns[0],
          minWidth: 240,
          flex: 2,
        };
      }

      return [
        ...symbolColumns,
        ...createFavoritesColumns(),
      ];
    },
    [renderFavoriteButton],
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

      <section className="flex items-center gap-4 pb-2">
        <label className="text-sm font-semibold text-slate-200">Filter:</label>
        <select
          value={assetType}
          onChange={(event) => setAssetType(event.target.value as 'all' | 'stock' | 'mutualfund')}
          className="rounded-md border border-slate-700 bg-slate-950 px-3 py-2 text-sm text-slate-100"
        >
          <option value="all">All</option>
          <option value="stock">Stock</option>
          <option value="mutualfund">Mutual Fund</option>
        </select>
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
            loading={loading}
            pagination={true}
            paginationPageSize={20}
          />
        </div>
      </section>
    </div>
  );
};

export default FavoritesPage;
