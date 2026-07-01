import { useMemo } from 'react';
import { Link } from 'react-router-dom';
import { AgGridReact } from '@ag-grid-community/react';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';
import { ModuleRegistry, type ColDef, type GridOptions, type ICellRendererParams } from '@ag-grid-community/core';
import { SetFilterModule } from '@ag-grid-enterprise/set-filter';
import { YearwiseStockSummary } from '../models/YearwiseStockSummary';

ModuleRegistry.registerModules([ClientSideRowModelModule, SetFilterModule]);

type YearwiseSummaryGridProps = {
  data: YearwiseStockSummary[];
};

const createSlug = (company: string) =>
  company
    .trim()
    .replace(/[^a-zA-Z0-9]+/g, '-')
    .replace(/^-+|-+$/g, '')
    .toLowerCase();

const renderExternalViewLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as YearwiseStockSummary)?.companyName ?? '';
  const slug = createSlug(company);

  return (
    <a
      className="text-sky-300 hover:text-sky-100 underline"
      href={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
      target="_blank"
      rel="noreferrer"
    >
      View
    </a>
  );
};

const renderDetailsLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as YearwiseStockSummary)?.companyName ?? '';
  const slug = createSlug(company);

  return (
    <Link className="text-sky-300 underline hover:text-sky-100" to={`/details/${symbol}/${slug}`}>
      Details
    </Link>
  );
};

const YearwiseSummaryGrid = ({ data }: YearwiseSummaryGridProps) => {
  const columnDefs = useMemo<ColDef[]>(
    () => [
      {
        colId: 'view',
        field: 'symbol',
        headerName: 'View',
        width: 70,
        cellRenderer: renderExternalViewLink,
        sortable: false,
        filter: false,
        pinned: 'left',
      },
      {
        colId: 'details',
        field: 'symbol',
        headerName: 'Details',
        width: 80,
        cellRenderer: renderDetailsLink,
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
    defaultColDef: {
      sortable: true,
      resizable: true,
      filter: true,
      floatingFilter: true,
    },
    animateRows: true,
    rowHeight: 42,
    pagination: true,
    paginationPageSize: 20,
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
