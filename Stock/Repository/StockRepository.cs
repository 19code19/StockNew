namespace Stock.Repository;

public class StockRepository(IDbContextFactory<StockDbContext> contextFactory)
{
    private readonly IDbContextFactory<StockDbContext> _contextFactory = contextFactory;

    public async Task<IReadOnlyList<string>> GetAllSymbolsAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.EquityListings
            .AsNoTracking()
            .Select(x => x.Symbol)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetAllSymbolsSeriesAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.EquityListings
            .AsNoTracking()
            .Select(x => x.Symbol + x.Series + "N")
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<int> SaveEquityListingsAsync(IEnumerable<Stock.Entity.EquityListing> listings)
    {
        await using var context = _contextFactory.CreateDbContext();
        var entities = listings.Select(x => Mapper.ToEntity<Entity.EquityListing, EquityListingEntity>(x)).ToList();

        // Replace bulk delete with tracked remove + add so EF handles the diff via change tracking.
        var existing = await context.EquityListings.ToListAsync();
        context.EquityListings.RemoveRange(existing);
        await context.EquityListings.AddRangeAsync(entities);

        return await context.SaveChangesAsync();
    }

    public async Task<SymbolDataResponse?> SaveSymbolDataAsync(SymbolDataResponse response)
    {
        if (response is null)
        {
            return null;
        }

        await using var context = _contextFactory.CreateDbContext();

        var existing = await context.SymbolDataEntities
            .Include(x => x.EquityResponse)
            .FirstOrDefaultAsync(x => x.Symbol == response.Symbol);

        if (existing is not null)
        {
            context.SymbolDataEntities.Remove(existing); // requires cascade delete on EquityResponse FK
        }

        // Single AutoMapper call now maps the entire graph, including EquityResponse and its children
        var entity = Mapper.ToEntity<SymbolDataResponse, SymbolDataEntity>(response);

        // IndexList -> JSON is custom logic AutoMapper won't do on its own; fix up after mapping
        for (int i = 0; i < response.EquityResponse.Count; i++)
        {
            var srcSecInfo = response.EquityResponse[i]?.SecInfo;
            if (srcSecInfo?.IndexList is not null && srcSecInfo.IndexList.Count != 0)
            {
                entity.EquityResponse.ElementAt(i).SecInfo!.IndexListJson =
                    System.Text.Json.JsonSerializer.Serialize(srcSecInfo.IndexList);
            }
        }

        await context.SymbolDataEntities.AddAsync(entity);
        await context.SaveChangesAsync();
        return response;
    }

    public async Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Entity.YearwiseData> data, string symbol)
    {
        await using var context = _contextFactory.CreateDbContext();

        var existing = await context.YearwiseDataEntities
            .Where(x => x.Symbol == symbol)
            .ToListAsync();
        context.YearwiseDataEntities.RemoveRange(existing);

        var entities = data.Select(x => Mapper.ToEntity<Stock.Entity.YearwiseData, YearwiseDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        await context.YearwiseDataEntities.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }
}