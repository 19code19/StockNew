namespace Stock.Data;

public class CorpAnnualReportEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
    public string FromYr { get; set; } = string.Empty;
    public string ToYr { get; set; } = string.Empty;
    public string SubmissionType { get; set; } = string.Empty;
    public string BroadcastDttm { get; set; } = string.Empty;
    public string DisseminationDateTime { get; set; } = string.Empty;
    public string TimeTaken { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public string? AttFileSize { get; set; }
}
