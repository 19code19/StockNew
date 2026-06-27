namespace Stock.Data;

public class ShareholdingPatternEntryEntity
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Ndsid { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public decimal Value { get; set; }
}
