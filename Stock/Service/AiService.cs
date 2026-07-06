using Microsoft.Extensions.Caching.Memory;

namespace Stock.Service;

public class AiService(AiRepository aiRepository, IWebHostEnvironment environment, IMemoryCache memoryCache)
{
    private const string RecommendationViewCacheKey = "ai-recommendation-view-rows";
    private readonly AiRepository _aiRepository = aiRepository;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<IReadOnlyList<AiRecommendationViewEntity>> GetRecommendationViewsAsync()
    {
        if (_memoryCache.TryGetValue(RecommendationViewCacheKey, out IReadOnlyList<AiRecommendationViewEntity>? cachedRows) && cachedRows is not null)
        {
            return cachedRows;
        }

        var rows = await _aiRepository.GetAiRecommendationViewsAsync();
        SetCachedRows(RecommendationViewCacheKey, rows);
        return rows;
    }

    public async Task<string?> GetFormatJsonAsync()
    {
        var templatePath = Path.Combine(_environment.ContentRootPath, "AI", "Template", "Format.json");
        if (!File.Exists(templatePath))
        {
            return null;
        }

        return await File.ReadAllTextAsync(templatePath);
    }

    public async Task<int> SaveRecommendationsAsync(IEnumerable<AiRecommendationDto>? recommendations)
    {
        if (recommendations is null)
        {
            throw new ArgumentException("Request body must contain at least one recommendation.", nameof(recommendations));
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

        if (entities.Count == 0)
        {
            throw new ArgumentException("Request body must contain at least one recommendation.", nameof(recommendations));
        }

        var savedCount = await _aiRepository.SaveAiRecommendationsAsync(entities);
        _memoryCache.Remove(RecommendationViewCacheKey);
        return savedCount;
    }

    private void SetCachedRows<T>(string cacheKey, IReadOnlyList<T> rows)
    {
        _memoryCache.Set(cacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });
    }
}
