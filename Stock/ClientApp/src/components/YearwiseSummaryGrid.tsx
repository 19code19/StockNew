import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { YearwiseStockSummary } from '../models/YearwiseStockSummary';
import { buildCommonSymbolColumns, defaultGridOptions, useFavoriteGridState } from './agGridHelpers';
import { buildDynamicColumn, buildScopedColumns } from '../grid/yearwiseSummary/Columnconfig';
import { getTooltipValue } from '../grid/yearwiseSummary/Columnformatters';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

type YearwiseSummaryGridProps = {
  data: YearwiseStockSummary[];
  loading?: boolean;
};

const YearwiseSummaryGrid = ({ data, loading = false }: YearwiseSummaryGridProps) => {
  const { favoriteSymbols, renderFavoriteButton } = useFavoriteGridState();
  const [gridApi, setGridApi] = useState<GridApi | null>(null);

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

  const dynamicColumns = useMemo<ColDef[]>(() => {
    const usedFields = new Set<string>([
      'symbol',
      'companyName',
      'sector',
      'basicIndustry',
      'industryInfo',
      'macro',
      'indexName',
      'totalTradedVolume',
      'totalTradedValue',
      'quantityTraded',
      'deliveryQuantity',
      'totalMarketCap',
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
    ]);

    const rowFields = data?.length ? Object.keys(data[0]) : [];

    return rowFields
      .filter((field) => !usedFields.has(field))
      .map((field) => buildDynamicColumn(field, data?.[0]?.[field as keyof YearwiseStockSummary])) as ColDef[];
  }, [data]);

  const columnDefs = useMemo<ColDef[]>(() => {
    const commonSymbolColumns = buildCommonSymbolColumns({ includeFavorite: true, renderFavorite: renderFavoriteButton }).map((col) => ({
      ...col,
      wrapHeaderText: true,
      autoHeaderHeight: true,
      tooltipValueGetter: (params: any) => getTooltipValue(params),
    }));

    return [...commonSymbolColumns, ...buildScopedColumns(), ...dynamicColumns];
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
