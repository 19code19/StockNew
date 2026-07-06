import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { buildCommonSymbolColumns, defaultGridOptions, useFavoriteGridState } from '../components/agGridHelpers';
import { AiRecommendationView } from '../models/AiRecommendationView';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

const AiRecommendationsPage = () => {
  const [rows, setRows] = useState<AiRecommendationView[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [gridApi, setGridApi] = useState<GridApi | null>(null);
  const { favoriteSymbols, renderFavoriteButton } = useFavoriteGridState();

  const fetchRows = useCallback(async () => {
    setLoading(true);
    setError('');

    try {
      const response = await fetch('/api/ai/recommendations/view');
      if (!response.ok) {
        throw new Error('Unable to load AI recommendations');
      }

      const data = (await response.json()) as AiRecommendationView[];
      setRows(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load AI recommendations');
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void fetchRows();
  }, [fetchRows]);

  useEffect(() => {
    if (!gridApi) {
      return;
    }

    gridApi.refreshCells({ force: true, columns: ['symbol'] });
  }, [favoriteSymbols, gridApi]);

  const columnDefs = useMemo<ColDef[]>(
    () => [
      ...buildCommonSymbolColumns({ includeFavorite: true, renderFavorite: renderFavoriteButton }),
      { field: 'rank', headerName: 'Rank', minWidth: 80, filter: 'agNumberColumnFilter', flex: 1 },
      { field: 'companyName', headerName: 'Company', minWidth: 180, flex: 2 },
      { field: 'category', headerName: 'Category', minWidth: 140, flex: 1 },
      {
        field: 'score',
        headerName: 'Score',
        minWidth: 100,
        flex: 1,
        valueFormatter: (params) => Number(params.value as number).toFixed(2),
        filter: 'agNumberColumnFilter',
      },
      { field: 'source', headerName: 'Source', minWidth: 140, flex: 1 },
      {
        field: 'reason',
        headerName: 'Reason',
        minWidth: 180,
        flex: 2,
        tooltipField: 'reason',
      },
      { field: 'basicIndustry', headerName: 'Industry', minWidth: 140, flex: 1 },
      { field: 'sector', headerName: 'Sector', minWidth: 140, flex: 1 },
      { field: 'macro', headerName: 'Macro', minWidth: 160, flex: 1 },
      { field: 'industryInfo', headerName: 'Industry Info', minWidth: 160, flex: 1 },
      { field: 'issueDesc', headerName: 'Issue Desc', minWidth: 180, flex: 1 },
      {
        field: 'createdAt',
        headerName: 'Created',
        minWidth: 140,
        flex: 1,
        valueFormatter: (params) => new Date(params.value as string).toLocaleString(),
      },
    ],
    [favoriteSymbols, renderFavoriteButton],
  );

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
    domLayout: 'normal',
    suppressHorizontalScroll: true,
    sideBar: {
      toolPanels: [
        {
          id: 'columns',
          labelDefault: 'Columns',
          labelKey: 'columns',
          iconKey: 'columns',
          toolPanel: 'agColumnsToolPanel',
        },
      ],
      defaultToolPanel: 'columns',
    },
    defaultColDef: {
      ...defaultGridOptions.defaultColDef,
      filter: true,
      sortable: true,
      resizable: true,
      floatingFilter: true,
      flex: 1,
      minWidth: 120,
    },
  }), []);

  const onGridReady = useCallback((params: GridReadyEvent) => {
    setGridApi(params.api);
    params.api.sizeColumnsToFit();
  }, []);

  useEffect(() => {
    if (gridApi) {
      gridApi.sizeColumnsToFit();
    }
  }, [gridApi, rows]);

  useEffect(() => {
    if (!gridApi) return;
    if (loading) {
      gridApi.showLoadingOverlay();
    } else if (rows.length === 0) {
      gridApi.showNoRowsOverlay();
    } else {
      gridApi.hideOverlay();
    }
  }, [gridApi, loading, rows.length]);

  return (
    <div className="flex h-full w-full flex-col gap-4">
      {error ? <div className="rounded-2xl border border-rose-700 bg-rose-950/20 p-4 text-rose-200">{error}</div> : null}

      <section className="flex min-h-0 flex-1 rounded-2xl border border-slate-800 bg-slate-950/80 p-4 shadow-2xl">
        <div className="ag-theme-alpine h-full min-h-0 w-full" style={{ width: '100%', height: '100%' }}>
          <AgGridReact<AiRecommendationView>
            rowData={rows}
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

export default AiRecommendationsPage;
