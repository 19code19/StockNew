namespace Stock.Repository;

public class AiRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<int> SaveAiRecommendationsAsync(IEnumerable<AiRecommendationEntity> recommendations)
    {
        await using var context = _contextFactory.CreateDbContext();
        await context.AiRecommendations.ExecuteDeleteAsync();
        await context.AiRecommendations.AddRangeAsync(recommendations);
        return await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<AiRecommendationViewEntity>> GetAiRecommendationViewsAsync(string? assetType = null)
    {
        await using var context = _contextFactory.CreateDbContext();

        var query = context.AiRecommendations.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(assetType))
        {
            var normalized = assetType.Trim().ToLowerInvariant();
            query = query.Where(x => x.AssetType == normalized);
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
