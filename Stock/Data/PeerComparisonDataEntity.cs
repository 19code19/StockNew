namespace Stock.Data;

public class PeerComparisonDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string Quarter { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;
    public decimal MarketCap { get; set; }
    public decimal Value { get; set; }
    public long Volume { get; set; }
    public decimal Eps { get; set; }
    public decimal Ltp { get; set; }
    public decimal Pat { get; set; }
    public decimal Pe { get; set; }
    public decimal? DebtEqRatio { get; set; }
    public decimal PromoterHolding { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal PChangeUpper { get; set; }
    public decimal PChange { get; set; }
}
