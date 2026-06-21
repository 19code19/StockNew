using System.Text.Json.Serialization;

namespace Stock.Model
{
    // Model
    public class CorpAnnouncement
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public string Desc { get; set; } = string.Empty;

        [JsonPropertyName("dt")]
        public string Dt { get; set; } = string.Empty;

        [JsonPropertyName("attchmntFile")]
        public string? AttchmntFile { get; set; }

        [JsonPropertyName("sm_name")]
        public string SmName { get; set; } = string.Empty;

        [JsonPropertyName("sm_isin")]
        public string SmIsin { get; set; } = string.Empty;

        [JsonPropertyName("an_dt")]
        public string AnDt { get; set; } = string.Empty;

        [JsonPropertyName("sort_date")]
        public string SortDate { get; set; } = string.Empty;

        [JsonPropertyName("seq_id")]
        public string? SeqId { get; set; }

        [JsonPropertyName("smIndustry")]
        public string SmIndustry { get; set; } = string.Empty;

        [JsonPropertyName("orgid")]
        public string? Orgid { get; set; }

        [JsonPropertyName("attchmntText")]
        public string? AttchmntText { get; set; }

        [JsonPropertyName("bflag")]
        public string? Bflag { get; set; }

        [JsonPropertyName("old_new")]
        public string? OldNew { get; set; }

        [JsonPropertyName("csvName")]
        public string? CsvName { get; set; }

        [JsonPropertyName("exchdisstime")]
        public string ExchDissTime { get; set; } = string.Empty;

        [JsonPropertyName("difference")]
        public string Difference { get; set; } = string.Empty;

        [JsonPropertyName("fileSize")]
        public string? FileSize { get; set; }

        [JsonPropertyName("attFileSize")]
        public string? AttFileSize { get; set; }

        [JsonPropertyName("hasXbrl")]
        public bool HasXbrl { get; set; }
    }

    public class CorpAnnouncementResult
    {
        public string Symbol { get; set; } = string.Empty;
        public List<CorpAnnouncement>? Data { get; set; }
    }
}