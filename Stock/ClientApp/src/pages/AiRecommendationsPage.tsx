import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { useFavoriteGridState } from '../components/agGridHelpers';
import { buildCommonSymbolColumns, createSlug } from '../grid/commonSymbolColumns';
import { createAiRecommendationColumns } from '../grid/columnConfigs';
import { defaultGridOptions } from '../grid/defaultGridOptions';
import { apiUrl } from '../api/api';
import { AiRecommendationView } from '../models/AiRecommendationView';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

const AiRecommendationsPage = () => {
  const [rows, setRows] = useState<AiRecommendationView[]>([]);
  const [assetType, setAssetType] = useState<'all' | 'stock' | 'mutualfund'>('all');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [gridApi, setGridApi] = useState<GridApi | null>(null);
  const { favoriteSymbols, renderFavoriteButton } = useFavoriteGridState();

  const fetchRows = useCallback(async () => {
    setLoading(true);
    setError('');

    try {
      const query = assetType === 'all' ? '' : `?assetType=${encodeURIComponent(assetType)}`;
      const response = await fetch(apiUrl(`/api/ai/recommendations/view${query}`));
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
  }, [assetType]);

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
      ...buildCommonSymbolColumns({
        includeFavorite: true,
        renderFavorite: renderFavoriteButton,
        showNse: false,
        externalLinkUrl: (symbol, company, params) => {
          const assetType = (params.data as AiRecommendationView)?.assetType ?? 'stock';
          if (assetType.toLowerCase() === 'mutualfund' || assetType.toLowerCase() === 'mutual-fund') {
            return `https://groww.in/mutual-funds/${symbol}`;
          }

          const slug = createSlug(company || symbol);
          return `https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`;
        },
      }),
      ...createAiRecommendationColumns(),
    ],
    [renderFavoriteButton],
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

      <section className="flex min-h-0 flex-1 rounded-2xl border border-slate-800 bg-slate-950/80 p-4 shadow-2xl">
        <div className="ag-theme-alpine h-full min-h-0 w-full" style={{ width: '100%', height: '100%' }}>
          <AgGridReact<AiRecommendationView>
            rowData={rows}
            columnDefs={columnDefs}
            gridOptions={gridOptions}
            onGridReady={onGridReady}
            defaultColDef={gridOptions.defaultColDef}
            loading={loading}
          />
        </div>
      </section>
    </div>
  );
};

export default AiRecommendationsPage;
