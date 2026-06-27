namespace Stock.Data;

public class EquitySymbolDataEntity
{
    public int Id { get; set; }
    public int SymbolDataId { get; set; }
    public SymbolDataEntity? SymbolData { get; set; }
    public int? OrderBookId { get; set; }
    public OrderBookEntity? OrderBook { get; set; }
    public int? MetaDataId { get; set; }
    public MetaDataEntity? MetaData { get; set; }
    public int? TradeInfoId { get; set; }
    public TradeInfoEntity? TradeInfo { get; set; }
    public int? PriceInfoId { get; set; }
    public PriceInfoEntity? PriceInfo { get; set; }
    public int? SecInfoId { get; set; }
    public SecInfoEntity? SecInfo { get; set; }
    public string LastUpdateTime { get; set; } = string.Empty;
}
