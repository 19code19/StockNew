using System.Text.Json.Serialization;

namespace Stock.Model
{
    public class CorpEventCalendar
    {
        [JsonPropertyName("bm_symbol")]
        public string BmSymbol { get; set; } = string.Empty;

        [JsonPropertyName("bm_date")]
        public string BmDate { get; set; } = string.Empty;

        [JsonPropertyName("bm_purpose")]
        public string BmPurpose { get; set; } = string.Empty;

        [JsonPropertyName("bm_desc")]
        public string BmDesc { get; set; } = string.Empty;

        [JsonPropertyName("sm_indusrty")]
        public string SmIndustry { get; set; } = string.Empty;

        [JsonPropertyName("bm_timestamp")]
        public string BmTimestamp { get; set; } = string.Empty;

        [JsonPropertyName("sm_name")]
        public string SmName { get; set; } = string.Empty;

        [JsonPropertyName("sm_isin")]
        public string SmIsin { get; set; } = string.Empty;

        [JsonPropertyName("bm_dt")]
        public string BmDt { get; set; } = string.Empty;

        [JsonPropertyName("bm_timestamp_full")]
        public string BmTimestampFull { get; set; } = string.Empty;

        [JsonPropertyName("bm_an_seq_id")]
        public string BmAnSeqId { get; set; } = string.Empty;

        [JsonPropertyName("bm_attachment")]
        public string? BmAttachment { get; set; }
    }

    public class CorpEventCalendarResult
    {
        public string Symbol { get; set; } = string.Empty;
        public List<CorpEventCalendar>? Data { get; set; }
    }
}
