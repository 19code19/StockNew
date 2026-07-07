using System.ComponentModel.DataAnnotations;

namespace Stock.Entity
{
    public class AiRecommendationMFEntity : AiRecommendationBase
    {
        [Key]
        public int Id { get; set; }
    }
}
