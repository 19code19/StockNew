import type { ColDef } from '@ag-grid-community/core';
import { createCompactColumn, formatColumnHeader, FORMATTERS, FormatterType } from './Columnformatters';

const COLUMN_SPECS: Array<ColDef & { formatter?: FormatterType }> = [
  { field: 'companyName', headerName: 'Company' },
  { field: 'sector', headerName: 'Sector' },
  { field: 'basicIndustry', headerName: 'Industry', pinned: 'left' },
  { field: 'industryInfo', headerName: 'Industry Info' },
  { field: 'macro', headerName: 'Macro' },
  { field: 'indexName', headerName: 'Index' },
  { field: 'totalTradedVolume', headerName: 'Volume', formatter: 'number', wrapHeaderText: true, autoHeaderHeight: true, width: 100 },
  { field: 'totalTradedValue', headerName: 'Turnover', formatter: 'currency', wrapHeaderText: true, autoHeaderHeight: true, width: 140 },
  { field: 'quantityTraded', headerName: 'Qty Traded', formatter: 'number', wrapHeaderText: true, autoHeaderHeight: true, width: 100 },
  { field: 'deliveryQuantity', headerName: 'Delivery Qty', formatter: 'number', wrapHeaderText: true, autoHeaderHeight: true, width: 100 },
  { field: 'totalMarketCap', headerName: 'Market Cap', formatter: 'currency', wrapHeaderText: true, autoHeaderHeight: true, width: 170 },
  { field: 'yesterdayChangePercent', headerName: 'One Day', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 95 },
  { field: 'oneWeekChangePercent', headerName: 'One Week', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'oneMonthChangePercent', headerName: 'One Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'threeMonthChangePercent', headerName: 'Three Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'sixMonthChangePercent', headerName: 'Six Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'oneYearChangePercent', headerName: 'One Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 95 },
  { field: 'twoYearChangePercent', headerName: 'Two Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'threeYearChangePercent', headerName: 'Three Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'fiveYearChangePercent', headerName: 'Five Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 95 },
  { field: 'indexYesterdayChangePercent', headerName: 'Index One Day', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexOneWeekChangePercent', headerName: 'Index One Week', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexOneMonthChangePercent', headerName: 'Index One Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexThreeMonthChangePercent', headerName: 'Index Three Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexSixMonthChangePercent', headerName: 'Index Six Month', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexOneYearChangePercent', headerName: 'Index One Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexTwoYearChangePercent', headerName: 'Index Two Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexThreeYearChangePercent', headerName: 'Index Three Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 85 },
  { field: 'indexFiveYearChangePercent', headerName: 'Index Five Year', formatter: 'percent', wrapHeaderText: true, autoHeaderHeight: true, width: 95 },
];

const buildColumn = (column: ColDef & { formatter?: FormatterType }): ColDef => {
  const field = column.field as string;
  const formatter = column.formatter ?? 'text';

  return createCompactColumn(field, column.headerName ?? field, {
    ...column,
    tooltipField: column.tooltipField ?? 'companyName',
    valueFormatter: (params: any) => FORMATTERS[formatter](params.value),
  });
};

export const KNOWN_FIELDS = new Set<string>(['symbol', ...COLUMN_SPECS.map((column) => column.field as string)]);

export const buildDynamicColumn = (field: string, sampleValue: unknown): ColDef => {
  const isNumeric = typeof sampleValue === 'number';
  return createCompactColumn(field, formatColumnHeader(field), {
    minWidth: 70,
    maxWidth: isNumeric ? 150 : 200,
    width: isNumeric ? 70 : 100,
    filter: true,
    sortable: true,
    valueFormatter: (params: any) => FORMATTERS.text(params.value),
  });
};

export const buildScopedColumns = (): ColDef[] => COLUMN_SPECS.map((column) => buildColumn(column));
