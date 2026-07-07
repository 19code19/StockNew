using System.ComponentModel.DataAnnotations;

namespace Stock.Entity;

public class AiRecommendationEntity : AiRecommendationBase
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual MetaDataEntity? MetaData { get; set; }

    public virtual SecInfoEntity? SecInfo { get; set; }
}
