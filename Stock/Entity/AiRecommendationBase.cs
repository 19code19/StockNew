namespace Stock.Entity
{
    public class AiRecommendationBase
    {
        public int Rank { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string AssetType { get; set; } = "stock";

        public string Category { get; set; } = string.Empty;

        public decimal Score { get; set; }
        public string Source { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
