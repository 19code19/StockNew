import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridOptions, type ICellRendererParams } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { YearwiseStockSummary } from '../models/YearwiseStockSummary';
import { buildCommonSymbolColumns, defaultGridOptions } from './agGridHelpers';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

type YearwiseSummaryGridProps = {
  data: YearwiseStockSummary[];
};

const YearwiseSummaryGrid = ({ data }: YearwiseSummaryGridProps) => {
  const [favoriteSymbols, setFavoriteSymbols] = useState<Set<string>>(new Set());

  const fetchFavorites = useCallback(async () => {
    try {
      const response = await fetch('/api/nse/favorites');
      if (!response.ok) {
        return;
      }
      const favorites = (await response.json()) as { symbol: string }[];
      setFavoriteSymbols(new Set(favorites.map((fav) => fav.symbol)));
    } catch {
      // ignore favorites load errors
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const toggleFavorite = useCallback(
    async (symbol: string, companyName: string, isFavorite: boolean) => {
      const url = `/api/nse/favorites?symbol=${encodeURIComponent(symbol)}${
        isFavorite ? '' : `&companyName=${encodeURIComponent(companyName)}`
      }`;
      const method = isFavorite ? 'DELETE' : 'POST';

      const response = await fetch(url, { method });
      if (response.ok) {
        await fetchFavorites();
      }
    },
    [fetchFavorites],
  );

  const renderFavoriteButton = useCallback(
    (params: ICellRendererParams): JSX.Element | null => {
      const symbol = params.data?.symbol as string;
      const companyName = params.data?.companyName as string;
      if (!symbol) return null;

      const isFavorite = favoriteSymbols.has(symbol);
      return (
        <button
          type="button"
          onClick={() => void toggleFavorite(symbol, companyName, isFavorite)}
          className={`rounded-full px-3 py-1 text-xs font-semibold transition ${
            isFavorite ? 'bg-rose-600 text-white hover:bg-rose-500' : 'bg-slate-700 text-slate-100 hover:bg-slate-600'
          }`}
        >
          {isFavorite ? '★' : '☆'}
        </button>
      );
    },
    [favoriteSymbols, toggleFavorite],
  );

  const columnDefs = useMemo<ColDef[]>(
    () => [
      ...buildCommonSymbolColumns(),
      {
        colId: 'favorite',
        field: 'symbol',
        headerName: 'Favorite',
        width: 100,
        cellRenderer: renderFavoriteButton,
        sortable: false,
        filter: false,
        pinned: 'left',
      },
      { field: 'symbol', headerName: 'SYMBOL', minWidth: 80, filter: true, pinned: 'left' },
      { field: 'companyName', headerName: 'Company', minWidth: 220, filter: true },
      {
        field: 'sector',
        headerName: 'Sector',
        minWidth: 180,
        filter: 'agSetColumnFilter',
        filterParams: {
          applyMiniFilterWhileTyping: true,
          suppressSelectAll: false,
        },
      },
      { field: 'basicIndustry', headerName: 'Industry', minWidth: 180, filter: true },
      {
        field: 'indexName',
        headerName: 'Index',
        minWidth: 140,
        filter: 'agSetColumnFilter',
        filterParams: {
          applyMiniFilterWhileTyping: true,
          suppressSelectAll: false,
        },
      },
      {
        field: 'totalTradedVolume',
        headerName: 'Volume',
        minWidth: 140,
        valueFormatter: (params) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      },
      {
        field: 'totalTradedValue',
        headerName: 'Turnover',
        minWidth: 140,
        valueFormatter: (params) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      },
      {
        field: 'yearHigh',
        headerName: 'Year High',
        minWidth: 120,
        valueFormatter: (params) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      },
      {
        field: 'yearLow',
        headerName: 'Year Low',
        minWidth: 120,
        valueFormatter: (params) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      },
      {
        field: 'yesterdayChangePercent',
        headerName: '1D %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
    ],
    [],
  );

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
  }), []);

  return (
    <div className="ag-theme-alpine h-full w-full bg-slate-950">
      <AgGridReact<YearwiseStockSummary>
        rowData={data}
        columnDefs={columnDefs}
        gridOptions={gridOptions}
      />
    </div>
  );
};

export default YearwiseSummaryGrid;
