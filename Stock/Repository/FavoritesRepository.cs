using Microsoft.EntityFrameworkCore;

namespace Stock.Repository;

public class FavoritesRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<int> AddFavoriteSymbolAsync(string symbol, string companyName)
    {
        await using var context = _contextFactory.CreateDbContext();
        if (await context.FavoriteSymbolEntities.AnyAsync(x => x.Symbol == symbol))
        {
            return 0;
        }

        var entity = new FavoriteSymbolEntity
        {
            Symbol = symbol,
            CompanyName = companyName,
            AddedAt = DateTime.UtcNow,
        };

        await context.FavoriteSymbolEntities.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> RemoveFavoriteSymbolAsync(string symbol)
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.FavoriteSymbolEntities.Where(x => x.Symbol == symbol).ExecuteDeleteAsync();
    }

    public async Task<IReadOnlyList<FavoriteSymbolEntity>> GetFavoriteSymbolsAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.FavoriteSymbolEntities.AsNoTracking().OrderByDescending(x => x.AddedAt).ToListAsync();
    }
}
