using Microsoft.EntityFrameworkCore;

namespace Stock.Repository;

public class FavoritesRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<int> AddFavoriteSymbolAsync(string symbol, string companyName, string assetType = "stock")
    {
        await using var context = _contextFactory.CreateDbContext();
        symbol = symbol.Trim().ToUpperInvariant();
        assetType = assetType.Trim().ToLowerInvariant();

        if (await context.FavoriteSymbolEntities.AnyAsync(x => x.AssetType == assetType && x.Symbol == symbol))
        {
            return 0;
        }

        var entity = new FavoriteSymbolEntity
        {
            Symbol = symbol,
            CompanyName = companyName,
            AssetType = assetType,
            AddedAt = DateTime.UtcNow,
        };

        await context.FavoriteSymbolEntities.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> RemoveFavoriteSymbolAsync(string symbol, string assetType = "stock")
    {
        await using var context = _contextFactory.CreateDbContext();
        symbol = symbol.Trim().ToUpperInvariant();
        assetType = assetType.Trim().ToLowerInvariant();
        return await context.FavoriteSymbolEntities
            .Where(x => x.AssetType == assetType && x.Symbol == symbol)
            .ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyList<FavoriteSymbolEntity>> GetFavoriteSymbolsAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.FavoriteSymbolEntities.AsNoTracking().OrderByDescending(x => x.AddedAt).ToListAsync();
    }
}
