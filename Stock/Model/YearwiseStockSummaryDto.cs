namespace Stock.Model;

public class YearwiseStockSummaryDto
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string IndustryInfo { get; set; } = string.Empty;
    public string BasicIndustry { get; set; } = string.Empty;
    public decimal YesterdayChangePercent { get; set; }
    public decimal OneWeekChangePercent { get; set; }
    public decimal OneMonthChangePercent { get; set; }
    public decimal ThreeMonthChangePercent { get; set; }
    public decimal SixMonthChangePercent { get; set; }
    public decimal OneYearChangePercent { get; set; }
    public string IndexName { get; set; } = string.Empty;
}
