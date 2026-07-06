namespace Stock.Entity;

public class EquitySymbolDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SymbolDataId { get; set; }
    public SymbolDataEntity? SymbolData { get; set; }
    public Guid? OrderBookId { get; set; }
    public OrderBookEntity? OrderBook { get; set; }
    public Guid? MetaDataId { get; set; }
    public MetaDataEntity? MetaData { get; set; }
    public Guid? TradeInfoId { get; set; }
    public TradeInfoEntity? TradeInfo { get; set; }
    public Guid? PriceInfoId { get; set; }
    public PriceInfoEntity? PriceInfo { get; set; }
    public Guid? SecInfoId { get; set; }
    public SecInfoEntity? SecInfo { get; set; }
    public string LastUpdateTime { get; set; } = string.Empty;
}
