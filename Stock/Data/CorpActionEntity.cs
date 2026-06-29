namespace Stock.Data;

public class CorpActionEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;
    public string? Date { get; set; }
    public string? RecordDate { get; set; }
    public string Series { get; set; } = string.Empty;
    public string Ind { get; set; } = string.Empty;
    public string FaceVal { get; set; } = string.Empty;
    public string ExDate { get; set; } = string.Empty;
    public string RecDate { get; set; } = string.Empty;
    public string BcStartDate { get; set; } = string.Empty;
    public string BcEndDate { get; set; } = string.Empty;
    public string NdStartDate { get; set; } = string.Empty;
    public string Comp { get; set; } = string.Empty;
    public string Isin { get; set; } = string.Empty;
    public string NdEndDate { get; set; } = string.Empty;
    public string? CaBroadcastDate { get; set; }
}
