namespace Stock.Model;

public class MutualFundResponse
{
    [JsonPropertyName("content")]
    public List<MutualFundScheme> Content { get; set; } = new();

    [JsonPropertyName("total_results")]
    public int Total { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }
}

public class MutualFundScheme
{
    [JsonPropertyName("logo_url")] public string LogoUrl { get; set; } = string.Empty;
    [JsonPropertyName("sip_return1y")] public decimal? SipReturn1Y { get; set; }
    [JsonPropertyName("sip_return3y")] public decimal? SipReturn3Y { get; set; }
    [JsonPropertyName("sip_return5y")] public decimal? SipReturn5Y { get; set; }
    [JsonPropertyName("sip_return10y")] public decimal? SipReturn10Y { get; set; }
    [JsonPropertyName("scheme_type")] public string SchemeType { get; set; } = string.Empty;
    [JsonPropertyName("groww_verdict_score")] public int? GrowwVerdictScore { get; set; }
    [JsonPropertyName("return6m")] public decimal? Return6M { get; set; }
    [JsonPropertyName("expense_ratio")] public string ExpenseRatio { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string SchemeId { get; set; } = string.Empty;
    [JsonPropertyName("nav")] public decimal? Nav { get; set; }
    [JsonPropertyName("return1y")] public decimal? Return1Y { get; set; }
    [JsonPropertyName("return7y")] public decimal? Return7Y { get; set; }
    [JsonPropertyName("return5y")] public decimal? Return5Y { get; set; }
    [JsonPropertyName("return10y")] public decimal? Return10Y { get; set; }
    [JsonPropertyName("doc_required")] public bool DocRequired { get; set; }
    [JsonPropertyName("return1d")] public decimal? Return1D { get; set; }
    [JsonPropertyName("min_investment_amount")] public decimal? MinInvestmentAmount { get; set; }
    [JsonPropertyName("min_sip_investment")] public decimal? MinSipInvestment { get; set; }
    [JsonPropertyName("amc")] public string Amc { get; set; } = string.Empty;
    [JsonPropertyName("aum")] public decimal? Aum { get; set; }
    [JsonPropertyName("plan_type")] public string PlanType { get; set; } = string.Empty;
    [JsonPropertyName("fund_manager")] public string FundManager { get; set; } = string.Empty;
    [JsonPropertyName("sub_category")] public string SubCategory { get; set; } = string.Empty;
    [JsonPropertyName("sip_allowed")] public bool SipAllowed { get; set; }
    [JsonPropertyName("doc_type")] public string DocType { get; set; } = string.Empty;
    [JsonPropertyName("risk_rating")] public int? RiskRating { get; set; }
    [JsonPropertyName("lumpsum_allowed")] public bool LumpsumAllowed { get; set; }
    [JsonPropertyName("scheme_code")] public string SchemeCode { get; set; } = string.Empty;
    [JsonPropertyName("direct_scheme_name")] public string DirectSchemeName { get; set; } = string.Empty;
    [JsonPropertyName("fund_house")] public string FundHouse { get; set; } = string.Empty;
    [JsonPropertyName("return3y")] public decimal? Return3Y { get; set; }
    [JsonPropertyName("fund_name")] public string FundName { get; set; } = string.Empty;
    [JsonPropertyName("available_for_investment")] public int? AvailableForInvestment { get; set; }
    [JsonPropertyName("scheme_name")] public string SchemeName { get; set; } = string.Empty;
    [JsonPropertyName("direct_fund")] public string DirectFund { get; set; } = string.Empty;
    [JsonPropertyName("return3m")] public decimal? Return3M { get; set; }
    [JsonPropertyName("risk")] public string Risk { get; set; } = string.Empty;
    [JsonPropertyName("page_view")] public int? PageView { get; set; }
    [JsonPropertyName("launch_date")] public string LaunchDate { get; set; } = string.Empty;
    [JsonPropertyName("exit_load")] public string ExitLoad { get; set; } = string.Empty;
    [JsonPropertyName("category")] public string Category { get; set; } = string.Empty;
    [JsonPropertyName("sub_sub_category")] public List<string> SubSubCategory { get; set; } = new();
    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = new();
}
