using System.Text.Json.Serialization;

namespace Stock.Model
{
    public class IndicesResponse
    {
        [JsonPropertyName("data")]
        public List<IndexData> Data { get; set; } = new();

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonPropertyName("advances")]
        public int Advances { get; set; }

        [JsonPropertyName("declines")]
        public int Declines { get; set; }

        [JsonPropertyName("unchanged")]
        public int Unchanged { get; set; }
    }

    public class IndexData
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;

        [JsonPropertyName("indexSymbol")]
        public string IndexSymbol { get; set; } = string.Empty;

        [JsonPropertyName("last")]
        public double Last { get; set; }

        [JsonPropertyName("variation")]
        public double Variation { get; set; }

        [JsonPropertyName("percentChange")]
        public double PercentChange { get; set; }

        [JsonPropertyName("open")]
        public double Open { get; set; }

        [JsonPropertyName("high")]
        public double High { get; set; }

        [JsonPropertyName("low")]
        public double Low { get; set; }

        [JsonPropertyName("previousClose")]
        public double PreviousClose { get; set; }

        [JsonPropertyName("yearHigh")]
        public double YearHigh { get; set; }

        [JsonPropertyName("yearLow")]
        public double YearLow { get; set; }

        [JsonPropertyName("pe")]
        public string PE { get; set; } = string.Empty;

        [JsonPropertyName("pb")]
        public string PB { get; set; } = string.Empty;

        [JsonPropertyName("dy")]
        public string DY { get; set; } = string.Empty;

        [JsonPropertyName("advances")]
        public string? Advances { get; set; }

        [JsonPropertyName("declines")]
        public string? Declines { get; set; }

        [JsonPropertyName("unchanged")]
        public string? Unchanged { get; set; }

        [JsonPropertyName("perChange365d")]
        public double PerChange365d { get; set; }

        [JsonPropertyName("perChange30d")]
        public double PerChange30d { get; set; }

        [JsonPropertyName("chartTodayPath")]
        public string? ChartTodayPath { get; set; }
    }
}
