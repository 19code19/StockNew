import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { useFavoriteGridState } from '../components/agGridHelpers';
import { buildCommonSymbolColumns } from '../grid/commonSymbolColumns';
import { defaultGridOptions } from '../grid/defaultGridOptions';
import { apiUrl } from '../api/api';
import { MutualFundScheme } from '../models/MutualFundScheme';
import { createMutualFundColumns } from '../grid/mutualFundColumns';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);


const MutualFundsPage = () => {
  const [rows, setRows] = useState<MutualFundScheme[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [gridApi, setGridApi] = useState<GridApi | null>(null);
  const { renderFavoriteButton } = useFavoriteGridState();

  const fetchRows = useCallback(async () => {
    setLoading(true);
    setError('');

    try {
      const response = await fetch(apiUrl('/api/mutualfund'));
      if (!response.ok) {
        throw new Error('Unable to load mutual funds');
      }

      const data = (await response.json()) as MutualFundScheme[];
      setRows(data.map((row) => ({ ...row, assetType: 'mutualFund' })));
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load mutual funds');
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void fetchRows();
  }, [fetchRows]);

  const columnDefs = useMemo<ColDef[]>(
    () => [
      ...buildCommonSymbolColumns({
        assetType: 'mutualFund',
        includeFavorite: true,
        renderFavorite: renderFavoriteButton,
        externalLinkUrl: (schemeId) => `https://groww.in/mutual-funds/${schemeId}`,
      }),
      ...createMutualFundColumns(),
    ],
    [renderFavoriteButton],
  );

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
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
          <AgGridReact<MutualFundScheme>
            rowData={rows}
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

export default MutualFundsPage;
