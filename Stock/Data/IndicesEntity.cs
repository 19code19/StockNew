namespace Stock.Data;

public class IndicesEntity
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Index { get; set; } = string.Empty;
    public string IndexSymbol { get; set; } = string.Empty;
    public double Last { get; set; }
    public double Variation { get; set; }
    public double PercentChange { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double PreviousClose { get; set; }
    public double YearHigh { get; set; }
    public double YearLow { get; set; }
    public string PE { get; set; } = string.Empty;
    public string PB { get; set; } = string.Empty;
    public string DY { get; set; } = string.Empty;
    public string? Advances { get; set; }
    public string? Declines { get; set; }
    public string? Unchanged { get; set; }
    public double PerChange365d { get; set; }
    public double PerChange30d { get; set; }
    public string? ChartTodayPath { get; set; }
}
