namespace Stock.Data;

public class FinancialStatusEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public string? FromDate { get; set; }
    public string? ToDate { get; set; }
    public string? ToDateMonYr { get; set; }
    public string? Series { get; set; }
    public string? Expenditure { get; set; }
    public decimal? TotalIncome { get; set; }
    public string? Audited { get; set; }
    public string? Cumulative { get; set; }
    public string? Consolidated { get; set; }
    public decimal? Eps { get; set; }
    public decimal? ReProLossBefTax { get; set; }
    public decimal? NetProLossAftTax { get; set; }
    public string? ReBroadcastTimestamp { get; set; }
}
