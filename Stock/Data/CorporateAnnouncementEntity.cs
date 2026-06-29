namespace Stock.Data;

public class CorporateAnnouncementEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public string Desc { get; set; } = string.Empty;
    public string Dt { get; set; } = string.Empty;
    public string? AttchmntFile { get; set; }
    public string SmName { get; set; } = string.Empty;
    public string SmIsin { get; set; } = string.Empty;
    public string AnDt { get; set; } = string.Empty;
    public string SortDate { get; set; } = string.Empty;
    public string? SeqId { get; set; }
    public string SmIndustry { get; set; } = string.Empty;
    public string? Orgid { get; set; }
    public string? AttchmntText { get; set; }
    public string? Bflag { get; set; }
    public string? OldNew { get; set; }
    public string? CsvName { get; set; }
    public string ExchDissTime { get; set; } = string.Empty;
    public string Difference { get; set; } = string.Empty;
    public string? FileSize { get; set; }
    public string? AttFileSize { get; set; }
    public bool HasXbrl { get; set; }
}
