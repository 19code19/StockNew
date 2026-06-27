namespace Stock.Data;

public class IndexDataEntity
{
    public int Id { get; set; }
    public string IndexName { get; set; } = string.Empty;
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Last { get; set; }
    public decimal PreviousClose { get; set; }
    public decimal PercChange { get; set; }
    public decimal YearHigh { get; set; }
    public decimal YearLow { get; set; }
    public string TimeVal { get; set; } = string.Empty;
    public string ConstituentsJson { get; set; } = "{}";
    public decimal IndicativeClose { get; set; }
    public decimal IcChange { get; set; }
    public decimal IcPerChange { get; set; }
    public string IsConstituents { get; set; } = string.Empty;
}
