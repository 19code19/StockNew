using Microsoft.EntityFrameworkCore;

namespace Stock.Data;

[Keyless]
public class YearwiseStockSummaryEntity
{
    //YearwiseDataEntity
    public decimal YesterdayChangePercent { get; set; }
    public decimal OneWeekChangePercent { get; set; }
    public decimal OneMonthChangePercent { get; set; }
    public decimal ThreeMonthChangePercent { get; set; }
    public decimal SixMonthChangePercent { get; set; }
    public decimal OneYearChangePercent { get; set; }
    public decimal TwoYearChangePercent { get; set; }
    public decimal ThreeYearChangePercent { get; set; }
    public decimal FiveYearChangePercent { get; set; }
    public string OneWeekDate { get; set; } = string.Empty;
    public decimal IndexYesterdayChangePercent { get; set; }
    public decimal IndexOneWeekChangePercent { get; set; }
    public decimal IndexOneMonthChangePercent { get; set; }
    public decimal IndexThreeMonthChangePercent { get; set; }
    public decimal IndexSixMonthChangePercent { get; set; }
    public decimal IndexOneYearChangePercent { get; set; }
    public decimal IndexTwoYearChangePercent { get; set; }
    public decimal IndexThreeYearChangePercent { get; set; }
    public decimal IndexFiveYearChangePercent { get; set; }
    public string IndexOneWeekDate { get; set; } = string.Empty;
    public string IndexName { get; set; } = string.Empty;

    //TradeInfoEntity
    public long TotalTradedVolume { get; set; }
    public decimal TotalTradedValue { get; set; }
    public decimal DeliveryToTradedQuantity { get; set; }
    public long QuantityTraded { get; set; }
    public long DeliveryQuantity { get; set; }
    public decimal TotalMarketCap { get; set; }
    public decimal LastPrice { get; set; }
    public long IssuedSize { get; set; }

    //SecInfoEntity

    public string BasicIndustry { get; set; } = string.Empty;
    public string IsSuspended { get; set; } = string.Empty;
    public string VarMargin { get; set; } = string.Empty;
    public string ApplicableMargin { get; set; } = string.Empty;
    public string AdhocMargin { get; set; } = string.Empty;
    public string? IssueDesc { get; set; }
    public string Macro { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string IndustryInfo { get; set; } = string.Empty;
    public string IndexListJson { get; set; } = "[]";
    public string TradingSegment { get; set; } = string.Empty;
    public string? NameOfComplianceOfficer { get; set; }

    //PriceInfoEntities
    public string YearHighDt { get; set; } = string.Empty;
    public string YearLowDt { get; set; } = string.Empty;
    public decimal YearHigh { get; set; }
    public decimal YearLow { get; set; }
    public string CmDailyVolatility { get; set; } = string.Empty;
    public string CmAnnualVolatility { get; set; } = string.Empty;

    //MetaDataEntities

    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal PChange { get; set; }
    public decimal Open { get; set; }
    public decimal DayHigh { get; set; }
    public decimal DayLow { get; set; }
    public decimal PreviousClose { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ClosePrice { get; set; }
}
