using Microsoft.EntityFrameworkCore;

namespace Stock.Entity;

[Keyless]
public class AiRecommendationViewEntity
{
    public int Rank { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string AssetType { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Source { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string BasicIndustry { get; set; } = string.Empty;

    public string? IssueDesc { get; set; }

    public string Macro { get; set; } = string.Empty;

    public string Sector { get; set; } = string.Empty;

    public string IndustryInfo { get; set; } = string.Empty;
}
