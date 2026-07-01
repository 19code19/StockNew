import { useCallback, useEffect, useMemo, useState } from 'react';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridApi, type GridOptions, type GridReadyEvent } from '@ag-grid-community/core';
import { defaultGridOptions } from './agGridHelpers';

ModuleRegistry.registerModules([ClientSideRowModelModule]);

type HistoricalTradeRow = Record<string, unknown>;

type HistoricalTradePanelProps = {
  rowsCount?: number;
  symbol?: string | null;
};

const formatDateInput = (date: Date) => date.toISOString().slice(0, 10);

const HistoricalTradePanel = ({ rowsCount, symbol }: HistoricalTradePanelProps) => {
  const [fromDate, setFromDate] = useState(() => {
    const date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    return formatDateInput(date);
  });
  const [toDate, setToDate] = useState(() => formatDateInput(new Date()));
  const [rows, setRows] = useState<HistoricalTradeRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [loaded, setLoaded] = useState(false);

  const [columnDefs, setColumnDefs] = useState<ColDef[]>([]);
  const [gridApi, setGridApi] = useState<GridApi | null>(null);

  const createColumnDefs = (rows: HistoricalTradeRow[]) => {
    if (!rows?.length) {
      return [];
    }

    const allKeys = Array.from(
      rows.reduce((set, row) => {
        Object.keys(row).forEach((key) => set.add(key));
        return set;
      }, new Set<string>()),
    );

    return allKeys.map((key) => ({
      field: key,
      headerName: key
        .replace(/([A-Z])/g, ' $1')
        .replace(/^./, (char) => char.toUpperCase()),
      filter: true,
      sortable: true,
      resizable: true,
      minWidth: 130,
      valueFormatter: (params) => {
        if (params.value == null) {
          return '';
        }
        if (typeof params.value === 'number') {
          return Number(params.value).toLocaleString('en-IN');
        }
        return String(params.value);
      },
    })) as ColDef[];
  };

  const gridOptions = useMemo<GridOptions>(() => ({
    ...defaultGridOptions,
    suppressDragLeaveHidesColumns: true,
  }), []);

  useEffect(() => {
    if (!gridApi) {
      return;
    }

    if (loading) {
      gridApi.showLoadingOverlay();
    } else if (rows.length === 0) {
      gridApi.showNoRowsOverlay();
    } else {
      gridApi.hideOverlay();
    }
  }, [gridApi, loading, rows.length]);

  const fetchHistoricalData = async (forceRefresh = false) => {
    setLoading(true);
    setError('');
    try {
      const queryParams = new URLSearchParams({
        fromDate,
        toDate,
        series: 'EQ',
        forceRefresh: forceRefresh ? 'true' : 'false',
      });
      if (symbol) {
        queryParams.append('symbol', symbol);
      }
      const response = await fetch(`/api/nse/historical-trade-data?${queryParams}`);
      if (!response.ok) {
        throw new Error('Failed to load historical trade data');
      }

      const data = (await response.json()) as HistoricalTradeRow[];
      setRows(data);
      setColumnDefs(createColumnDefs(data));
      setLoaded(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unable to load historical trade data');
      setRows([]);
      setLoaded(false);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void fetchHistoricalData(false);
  }, [symbol]);

  return (
    <div className="flex h-full flex-col gap-6">
      <div className="rounded-2xl border border-slate-800 bg-slate-950/80 p-4">
        <div className="grid gap-4 sm:grid-cols-[1.2fr_1.2fr_auto] items-end">
          <label className="flex flex-col gap-2 text-slate-300">
            <span className="text-xs uppercase tracking-[0.2em] text-slate-500">From</span>
            <input
              type="date"
              value={fromDate}
              onChange={(event) => setFromDate(event.target.value)}
              className="rounded-xl border border-slate-700 bg-slate-950 px-3 py-2 text-slate-100 outline-none focus:border-sky-500"
            />
          </label>
          <label className="flex flex-col gap-2 text-slate-300">
            <span className="text-xs uppercase tracking-[0.2em] text-slate-500">To</span>
            <input
              type="date"
              value={toDate}
              onChange={(event) => setToDate(event.target.value)}
              className="rounded-xl border border-slate-700 bg-slate-950 px-3 py-2 text-slate-100 outline-none focus:border-sky-500"
            />
          </label>
          <button
            onClick={() => void fetchHistoricalData(false)}
            disabled={loading}
            className="h-[44px] rounded-2xl bg-sky-500 px-5 text-sm font-semibold text-slate-950 transition hover:bg-sky-400 disabled:cursor-not-allowed disabled:opacity-60"
          >
            Load range
          </button>
        </div>
      </div>

      <div className="text-sm text-slate-300">Loaded {rows.length} row{rows.length === 1 ? '' : 's'}</div>

      <div className="ag-theme-alpine h-[calc(100vh-18rem)] min-h-[420px] w-full rounded-2xl border border-slate-800 bg-slate-950/80">
        <AgGridReact<HistoricalTradeRow>
          rowData={rows}
          columnDefs={columnDefs}
          gridOptions={gridOptions}
          defaultColDef={gridOptions.defaultColDef}
          animateRows={true}
          pagination={true}
          paginationPageSize={20}
        />
      </div>
    </div>
  );
};

export default HistoricalTradePanel;
