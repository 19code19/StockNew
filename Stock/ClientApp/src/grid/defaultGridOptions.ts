import { type GridOptions } from '@ag-grid-community/core';

export const defaultGridOptions: GridOptions = {
  defaultColDef: {
    sortable: true,
    resizable: true,
    filter: true,
    floatingFilter: true,
    minWidth: 100,
    wrapHeaderText: true,
    autoHeaderHeight: true,
    flex: 1,
  },
  animateRows: true,
  columnHoverHighlight: true,
  rowDragEntireRow: true,
  rowHeight: 42,
  loading: false,
  pagination: true,
  paginationPageSize: 100,
  paginationPageSizeSelector: [20, 50, 100, 200, 500, 1000, 2000, 5000],
  rowSelection: {
    mode: 'singleRow',
    enableClickSelection: true,
  },
  rowClassRules: {
    'ag-row-selected-highlight': (params) => Boolean(params.node.isSelected()),
  },
  overlayLoadingTemplate:
  '<div class="flex items-center gap-4 text-slate-300"><div class="h-16 w-16 rounded-full border-8 border-slate-600 border-t-sky-400 animate-spin"></div><span class="text-2xl font-semibold">Loading...</span></div>',
overlayNoRowsTemplate:
  '<div class="text-2xl font-semibold text-slate-400">No records found</div>',
};