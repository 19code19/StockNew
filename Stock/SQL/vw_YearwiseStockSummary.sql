CREATE  OR ALTER  VIEW [dbo].[vw_YearwiseStockSummary] AS
SELECT 
    Y.YesterdayChangePercent,
    Y.OneWeekChangePercent,
    Y.OneMonthChangePercent,
    Y.ThreeMonthChangePercent,
    Y.SixMonthChangePercent,
    Y.OneYearChangePercent,
    Y.TwoYearChangePercent,
    Y.ThreeYearChangePercent,
    Y.FiveYearChangePercent,
    Y.IndexYesterdayChangePercent,
    Y.IndexOneWeekChangePercent,
    Y.IndexOneMonthChangePercent,
    Y.IndexThreeMonthChangePercent,
    Y.IndexSixMonthChangePercent,
    Y.IndexOneYearChangePercent,
    Y.IndexTwoYearChangePercent,
    Y.IndexThreeYearChangePercent,
    Y.IndexFiveYearChangePercent,
    Y.IndexName,
    S.BasicIndustry,
    S.IsSuspended,
    S.Macro,
    S.Sector,
    S.IndustryInfo,
    M.CompanyName,
    M.Symbol
FROM YearwiseDataEntities    Y
INNER JOIN TradeInfoEntities T ON REPLACE(Y.Symbol, 'EQN', '') = T.Symbol
INNER JOIN SecInfoEntities   S ON S.Symbol = T.Symbol
INNER JOIN PriceInfoEntities P ON S.Symbol = P.Symbol
INNER JOIN MetaDataEntities  M ON M.Symbol = P.Symbol;
GO
