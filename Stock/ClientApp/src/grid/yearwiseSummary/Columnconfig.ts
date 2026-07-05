import type { ColDef } from '@ag-grid-community/core';
import { createCompactColumn, formatColumnHeader, FORMATTERS, FormatterType } from './Columnformatters';

const COLUMN_SPECS: Array<ColDef & { formatter?: FormatterType }> = [
  { field: 'companyName', headerName: 'Company' },
  { field: 'sector', headerName: 'Sector' },
  { field: 'basicIndustry', headerName: 'Industry', pinned: 'left' },
  { field: 'industryInfo', headerName: 'Industry Info' },
  { field: 'macro', headerName: 'Macro' },
  { field: 'indexName', headerName: 'Index' },
  { field: 'totalTradedVolume', headerName: 'Volume', formatter: 'number' },
  { field: 'totalTradedValue', headerName: 'Turnover', formatter: 'currency' },
  { field: 'quantityTraded', headerName: 'Qty Traded', formatter: 'number' },
  { field: 'deliveryQuantity', headerName: 'Delivery Qty', formatter: 'number' },
  { field: 'totalMarketCap', headerName: 'Market Cap', formatter: 'currency' },
  { field: 'yesterdayChangePercent', headerName: 'One Day', formatter: 'percent' },
  { field: 'oneWeekChangePercent', headerName: 'One Week', formatter: 'percent' },
  { field: 'oneMonthChangePercent', headerName: 'One Month', formatter: 'percent' },
  { field: 'threeMonthChangePercent', headerName: 'Three Month', formatter: 'percent' },
  { field: 'sixMonthChangePercent', headerName: 'Six Month', formatter: 'percent' },
  { field: 'oneYearChangePercent', headerName: 'One Year', formatter: 'percent' },
  { field: 'twoYearChangePercent', headerName: 'Two Year', formatter: 'percent' },
  { field: 'threeYearChangePercent', headerName: 'Three Year', formatter: 'percent' },
  { field: 'fiveYearChangePercent', headerName: 'Five Year', formatter: 'percent' },
  { field: 'indexYesterdayChangePercent', headerName: 'Index One Day', formatter: 'percent' },
  { field: 'indexOneWeekChangePercent', headerName: 'Index One Week', formatter: 'percent' },
  { field: 'indexOneMonthChangePercent', headerName: 'Index One Month', formatter: 'percent' },
  { field: 'indexThreeMonthChangePercent', headerName: 'Index Three Month', formatter: 'percent' },
  { field: 'indexSixMonthChangePercent', headerName: 'Index Six Month', formatter: 'percent' },
  { field: 'indexOneYearChangePercent', headerName: 'Index One Year', formatter: 'percent' },
  { field: 'indexTwoYearChangePercent', headerName: 'Index Two Year', formatter: 'percent' },
  { field: 'indexThreeYearChangePercent', headerName: 'Index Three Year', formatter: 'percent' },
  { field: 'indexFiveYearChangePercent', headerName: 'Index Five Year', formatter: 'percent' },
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
