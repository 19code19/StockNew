namespace Stock.Data;

public class TradeInfoEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public long TotalTradedVolume { get; set; }
    public decimal TotalTradedValue { get; set; }
    public string Series { get; set; } = string.Empty;
    public decimal LastPrice { get; set; }
    public long IssuedSize { get; set; }
    public decimal BasePrice { get; set; }
    public decimal Ffmc { get; set; }
    public decimal FaceValue { get; set; }
    public decimal ImpactCost { get; set; }
    public decimal DeliveryToTradedQuantity { get; set; }
    public decimal ApplicableMargin { get; set; }
    public int? MarketLot { get; set; }
    public long QuantityTraded { get; set; }
    public long DeliveryQuantity { get; set; }
    public decimal TotalMarketCap { get; set; }
    public string SecWiseDelPosDate { get; set; } = string.Empty;
}
