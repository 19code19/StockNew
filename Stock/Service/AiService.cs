using Microsoft.Extensions.Caching.Memory;

namespace Stock.Service;

public class AiService(AiRepository aiRepository, IWebHostEnvironment environment, IMemoryCache memoryCache)
{
    private const string RecommendationViewCacheKey = "ai-recommendation-view-rows";
    private readonly AiRepository _aiRepository = aiRepository;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<IReadOnlyList<AiRecommendationViewEntity>> GetRecommendationViewsAsync(string? assetType = null)
    {
        if (string.IsNullOrWhiteSpace(assetType))
        {
            if (_memoryCache.TryGetValue(RecommendationViewCacheKey, out IReadOnlyList<AiRecommendationViewEntity>? cachedRows) && cachedRows is not null)
            {
                return cachedRows;
            }

            var rows = await _aiRepository.GetAiRecommendationViewsAsync(assetType);
            SetCachedRows(RecommendationViewCacheKey, rows);
            return rows;
        }

        if (_memoryCache.TryGetValue(RecommendationViewCacheKey, out IReadOnlyList<AiRecommendationViewEntity>? cachedRowsWithFilter) && cachedRowsWithFilter is not null)
        {
            var normalized = assetType.Trim().ToLowerInvariant();
            return cachedRowsWithFilter.Where(x => x.AssetType == normalized).ToList();
        }

        return await _aiRepository.GetAiRecommendationViewsAsync(assetType);
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
            AssetType = string.IsNullOrWhiteSpace(x.AssetType) ? "stock" : x.AssetType.Trim().ToLowerInvariant(),
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

    public async Task<int> SaveMFRecommendationsAsync(IEnumerable<AiRecommendationDto>? recommendations)
    {
        if (recommendations is null)
        {
            throw new ArgumentException("Request body must contain at least one recommendation.", nameof(recommendations));
        }

        var entities = recommendations.Select(x => new AiRecommendationMFEntity
        {
            Rank = x.Rank,
            Symbol = x.Symbol,
            AssetType = string.IsNullOrWhiteSpace(x.AssetType) ? "stock" : x.AssetType.Trim().ToLowerInvariant(),
            Category = x.Category,
            Score = x.Score,
            Source = x.Source,
            Reason = x.Reason
        }).ToList();

        if (entities.Count == 0)
        {
            throw new ArgumentException("Request body must contain at least one recommendation.", nameof(recommendations));
        }

        var savedCount = await _aiRepository.SaveMFAiRecommendationsAsync(entities);
        _memoryCache.Remove(RecommendationViewCacheKey);
        return savedCount;
    }

    private void SetCachedRows<T>(string cacheKey, IReadOnlyList<T> rows)
    {
        _memoryCache.Set(cacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
            SlidingExpiration = TimeSpan.FromHours(1),
        });
    }
}
