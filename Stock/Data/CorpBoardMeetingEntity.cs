namespace Stock.Data;

public class CorpBoardMeetingEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    // common symbol field for queries
    public string Symbol { get; set; } = string.Empty;

    public string BmSymbol { get; set; } = string.Empty;
    public string BmDate { get; set; } = string.Empty;
    public string BmPurpose { get; set; } = string.Empty;
    public string BmDesc { get; set; } = string.Empty;
    public string SmIndustry { get; set; } = string.Empty;
    public string BmTimestamp { get; set; } = string.Empty;
    public string SmName { get; set; } = string.Empty;
    public string SmIsin { get; set; } = string.Empty;
    public string? Attachment { get; set; }
    public string? Ixbrl { get; set; }
    public string Diff { get; set; } = string.Empty;
    public string SysTime { get; set; } = string.Empty;
    public string? AttFileSize { get; set; }
    public string? IxbrlFileSize { get; set; }
}
