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
        var json = await _aiService.GetFormatJsonAsync();
        if (json is null)
        {
            return NotFound(new { error = "Template file not found." });
        }

        return Content(json, "application/json");
    }

    [HttpPost("recommendations")]
    public async Task<ActionResult> SaveRecommendations([FromBody] IEnumerable<AiRecommendationDto> recommendations)
    {
        try
        {
            var savedCount = await _aiService.SaveRecommendationsAsync(recommendations);
            return CreatedAtAction(nameof(GetRecommendationViews), new { count = savedCount }, new { saved = savedCount });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
