namespace Stock.Data;

public class OrderBookEntity
{
    public int Id { get; set; }
    public decimal BuyPrice1 { get; set; }
    public long BuyQuantity1 { get; set; }
    public decimal BuyPrice2 { get; set; }
    public long BuyQuantity2 { get; set; }
    public decimal BuyPrice3 { get; set; }
    public long BuyQuantity3 { get; set; }
    public decimal BuyPrice4 { get; set; }
    public long BuyQuantity4 { get; set; }
    public decimal BuyPrice5 { get; set; }
    public long BuyQuantity5 { get; set; }
    public decimal SellPrice1 { get; set; }
    public long SellQuantity1 { get; set; }
    public decimal SellPrice2 { get; set; }
    public long SellQuantity2 { get; set; }
    public decimal SellPrice3 { get; set; }
    public long SellQuantity3 { get; set; }
    public decimal SellPrice4 { get; set; }
    public long SellQuantity4 { get; set; }
    public decimal SellPrice5 { get; set; }
    public long SellQuantity5 { get; set; }
    public decimal LastPrice { get; set; }
    public long TotalBuyQuantity { get; set; }
    public long TotalSellQuantity { get; set; }
    public decimal PerBuyQty { get; set; }
    public decimal PerSellQty { get; set; }
}
