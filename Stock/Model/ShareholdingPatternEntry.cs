namespace Stock.Model;

public class ShareholdingCategory
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}

public class ShareholdingPatternEntry
{
    [JsonPropertyName("ndsid")]
    public string Ndsid { get; set; } = string.Empty;

    [JsonPropertyName("series")]
    public string Series { get; set; } = string.Empty;

    [JsonPropertyName("Total")]
    public decimal Total { get; set; }

    [JsonPropertyName("public")]
    public ShareholdingCategory? Public { get; set; }

    [JsonPropertyName("promoter_group")]
    public ShareholdingCategory? PromoterGroup { get; set; }
}

public class ShareholdingPatternResult
{
    public string Symbol { get; set; } = string.Empty;
    public Dictionary<string, ShareholdingPatternEntry>? Data { get; set; }

    public ShareholdingPatternResult(string symbol, Dictionary<string, ShareholdingPatternEntry>? data)
    {
        Symbol = symbol;
        Data = data;
    }
}
