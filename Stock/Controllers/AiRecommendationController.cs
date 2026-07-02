using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stock.Data;
using Stock.Model;
using Stock.Repository;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiRecommendationController(IStockRepository stockRepository, IWebHostEnvironment environment, IMemoryCache memoryCache) : ControllerBase
{
    private const string RecommendationCacheKey = "ai-recommendation-rows";
    private const string RecommendationViewCacheKey = "ai-recommendation-view-rows";
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IMemoryCache _memoryCache = memoryCache;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AiRecommendationEntity>>> Get()
    {
        if (_memoryCache.TryGetValue(RecommendationCacheKey, out List<AiRecommendationEntity>? cachedRows) && cachedRows is not null)
        {
            return Ok(cachedRows);
        }

        var rows = await _stockRepository.GetAiRecommendationsAsync();
        _memoryCache.Set(RecommendationCacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });
        return Ok(rows);
    }

    [HttpGet("view")]
    public async Task<ActionResult<IEnumerable<AiRecommendationViewEntity>>> GetView()
    {
        if (_memoryCache.TryGetValue(RecommendationViewCacheKey, out List<AiRecommendationViewEntity>? cachedRows) && cachedRows is not null)
        {
            return Ok(cachedRows);
        }

        var rows = await _stockRepository.GetAiRecommendationViewsAsync();
        _memoryCache.Set(RecommendationViewCacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });
        return Ok(rows);
    }

    [HttpGet("format")]
    public IActionResult GetFormat()
    {
        var templatePath = Path.Combine(_environment.ContentRootPath, "AI", "Template", "Format.json");
        if (!System.IO.File.Exists(templatePath))
        {
            return NotFound(new { error = "Template file not found." });
        }

        var json = System.IO.File.ReadAllText(templatePath);
        return Content(json, "application/json");
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] IEnumerable<AiRecommendationDto> recommendations)
    {
        if (recommendations is null || !recommendations.Any())
        {
            return BadRequest("Request body must contain at least one recommendation.");
        }

        var entities = recommendations.Select(x => new AiRecommendationEntity
        {
            Rank = x.Rank,
            Symbol = x.Symbol,
            Category = x.Category,
            Score = x.Score,
            Source = x.Source,
            Reason = x.Reason,
            CreatedAt = DateTime.UtcNow,
        }).ToList();

        var savedCount = await _stockRepository.SaveAiRecommendationsAsync(entities);
        _memoryCache.Remove(RecommendationCacheKey);
        _memoryCache.Remove(RecommendationViewCacheKey);
        return CreatedAtAction(nameof(Get), new { count = savedCount }, new { saved = savedCount });
    }
}
