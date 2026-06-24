namespace Stock.Model;

public class IndexDataResponse
{
    [JsonPropertyName("data")]
    public List<IndexData>? Data { get; set; }
}

public class IndexData
{
    [JsonPropertyName("indexName")]
    public string IndexName { get; set; } = string.Empty;

    [JsonPropertyName("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("high")]
    public decimal High { get; set; }

    [JsonPropertyName("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("last")]
    public decimal Last { get; set; }

    [JsonPropertyName("previousClose")]
    public decimal PreviousClose { get; set; }

    [JsonPropertyName("percChange")]
    public decimal PercChange { get; set; }

    [JsonPropertyName("yearHigh")]
    public decimal YearHigh { get; set; }

    [JsonPropertyName("yearLow")]
    public decimal YearLow { get; set; }

    [JsonPropertyName("timeVal")]
    public string TimeVal { get; set; } = string.Empty;

    [JsonPropertyName("constituents")]
    public object? Constituents { get; set; }

    [JsonPropertyName("indicativeClose")]
    public decimal IndicativeClose { get; set; }

    [JsonPropertyName("icChange")]
    public decimal IcChange { get; set; }

    [JsonPropertyName("icPerChange")]
    public decimal IcPerChange { get; set; }

    [JsonPropertyName("isConstituents")]
    public string IsConstituents { get; set; } = string.Empty;
}
