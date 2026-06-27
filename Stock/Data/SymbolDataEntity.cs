namespace Stock.Data;

public class SymbolDataEntity
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;
    public ICollection<EquitySymbolDataEntity> EquityResponse { get; set; } = new List<EquitySymbolDataEntity>();
}
