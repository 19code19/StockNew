namespace Stock.Entity;

public class SecInfo
{
    public string SecStatus { get; set; } = string.Empty;
    public string ListingDate { get; set; } = string.Empty;
    public string PdSectorInd { get; set; } = string.Empty;
    public decimal? PdSectorPe { get; set; }
    public decimal? PdSymbolPe { get; set; }
    public string IsSuspended { get; set; } = string.Empty;
    public string BasicIndustry { get; set; } = string.Empty;
    public string Index { get; set; } = string.Empty;
    public string DeliveryQuantity { get; set; } = string.Empty;
    public string DeliveryTotradedQuantity { get; set; } = string.Empty;
    public string SecurityVar { get; set; } = string.Empty;
    public string IndexVar { get; set; } = string.Empty;
    public string ExtremeLossMargin { get; set; } = string.Empty;
    public string VarMargin { get; set; } = string.Empty;
    public string AdhocMargin { get; set; } = string.Empty;
    public string ApplicableMargin { get; set; } = string.Empty;
    public string? BondType { get; set; }
    public string? IssueDesc { get; set; }
    public string? IssueDate { get; set; }
    public string? MaturityDate { get; set; }
    public string? CouponRate { get; set; }
    public string? NxtIpDate { get; set; }
    public string? CreditRating { get; set; }
    public string Macro { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string IndustryInfo { get; set; } = string.Empty;
    public List<string> IndexList { get; set; } = new();
    public string BoardStatus { get; set; } = string.Empty;
    public string TradingSegment { get; set; } = string.Empty;
    public string? SessionNo { get; set; }
    public string ClassShare { get; set; } = string.Empty;
    public string? NameOfComplianceOfficer { get; set; }
    public string? SddCompliance { get; set; }
}
