namespace Stock.Entity;

public class MutualFundSchemeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SchemeId { get; set; } = string.Empty;
    public string SchemeName { get; set; } = string.Empty;
    public string FundHouse { get; set; } = string.Empty;
    public string FundName { get; set; } = string.Empty;
    public string DirectFund { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SubCategory { get; set; } = string.Empty;
    public string PlanType { get; set; } = string.Empty;
    public string SchemeCode { get; set; } = string.Empty;
    public string SchemeType { get; set; } = string.Empty;
    public int? GrowwVerdictScore { get; set; }
    public decimal? Nav { get; set; }
    public decimal? Return1D { get; set; }
    public decimal? Return1Y { get; set; }
    public decimal? Return3M { get; set; }
    public decimal? Return3Y { get; set; }
    public decimal? Return5Y { get; set; }
    public decimal? Return6M { get; set; }
    public decimal? Return7Y { get; set; }
    public decimal? Return10Y { get; set; }
    public decimal? SipReturn1Y { get; set; }
    public decimal? SipReturn3Y { get; set; }
    public decimal? SipReturn5Y { get; set; }
    public decimal? SipReturn10Y { get; set; }
    public bool DocRequired { get; set; }
    public decimal? MinInvestmentAmount { get; set; }
    public decimal? MinSipInvestment { get; set; }
    public string Amc { get; set; } = string.Empty;
    public string FundManager { get; set; } = string.Empty;
    public string DirectSchemeName { get; set; } = string.Empty;
    public string DocType { get; set; } = string.Empty;
    public int? PageView { get; set; }
    public string SubSubCategory { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public decimal? Aum { get; set; }
    public string ExpenseRatio { get; set; } = string.Empty;
    public string Risk { get; set; } = string.Empty;
    public int? RiskRating { get; set; }
    public bool AvailableForInvestment { get; set; }
    public bool SipAllowed { get; set; }
    public bool LumpsumAllowed { get; set; }
    public string LaunchDate { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string ExitLoad { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}
