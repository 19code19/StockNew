namespace Stock.Model;

public class HistoricalTradeData
{
    [JsonPropertyName("ch52WeekHighPrice")]
    public decimal Ch52WeekHighPrice { get; set; }

    [JsonPropertyName("ch52WeekLowPrice")]
    public decimal Ch52WeekLowPrice { get; set; }

    [JsonPropertyName("chClosingPrice")]
    public decimal ChClosingPrice { get; set; }

    [JsonPropertyName("chLastTradedPrice")]
    public decimal ChLastTradedPrice { get; set; }

    [JsonPropertyName("chOpeningPrice")]
    public decimal ChOpeningPrice { get; set; }

    [JsonPropertyName("chPreviousClsPrice")]
    public decimal ChPreviousClsPrice { get; set; }

    [JsonPropertyName("chSeries")]
    public string ChSeries { get; set; } = string.Empty;

    [JsonPropertyName("chSymbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("chTotTradedQty")]
    public long ChTotTradedQty { get; set; }

    [JsonPropertyName("chTotTradedVal")]
    public decimal ChTotTradedVal { get; set; }

    [JsonPropertyName("chTotalTrades")]
    public long ChTotalTrades { get; set; }

    [JsonPropertyName("chTradeHighPrice")]
    public decimal ChTradeHighPrice { get; set; }

    [JsonPropertyName("chTradeLowPrice")]
    public decimal ChTradeLowPrice { get; set; }

    [JsonPropertyName("mtimestamp")]
    public string MTimestamp { get; set; } = string.Empty;

    [JsonPropertyName("vwap")]
    public decimal Vwap { get; set; }
}
