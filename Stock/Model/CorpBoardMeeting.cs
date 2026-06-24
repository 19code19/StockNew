namespace Stock.Model;

public class CorpBoardMeeting
{
    [JsonPropertyName("bm_symbol")]
    public string BmSymbol { get; set; } = string.Empty;

    [JsonPropertyName("bm_date")]
    public string BmDate { get; set; } = string.Empty;

    [JsonPropertyName("bm_purpose")]
    public string BmPurpose { get; set; } = string.Empty;

    [JsonPropertyName("bm_desc")]
    public string BmDesc { get; set; } = string.Empty;

    [JsonPropertyName("sm_indusrty")]
    public string SmIndustry { get; set; } = string.Empty;

    [JsonPropertyName("bm_timestamp")]
    public string BmTimestamp { get; set; } = string.Empty;

    [JsonPropertyName("sm_name")]
    public string SmName { get; set; } = string.Empty;

    [JsonPropertyName("sm_isin")]
    public string SmIsin { get; set; } = string.Empty;

    [JsonPropertyName("attachment")]
    public string? Attachment { get; set; }

    [JsonPropertyName("ixbrl")]
    public string? Ixbrl { get; set; }

    [JsonPropertyName("diff")]
    public string Diff { get; set; } = string.Empty;

    [JsonPropertyName("sysTime")]
    public string SysTime { get; set; } = string.Empty;

    [JsonPropertyName("attFileSize")]
    public string? AttFileSize { get; set; }

    [JsonPropertyName("ixbrlFileSize")]
    public string? IxbrlFileSize { get; set; }
}

public class CorpBoardMeetingResult
{
    public string Symbol { get; set; } = string.Empty;
    public List<CorpBoardMeeting>? Data { get; set; }
    public CorpBoardMeetingResult(string symbol, List<CorpBoardMeeting>? data)
    {
        Data = data;
        Symbol = symbol;
    }
}
