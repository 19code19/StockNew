namespace Stock.Entity;

public class PriceInfo
{
    public string YearHighDt { get; set; } = string.Empty;
    public string YearLowDt { get; set; } = string.Empty;
    public decimal YearHigh { get; set; }
    public decimal YearLow { get; set; }
    public string CmDailyVolatility { get; set; } = string.Empty;
    public string CmAnnualVolatility { get; set; } = string.Empty;
    public decimal TickSize { get; set; }
    public decimal Inav { get; set; }
    public string? IsINav { get; set; }
    public string PriceBand { get; set; } = string.Empty;
    public string PPriceBand { get; set; } = string.Empty;
}
