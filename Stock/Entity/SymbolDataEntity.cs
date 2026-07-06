namespace Stock.Entity;

public class SymbolDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;

    public OrderBookEntity? OrderBook { get; set; }
    public MetaDataEntity? MetaData { get; set; }
    public TradeInfoEntity? TradeInfo { get; set; }
    public PriceInfoEntity? PriceInfo { get; set; }
    public SecInfoEntity? SecInfo { get; set; }

    public DateTime LastUpdateTime { get; set; } = DateTime.Now;
}
