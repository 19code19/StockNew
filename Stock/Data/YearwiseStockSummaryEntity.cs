using Microsoft.EntityFrameworkCore;

namespace Stock.Data;

[Keyless]
public class YearwiseStockSummaryEntity
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string IndustryInfo { get; set; } = string.Empty;
    public string BasicIndustry { get; set; } = string.Empty;
    public long TotalTradedVolume { get; set; }
    public decimal TotalTradedValue { get; set; }
    public decimal DeliveryToTradedQuantity { get; set; }
    public long QuantityTraded { get; set; }
    public long DeliveryQuantity { get; set; }
    public decimal TotalMarketCap { get; set; }
    public decimal YearHigh { get; set; }
    public decimal YearLow { get; set; }
    public decimal YesterdayChangePercent { get; set; }
    public decimal OneWeekChangePercent { get; set; }
    public decimal OneMonthChangePercent { get; set; }
    public decimal ThreeMonthChangePercent { get; set; }
    public decimal SixMonthChangePercent { get; set; }
    public decimal OneYearChangePercent { get; set; }
    public string IndexName { get; set; } = string.Empty;
}
