export interface YearwiseStockSummary {
  symbol: string;
  companyName: string;
  basicIndustry: string;
  isSuspended: string;
  macro: string;
  sector: string;
  industryInfo: string;
  yesterdayChangePercent: number;
  oneWeekChangePercent: number;
  oneMonthChangePercent: number;
  threeMonthChangePercent: number;
  sixMonthChangePercent: number;
  oneYearChangePercent: number;
  twoYearChangePercent: number;
  threeYearChangePercent: number;
  fiveYearChangePercent: number;
  indexYesterdayChangePercent: number;
  indexOneWeekChangePercent: number;
  indexOneMonthChangePercent: number;
  indexThreeMonthChangePercent: number;
  indexSixMonthChangePercent: number;
  indexOneYearChangePercent: number;
  indexTwoYearChangePercent: number;
  indexThreeYearChangePercent: number;
  indexFiveYearChangePercent: number;
  indexName: string;
}
