import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent, type ICellRendererParams } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { YearwiseStockSummary } from '../models/YearwiseStockSummary';
import { buildCommonSymbolColumns, defaultGridOptions } from './agGridHelpers';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

type YearwiseSummaryGridProps = {
  data: YearwiseStockSummary[];
  loading?: boolean;
};

const normalizeSymbol = (symbol?: string | null) => symbol?.trim().toUpperCase() ?? '';

const formatColumnHeader = (field: string) =>
  field
    .replace(/([a-z0-9])([A-Z])/g, '$1 $2')
    .replace(/_/g, ' ')
    .replace(/^./, (char) => char.toUpperCase());

const YearwiseSummaryGrid = ({ data, loading = false }: YearwiseSummaryGridProps) => {
  const [favoriteSymbols, setFavoriteSymbols] = useState<Set<string>>(new Set());
  const [gridApi, setGridApi] = useState<GridApi | null>(null);

  const fetchFavorites = useCallback(async () => {
    try {
      const response = await fetch('/api/nse/favorites');
      if (!response.ok) {
        return;
      }
      const favorites = (await response.json()) as { symbol?: string | null }[];
      const normalized = new Set(favorites.map((fav) => normalizeSymbol(fav.symbol)) .filter(Boolean));
      setFavoriteSymbols(normalized);
    } catch {
      setFavoriteSymbols(new Set());
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  useEffect(() => {
    if (!gridApi) {
      return;
    }

    gridApi.refreshCells({ force: true, columns: ['symbol'] });
  }, [favoriteSymbols, gridApi]);

  const onGridReady = useCallback((params: GridReadyEvent) => {
    setGridApi(params.api);
    params.api.refreshCells({ force: true, columns: ['symbol'] });
  }, []);

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

      const normalizedSymbol = normalizeSymbol(symbol);
      const isFavorite = favoriteSymbols.has(normalizedSymbol);
      return (
        <button
          type="button"
          onClick={() => void toggleFavorite(symbol, companyName, isFavorite)}
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

  const dynamicColumns = useMemo<ColDef[]>(() => {
    const usedFields = new Set<string>([
      'symbol',
      'companyName',
      'sector',
      'basicIndustry',
      'industryInfo',
      'issueDesc',
      'macro',
      'tradingSegment',
      'nameOfComplianceOfficer',
      'indexListJson',
      'indexName',
      'totalTradedVolume',
      'totalTradedValue',
      'quantityTraded',
      'deliveryQuantity',
      'deliveryToTradedQuantity',
      'totalMarketCap',
      'yearHigh',
      'yearLow',
      'yesterdayChangePercent',
      'oneWeekChangePercent',
      'oneMonthChangePercent',
      'threeMonthChangePercent',
      'sixMonthChangePercent',
      'oneYearChangePercent',
      'applicableMargin',
      'varMargin',
      'adhocMargin',
    ]);

    const rowFields = data?.length ? Object.keys(data[0]) : [];

    return rowFields
      .filter((field) => !usedFields.has(field))
      .map((field) => ({
        field,
        headerName: formatColumnHeader(field),
        minWidth: 140,
        filter: true,
        sortable: true,
        valueFormatter: (params) => {
          const value = params.value;
          if (value == null || value === '') {
            return '';
          }
          if (typeof value === 'number') {
            return Number(value).toLocaleString('en-IN', { maximumFractionDigits: 2 });
          }
          return String(value);
        },
      })) as ColDef[];
  }, [data]);

  const columnDefs = useMemo<ColDef[]>(
    () => [
      ...buildCommonSymbolColumns({ includeFavorite: true, renderFavorite: renderFavoriteButton }),
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
      { field: 'industryInfo', headerName: 'Industry Info', minWidth: 220, filter: true },
      { field: 'issueDesc', headerName: 'Issue Desc', minWidth: 220, filter: true },
      { field: 'macro', headerName: 'Macro', minWidth: 220, filter: true },
      { field: 'tradingSegment', headerName: 'Trading Segment', minWidth: 180, filter: true },
      { field: 'nameOfComplianceOfficer', headerName: 'Compliance Officer', minWidth: 220, filter: true },
      { field: 'indexListJson', headerName: 'Index List', minWidth: 220, filter: true },
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
        field: 'quantityTraded',
        headerName: 'Quantity Traded',
        minWidth: 140,
        valueFormatter: (params) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      },
      {
        field: 'deliveryQuantity',
        headerName: 'Delivery Qty',
        minWidth: 140,
        valueFormatter: (params) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      },
      {
        field: 'deliveryToTradedQuantity',
        headerName: 'Delivery %',
        minWidth: 140,
        valueFormatter: (params) => `${Number(params.value ?? 0).toFixed(2)}%`,
      },
      {
        field: 'totalMarketCap',
        headerName: 'Market Cap',
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
      {
        field: 'oneWeekChangePercent',
        headerName: '1W %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
      {
        field: 'oneMonthChangePercent',
        headerName: '1M %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
      {
        field: 'threeMonthChangePercent',
        headerName: '3M %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
      {
        field: 'sixMonthChangePercent',
        headerName: '6M %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
      {
        field: 'oneYearChangePercent',
        headerName: '1Y %',
        minWidth: 110,
        valueFormatter: (params) => `${Number(params.value).toFixed(2)}%`,
      },
      {
        field: 'applicableMargin',
        headerName: 'Applicable Margin',
        minWidth: 140,
        valueFormatter: (params) => String(params.value ?? ''),
      },
      {
        field: 'varMargin',
        headerName: 'Var Margin',
        minWidth: 120,
        valueFormatter: (params) => String(params.value ?? ''),
      },
      {
        field: 'adhocMargin',
        headerName: 'Adhoc Margin',
        minWidth: 120,
        valueFormatter: (params) => String(params.value ?? ''),
      },
      ...dynamicColumns,
    ],
    [dynamicColumns, renderFavoriteButton],
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
        loading={loading}
        onGridReady={onGridReady}
        pagination={true}
        paginationPageSize={100}
        paginationPageSizeSelector={[50, 100, 200, 500, 1000, 2000, 5000,10000]}
      />
    </div>
  );
};

export default YearwiseSummaryGrid;
