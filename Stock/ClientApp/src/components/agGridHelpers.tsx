import { type ColDef, type GridOptions, type ICellRendererParams } from '@ag-grid-community/core';
import { Link } from 'react-router-dom';

export const createSlug = (company: string) =>
  company
    .trim()
    .replace(/[^a-zA-Z0-9]+/g, '-')
    .replace(/^-+|-+$/g, '')
    .toLowerCase();

export const renderExternalViewLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as { companyName?: string })?.companyName ?? '';
  const slug = createSlug(company);
  return (
    <Link
      className="text-sky-300 hover:text-sky-100 underline"
      to={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
      target="_blank"
      rel="noreferrer"
    >
      View
    </Link>
  );
};

export const renderDetailsLink = (params: ICellRendererParams): JSX.Element | null => {
  const symbol = params.value as string;
  if (!symbol) return null;

  const company = (params.data as { companyName?: string })?.companyName ?? '';
  const slug = createSlug(company);
  return (
    <Link className="text-sky-300 underline hover:text-sky-100" to={`/details/${symbol}/${slug}`}>
      Details
    </Link>
  );
};

export const defaultGridOptions: GridOptions = {
  defaultColDef: {
    sortable: true,
    resizable: true,
    filter: true,
    floatingFilter: true,
    minWidth: 100,
  },
  animateRows: true,
  rowHeight: 42,
  pagination: true,
  paginationPageSize: 20,
  loading: false,
  overlayLoadingTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="mb-3 h-10 w-10 rounded-full border-4 border-slate-700 border-t-sky-300 animate-spin"></div><div class="text-sm font-medium">Loading data...</div></div></div>',
  overlayNoRowsTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="text-sm font-medium">No records found</div></div></div>',
};

export const buildCommonSymbolColumns = (): ColDef[] => [
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
];
