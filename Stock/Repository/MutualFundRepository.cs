using Microsoft.EntityFrameworkCore;

namespace Stock.Repository;

public class MutualFundRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<int> SaveMutualFundSchemesAsync(IEnumerable<MutualFundSchemeEntity> schemes)
    {
        await using var context = _contextFactory.CreateDbContext();

        await context.MutualFundSchemes.ExecuteDeleteAsync();
        await context.MutualFundSchemes.AddRangeAsync(schemes);

        return await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<MutualFundSchemeEntity>> GetMutualFundSchemesAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.MutualFundSchemes.AsNoTracking().ToListAsync();
    }
}
