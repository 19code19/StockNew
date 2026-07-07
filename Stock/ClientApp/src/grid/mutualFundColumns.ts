import type { ColDef } from '@ag-grid-community/core';

export const createMutualFundColumns = (): ColDef[] => [
  { field: 'fundHouse', headerName: 'Fund House', minWidth: 180, flex: 1 },
  { field: 'amc', headerName: 'AMC', minWidth: 140, flex: 1 },
  { field: 'schemeName', headerName: 'Scheme', minWidth: 400, flex: 2 },
  { field: 'nav', headerName: 'NAV', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return1D', headerName: '1D', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return3M', headerName: '3M', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return6M', headerName: '6M', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return1Y', headerName: '1Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return3Y', headerName: '3Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return5Y', headerName: '5Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return7Y', headerName: '7Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'return10Y', headerName: '10Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'sipReturn1Y', headerName: 'SIP 1Y', minWidth: 120, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'sipReturn3Y', headerName: 'SIP 3Y', minWidth: 120, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'sipReturn5Y', headerName: 'SIP 5Y', minWidth: 120, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'sipReturn10Y', headerName: 'SIP 10Y', minWidth: 100, flex: 1, filter: 'agNumberColumnFilter' },
  { field: 'risk', headerName: 'Risk', minWidth: 100, flex: 1 },
  { field: 'riskRating', headerName: 'Risk Rating', minWidth: 110, flex: 1, filter: 'agNumberColumnFilter' },
];
