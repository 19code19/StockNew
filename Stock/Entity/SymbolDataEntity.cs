namespace Stock.Entity;

public class SymbolDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;
    public ICollection<EquitySymbolDataEntity> EquityResponse { get; set; } = new List<EquitySymbolDataEntity>();
}
