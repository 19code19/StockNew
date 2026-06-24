namespace Stock.Model;

public class PeerComparisonData
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("series")]
    public string Series { get; set; } = string.Empty;

    [JsonPropertyName("marketType")]
    public string MarketType { get; set; } = string.Empty;

    [JsonPropertyName("marketCap")]
    public decimal MarketCap { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [JsonPropertyName("volume")]
    public long Volume { get; set; }

    [JsonPropertyName("eps")]
    public decimal Eps { get; set; }

    [JsonPropertyName("ltp")]
    public decimal Ltp { get; set; }

    [JsonPropertyName("pat")]
    public decimal Pat { get; set; }

    [JsonPropertyName("pe")]
    public decimal Pe { get; set; }

    [JsonPropertyName("debtEqRatio")]
    public decimal? DebtEqRatio { get; set; }

    [JsonPropertyName("promoterHolding")]
    public decimal PromoterHolding { get; set; }

    [JsonPropertyName("totalIncome")]
    public decimal TotalIncome { get; set; }

    [JsonPropertyName("PChange")]
    public decimal PChangeUpper { get; set; }

    [JsonPropertyName("pChange")]
    public decimal PChange { get; set; }
}

public class PeerComparisonDataResult
{
    public string Symbol { get; set; } = string.Empty;
    public string Quarter { get; set; } = string.Empty;
    public List<PeerComparisonData>? Data { get; set; }

    public PeerComparisonDataResult( string symbol, string quarter, List<PeerComparisonData>? data)
    {
        Symbol = symbol;
        Quarter = quarter;
        Data = data;
    }
}
