import type { ColDef } from '@ag-grid-community/core';
import { YEARWISE_COLUMNS } from './yearwiseSummary/Columnconfig';

export const createYearwiseSummaryColumns = (): ColDef[] => YEARWISE_COLUMNS;

export const createAiRecommendationColumns = (): ColDef[] => [
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
];

export const createFavoritesColumns = (): ColDef[] => [
  {
    field: 'companyName',
    headerName: 'Company',
    minWidth: 260,
  },
  {
    field: 'addedAt',
    headerName: 'Added',
    minWidth: 180,
    valueFormatter: (params) => new Date(params.value as string).toLocaleString(),
  },
];
