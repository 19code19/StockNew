using System.Text.Json.Serialization;

namespace Stock.Model
{
    public class FinancialStatus
    {
        [JsonPropertyName("from_date")]
        public string? FromDate { get; set; }

        [JsonPropertyName("to_date")]
        public string? ToDate { get; set; }

        [JsonPropertyName("to_date_MonYr")]
        public string? ToDateMonYr { get; set; }

        [JsonPropertyName("series")]
        public string? Series { get; set; }

        [JsonPropertyName("expenditure")]
        public string? Expenditure { get; set; }

        [JsonPropertyName("totalIncome")]
        public decimal? TotalIncome { get; set; }

        [JsonPropertyName("audited")]
        public string? Audited { get; set; }

        [JsonPropertyName("cumulative")]
        public string? Cumulative { get; set; }

        [JsonPropertyName("consolidated")]
        public string? Consolidated { get; set; }

        [JsonPropertyName("eps")]
        public decimal? Eps { get; set; }

        [JsonPropertyName("reProLossBefTax")]
        public decimal? ReProLossBefTax { get; set; }

        [JsonPropertyName("netProLossAftTax")]
        public decimal? NetProLossAftTax { get; set; }

        [JsonPropertyName("re_broadcast_timestamp")]
        public string? ReBroadcastTimestamp { get; set; }
    }

    public class FinancialStatusResult
    {
        public string Symbol { get; set; } = string.Empty;
        public List<FinancialStatus>? Data { get; set; }
    }
}
