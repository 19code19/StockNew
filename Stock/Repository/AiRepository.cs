namespace Stock.Repository;

public class AiRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<int> SaveAiRecommendationsAsync(IEnumerable<AiRecommendationEntity> recommendations, bool isDelete)
    {
        await using var context = _contextFactory.CreateDbContext();

        if (isDelete)
            await context.AiRecommendations.ExecuteDeleteAsync();

        await context.AiRecommendations.AddRangeAsync(recommendations);
        return await context.SaveChangesAsync();
    }
    public async Task<int> SaveMFAiRecommendationsAsync(IEnumerable<AiRecommendationMFEntity> recommendations, bool isDelete)
    {
        await using var context = _contextFactory.CreateDbContext();

        if (isDelete)
            await context.AiRecommendationMFEntities.ExecuteDeleteAsync();

        await context.AiRecommendationMFEntities.AddRangeAsync(recommendations);
        return await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<AiRecommendationViewEntity>> GetAiRecommendationViewsAsync(string? assetType = null)
    {
        var normalizedAssetType = assetType?.Trim().ToLowerInvariant();

        await using var context = _contextFactory.CreateDbContext();

        return normalizedAssetType == "mutualfund"
            ? await GetMutualFundRecommendationsAsync(context)
            : await GetSecurityRecommendationsAsync(context, normalizedAssetType);
    }

    private static async Task<IReadOnlyList<AiRecommendationViewEntity>> GetMutualFundRecommendationsAsync(StockDbContext context)
    {
        return await context.AiRecommendationMFEntities
            .AsNoTracking()
            .OrderBy(x => x.Rank)
            .Select(x => new AiRecommendationViewEntity
            {
                Rank = x.Rank,
                Symbol = x.Symbol,
                Category = x.Category,
                Score = x.Score,
                Source = x.Source,
                Reason = x.Reason,
                CreatedAt = DateTime.Now,
                AssetType = x.AssetType
            })
            .ToListAsync();
    }

    private static async Task<IReadOnlyList<AiRecommendationViewEntity>> GetSecurityRecommendationsAsync(StockDbContext context, string? normalizedAssetType)
    {
        var query = context.AiRecommendations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(normalizedAssetType))
        {
            query = query.Where(x => x.AssetType == normalizedAssetType);
        }

        return await query
            .Include(x => x.MetaData)
            .Include(x => x.SecInfo)
            .OrderBy(x => x.Rank)
            .Select(x => new AiRecommendationViewEntity
            {
                Rank = x.Rank,
                Symbol = x.Symbol,
                Category = x.Category,
                Score = x.Score,
                Source = x.Source,
                Reason = x.Reason,
                CreatedAt = x.CreatedAt,
                AssetType = x.AssetType,
                CompanyName = x.MetaData == null ? string.Empty : x.MetaData.CompanyName,
                BasicIndustry = x.SecInfo == null ? string.Empty : x.SecInfo.BasicIndustry,
                IssueDesc = x.SecInfo == null ? null : x.SecInfo.IssueDesc,
                Macro = x.SecInfo == null ? string.Empty : x.SecInfo.Macro,
                Sector = x.SecInfo == null ? string.Empty : x.SecInfo.Sector,
                IndustryInfo = x.SecInfo == null ? string.Empty : x.SecInfo.IndustryInfo
            })
            .ToListAsync();
    }
}