namespace Stock.Model;

public class CorpAnnualReport
{
    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("fromYr")]
    public string FromYr { get; set; } = string.Empty;

    [JsonPropertyName("toYr")]
    public string ToYr { get; set; } = string.Empty;

    [JsonPropertyName("submission_type")]
    public string SubmissionType { get; set; } = string.Empty;

    [JsonPropertyName("broadcast_dttm")]
    public string BroadcastDttm { get; set; } = string.Empty;

    [JsonPropertyName("disseminationDateTime")]
    public string DisseminationDateTime { get; set; } = string.Empty;

    [JsonPropertyName("timeTaken")]
    public string TimeTaken { get; set; } = string.Empty;

    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }

    [JsonPropertyName("attFileSize")]
    public string? AttFileSize { get; set; }
}

public class CorpAnnualReportResult
{
    public string Symbol { get; set; } = string.Empty;
    public List<CorpAnnualReport>? Data { get; set; }

    public CorpAnnualReportResult(string symbol, List<CorpAnnualReport>? data)
    {
        Data = data;
        Symbol = symbol;
    }
}
