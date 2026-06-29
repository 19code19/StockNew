namespace Stock.Data;

public class CorpEventCalendarEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public string BmSymbol { get; set; } = string.Empty;
    public string BmDate { get; set; } = string.Empty;
    public string BmPurpose { get; set; } = string.Empty;
    public string BmDesc { get; set; } = string.Empty;
    public string SmIndustry { get; set; } = string.Empty;
    public string BmTimestamp { get; set; } = string.Empty;
    public string SmName { get; set; } = string.Empty;
    public string SmIsin { get; set; } = string.Empty;
    public string BmDt { get; set; } = string.Empty;
    public string BmTimestampFull { get; set; } = string.Empty;
    public string BmAnSeqId { get; set; } = string.Empty;
    public string? BmAttachment { get; set; }
}
