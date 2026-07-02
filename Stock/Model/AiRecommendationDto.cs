namespace Stock.Model;

public class AiRecommendationDto
{
    public int Rank { get; set; }

    public string Symbol { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Reason { get; set; } = string.Empty;
}
