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

const formatDisplayValue = (value: unknown) => {
  if (value == null || value === '') {
    return '';
  }

  if (typeof value === 'number' && Number.isFinite(value)) {
    return new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(value);
  }

  if (typeof value === 'boolean') {
    return value ? 'true' : 'false';
  }

  return String(value);
};

const getTooltipValue = (params: any) => formatDisplayValue(params.value);

const createCompactColumn = (field: string, headerName: string, options: Partial<ColDef> = {}) => ({
  field,
  headerName,
  wrapHeaderText: true,
  autoHeaderHeight: true,
  tooltipValueGetter: (params: any) => getTooltipValue(params),
  ...options,
});

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
      'macro',
      'tradingSegment',
      'nameOfComplianceOfficer',
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
      'twoYearChangePercent',
      'threeYearChangePercent',
      'fiveYearChangePercent',
      'indexYesterdayChangePercent',
      'indexOneWeekChangePercent',
      'indexOneMonthChangePercent',
      'indexThreeMonthChangePercent',
      'indexSixMonthChangePercent',
      'indexOneYearChangePercent',
      'indexTwoYearChangePercent',
      'indexThreeYearChangePercent',
      'indexFiveYearChangePercent',
      'applicableMargin',
      'varMargin',
      'adhocMargin',
    ]);

    const rowFields = data?.length ? Object.keys(data[0]) : [];

    return rowFields
      .filter((field) => !usedFields.has(field))
      .map((field) => {
        const sampleValue = data?.[0]?.[field as keyof YearwiseStockSummary];
        const numericConfig = typeof sampleValue === 'number'
          ? { minWidth: 72, maxWidth: 96, width: 82 }
          : { minWidth: 110, maxWidth: 180, width: 120 };

        return createCompactColumn(field, formatColumnHeader(field), {
          ...numericConfig,
          filter: true,
          sortable: true,
          valueFormatter: (params: any) => formatDisplayValue(params.value),
        }) as ColDef;
      }) as ColDef[];
  }, [data]);

  const columnDefs = useMemo<ColDef[]>(() => {
    const commonSymbolColumns = buildCommonSymbolColumns({ includeFavorite: true, renderFavorite: renderFavoriteButton }).map((col) => ({
      ...col,
      wrapHeaderText: true,
      autoHeaderHeight: true,
      tooltipValueGetter: (params: any) => getTooltipValue(params),
    }));

    return [
      ...commonSymbolColumns,
      createCompactColumn('companyName', 'Company', { minWidth: 160, maxWidth: 220, filter: true }),
      createCompactColumn('sector', 'Sector', {
        minWidth: 130,
        maxWidth: 180,
        filter: 'agSetColumnFilter',
        filterParams: {
          applyMiniFilterWhileTyping: true,
          suppressSelectAll: false,
        },
      }),
      createCompactColumn('basicIndustry', 'Industry', { minWidth: 130, maxWidth: 180, filter: true }),
      createCompactColumn('industryInfo', 'Industry Info', { minWidth: 150, maxWidth: 220, filter: true }),
      createCompactColumn('macro', 'Macro', { minWidth: 150, maxWidth: 220, filter: true }),
      createCompactColumn('tradingSegment', 'Trading Segment', { minWidth: 130, maxWidth: 180, filter: true }),
      createCompactColumn('indexName', 'Index', {
        minWidth: 110,
        maxWidth: 160,
        filter: 'agSetColumnFilter',
        filterParams: {
          applyMiniFilterWhileTyping: true,
          suppressSelectAll: false,
        },
      }),
      createCompactColumn('totalTradedVolume', 'Volume', {
        minWidth: 78,
        maxWidth: 100,
        width: 84,
        valueFormatter: (params: any) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      }),
      createCompactColumn('totalTradedValue', 'Turnover', {
        minWidth: 84,
        maxWidth: 106,
        width: 90,
        valueFormatter: (params: any) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      }),
      createCompactColumn('quantityTraded', 'Qty Traded', {
        minWidth: 78,
        maxWidth: 100,
        width: 84,
        valueFormatter: (params: any) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      }),
      createCompactColumn('deliveryQuantity', 'Delivery Qty', {
        minWidth: 78,
        maxWidth: 100,
        width: 84,
        valueFormatter: (params: any) => new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number),
      }),
      createCompactColumn('deliveryToTradedQuantity', 'Delivery %', {
        minWidth: 74,
        maxWidth: 90,
        width: 80,
        valueFormatter: (params: any) => `${Number(params.value ?? 0).toFixed(2)}%`,
      }),
      createCompactColumn('totalMarketCap', 'Market Cap', {
        minWidth: 84,
        maxWidth: 106,
        width: 90,
        valueFormatter: (params: any) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      }),
      createCompactColumn('yearHigh', 'Year High', {
        minWidth: 74,
        maxWidth: 92,
        width: 80,
        valueFormatter: (params: any) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      }),
      createCompactColumn('yearLow', 'Year Low', {
        minWidth: 74,
        maxWidth: 92,
        width: 80,
        valueFormatter: (params: any) => `₹${new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 }).format(params.value as number)}`,
      }),
      createCompactColumn('yesterdayChangePercent', 'One Day', {
        minWidth: 78,
        maxWidth: 92,
        width: 84,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('oneWeekChangePercent', 'One Week', {
        minWidth: 84,
        maxWidth: 98,
        width: 90,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('oneMonthChangePercent', 'One Month', {
        minWidth: 90,
        maxWidth: 106,
        width: 96,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('threeMonthChangePercent', 'Three Month', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('sixMonthChangePercent', 'Six Month', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('oneYearChangePercent', 'One Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('twoYearChangePercent', 'Two Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('threeYearChangePercent', 'Three Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('fiveYearChangePercent', 'Five Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexYesterdayChangePercent', 'Index One Day', {
        minWidth: 78,
        maxWidth: 92,
        width: 84,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexOneWeekChangePercent', 'Index One Week', {
        minWidth: 84,
        maxWidth: 98,
        width: 90,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexOneMonthChangePercent', 'Index One Month', {
        minWidth: 90,
        maxWidth: 106,
        width: 96,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexThreeMonthChangePercent', 'Index Three Month', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexSixMonthChangePercent', 'Index Six Month', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexOneYearChangePercent', 'Index One Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexTwoYearChangePercent', 'Index Two Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexThreeYearChangePercent', 'Index Three Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('indexFiveYearChangePercent', 'Index Five Year', {
        minWidth: 96,
        maxWidth: 112,
        width: 102,
        valueFormatter: (params: any) => `${formatDisplayValue(params.value)}%`,
      }),
      createCompactColumn('applicableMargin', 'Applicable Margin', {
        minWidth: 84,
        maxWidth: 110,
        width: 90,
        valueFormatter: (params: any) => String(params.value ?? ''),
      }),
      createCompactColumn('varMargin', 'Var Margin', {
        minWidth: 74,
        maxWidth: 96,
        width: 80,
        valueFormatter: (params: any) => String(params.value ?? ''),
      }),
      createCompactColumn('adhocMargin', 'Adhoc Margin', {
        minWidth: 74,
        maxWidth: 96,
        width: 80,
        valueFormatter: (params: any) => String(params.value ?? ''),
      }),
      ...dynamicColumns,
      createCompactColumn('nameOfComplianceOfficer', 'Compliance Officer', { minWidth: 150, maxWidth: 220, filter: true }),
    ];
  }, [dynamicColumns, renderFavoriteButton]);

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
    defaultColDef: {
      ...defaultGridOptions.defaultColDef,
      wrapHeaderText: true,
      autoHeaderHeight: true,
      minWidth: 78,
      maxWidth: 220,
      tooltipValueGetter: (params: any) => getTooltipValue(params),
    },
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
