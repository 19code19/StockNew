using System.Text.Json.Serialization;

namespace Stock.Model
{
    public class YearwiseDataBatchResult
    {
        public string Symbol { get; set; } = string.Empty;
        public List<YearwiseData>? Data { get; set; }
    }
    public class YearwiseData
    {
        [JsonPropertyName("yesterday_chng_per")]
        public decimal YesterdayChangePercent { get; set; }

        [JsonPropertyName("one_week_chng_per")]
        public decimal OneWeekChangePercent { get; set; }

        [JsonPropertyName("one_month_chng_per")]
        public decimal OneMonthChangePercent { get; set; }

        [JsonPropertyName("three_month_chng_per")]
        public decimal ThreeMonthChangePercent { get; set; }

        [JsonPropertyName("six_month_chng_per")]
        public decimal SixMonthChangePercent { get; set; }

        [JsonPropertyName("one_year_chng_per")]
        public decimal OneYearChangePercent { get; set; }

        [JsonPropertyName("two_year_chng_per")]
        public decimal TwoYearChangePercent { get; set; }

        [JsonPropertyName("three_year_chng_per")]
        public decimal ThreeYearChangePercent { get; set; }

        [JsonPropertyName("five_year_chng_per")]
        public decimal FiveYearChangePercent { get; set; }

        [JsonPropertyName("one_week_date")]
        public string OneWeekDate { get; set; } = string.Empty;

        [JsonPropertyName("index_yesterday_chng_per")]
        public decimal IndexYesterdayChangePercent { get; set; }

        [JsonPropertyName("index_one_week_chng_per")]
        public decimal IndexOneWeekChangePercent { get; set; }

        [JsonPropertyName("index_one_month_chng_per")]
        public decimal IndexOneMonthChangePercent { get; set; }

        [JsonPropertyName("index_three_month_chng_per")]
        public decimal IndexThreeMonthChangePercent { get; set; }

        [JsonPropertyName("index_six_month_chng_per")]
        public decimal IndexSixMonthChangePercent { get; set; }

        [JsonPropertyName("index_one_year_chng_per")]
        public decimal IndexOneYearChangePercent { get; set; }

        [JsonPropertyName("index_two_year_chng_per")]
        public decimal IndexTwoYearChangePercent { get; set; }

        [JsonPropertyName("index_three_year_chng_per")]
        public decimal IndexThreeYearChangePercent { get; set; }

        [JsonPropertyName("index_five_year_chng_per")]
        public decimal IndexFiveYearChangePercent { get; set; }

        [JsonPropertyName("index_one_week_date")]
        public string IndexOneWeekDate { get; set; } = string.Empty;

        [JsonPropertyName("index_name")]
        public string IndexName { get; set; } = string.Empty;
    }
}