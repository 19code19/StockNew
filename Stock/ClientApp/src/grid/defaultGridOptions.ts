import { type GridOptions } from '@ag-grid-community/core';

export const defaultGridOptions: GridOptions = {
  defaultColDef: {
    sortable: true,
    resizable: true,
    filter: true,
    floatingFilter: true,
    minWidth: 100,
  },
  animateRows: true,
  columnHoverHighlight: true,
  rowDragEntireRow: true,
  rowHeight: 42,
  pagination: true,
  paginationPageSize: 20,
  loading: false,
  rowSelection: {
    mode: 'singleRow',
    enableClickSelection: true,
  },
  rowClassRules: {
    'ag-row-selected-highlight': (params) => Boolean(params.node.isSelected()),
  },
  overlayLoadingTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="mb-3 h-10 w-10 rounded-full border-4 border-slate-700 border-t-sky-300 animate-spin"></div><div class="text-sm font-medium">Loading data...</div></div></div>',
  overlayNoRowsTemplate:
    '<div class="ag-overlay-loading-center text-slate-100"><div class="rounded-3xl border border-slate-700 bg-slate-950/95 px-6 py-5 shadow-lg shadow-slate-950/40"><div class="text-sm font-medium">No records found</div></div></div>',
};
