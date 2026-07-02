namespace Stock.Data;

public class HistoricalTradeDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public decimal Ch52WeekHighPrice { get; set; }
    public decimal Ch52WeekLowPrice { get; set; }
    public decimal ChClosingPrice { get; set; }
    public decimal ChLastTradedPrice { get; set; }
    public decimal ChOpeningPrice { get; set; }
    public decimal ChPreviousClsPrice { get; set; }
    public string ChSeries { get; set; } = string.Empty;
    public long ChTotTradedQty { get; set; }
    public decimal ChTotTradedVal { get; set; }
    public long ChTotalTrades { get; set; }
    public decimal ChTradeHighPrice { get; set; }
    public decimal ChTradeLowPrice { get; set; }
    public string MTimestamp { get; set; } = string.Empty;
    public decimal Vwap { get; set; }
    public DateTime BatchCreatedAt { get; set; }
}
