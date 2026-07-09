namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiController(AiService aiService) : ControllerBase
{
    private readonly AiService _aiService = aiService;

    [HttpGet("recommendations/view")]
    public async Task<ActionResult<IEnumerable<AiRecommendationViewEntity>>> GetRecommendationViews([FromQuery] string? assetType = null)
    {
        var rows = await _aiService.GetRecommendationViewsAsync(assetType);
        return Ok(rows);
    }

    [HttpGet("format")]
    public async Task<IActionResult> GetFormat()
    {
        return Ok( new AiRecommendationDto());
    }

    [HttpPost("recommendations/{isDelete}")]
    public async Task<ActionResult> SaveRecommendations([FromBody] IEnumerable<AiRecommendationDto> recommendations, [FromRoute] bool isDelete = false)
    {
        try
        {
            var savedCount = await _aiService.SaveRecommendationsAsync(recommendations, isDelete);
            return CreatedAtAction(nameof(GetRecommendationViews), new { count = savedCount }, new { saved = savedCount });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("mf/recommendations/{isDelete}")]
    public async Task<ActionResult> SaveMFRecommendations([FromBody] IEnumerable<AiRecommendationDto> recommendations, [FromRoute] bool isDelete = false)
    {
        try
        {
            var savedCount = await _aiService.SaveMFRecommendationsAsync(recommendations, isDelete);
            return CreatedAtAction(nameof(GetRecommendationViews), new { count = savedCount }, new { saved = savedCount });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
