using System.ComponentModel.DataAnnotations;

namespace Stock.Entity;

public class AiRecommendationEntity
{
    [Key]
    public int Id { get; set; }

    public int Rank { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string AssetType { get; set; } = "stock";

    public string Category { get; set; } = string.Empty;

    public decimal Score { get; set; }
    public string Source { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual MetaDataEntity? MetaData { get; set; }

    public virtual SecInfoEntity? SecInfo { get; set; }
}
