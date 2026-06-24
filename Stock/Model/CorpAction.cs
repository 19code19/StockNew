namespace Stock.Model;

public class CorpAction
{
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("recordDate")]
    public string? RecordDate { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("series")]
    public string Series { get; set; } = string.Empty;

    [JsonPropertyName("ind")]
    public string Ind { get; set; } = string.Empty;

    [JsonPropertyName("faceVal")]
    public string FaceVal { get; set; } = string.Empty;

    [JsonPropertyName("exDate")]
    public string ExDate { get; set; } = string.Empty;

    [JsonPropertyName("recDate")]
    public string RecDate { get; set; } = string.Empty;

    [JsonPropertyName("bcStartDate")]
    public string BcStartDate { get; set; } = string.Empty;

    [JsonPropertyName("bcEndDate")]
    public string BcEndDate { get; set; } = string.Empty;

    [JsonPropertyName("ndStartDate")]
    public string NdStartDate { get; set; } = string.Empty;

    [JsonPropertyName("comp")]
    public string Comp { get; set; } = string.Empty;

    [JsonPropertyName("isin")]
    public string Isin { get; set; } = string.Empty;

    [JsonPropertyName("ndEndDate")]
    public string NdEndDate { get; set; } = string.Empty;

    [JsonPropertyName("caBroadcastDate")]
    public string? CaBroadcastDate { get; set; }
}

public class CorpActionResult
{
    public string Symbol { get; set; } = string.Empty;
    public List<CorpAction>? Data { get; set; }

    public CorpActionResult(string symbol, List<CorpAction>? data) 
    {
        Data = data;
        Symbol = symbol;
    }
}
