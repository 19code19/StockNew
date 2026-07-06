using Microsoft.EntityFrameworkCore;

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

        await context.SymbolDataEntities
            .Where(x => x.Symbol == response.Symbol)
            .ExecuteDeleteAsync();

        context.ChangeTracker.Clear();

        var symbol = response.Symbol;
        if (string.IsNullOrWhiteSpace(symbol) || response.EquityResponse.Count == 0)
        {
            return null;
        }

        var firstEquity = response.EquityResponse[0];

        var entity = new SymbolDataEntity
        {
            Symbol = symbol,
            Series = response.Series,
            MarketType = response.MarketType,
            OrderBook = firstEquity.OrderBook is not null ? Mapper.ToEntity<OrderBook, OrderBookEntity>(firstEquity.OrderBook) : null,
            MetaData = firstEquity.MetaData is not null ? Mapper.ToEntity<MetaData, MetaDataEntity>(firstEquity.MetaData) : null,
            PriceInfo = firstEquity.PriceInfo is not null ? Mapper.ToEntity<PriceInfo, PriceInfoEntity>(firstEquity.PriceInfo) : null,
            SecInfo = firstEquity.SecInfo is not null ? Mapper.ToEntity<SecInfo, SecInfoEntity>(firstEquity.SecInfo) : null,
            TradeInfo = firstEquity.TradeInfo is not null ? Mapper.ToEntity<TradeInfo, TradeInfoEntity>(firstEquity.TradeInfo) : null,
            LastUpdateTime = DateTime.Now
        };

        entity.OrderBook?.Symbol = symbol;
        entity.MetaData?.Symbol = symbol;
        entity.PriceInfo?.Symbol = symbol;
        entity.SecInfo?.Symbol = symbol;
        entity.TradeInfo?.Symbol = symbol;

        await context.SymbolDataEntities.AddAsync(entity);
        await context.SaveChangesAsync();
        return response;
    }

    public async Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Entity.YearwiseData> data, string symbol)
    {
        await using var context = _contextFactory.CreateDbContext();

        // Delete existing yearwise rows for this symbol before inserting refreshed data.
        // This is a direct delete on the YearwiseDataEntities table, so it does not rely on
        // cascade behavior in the model for the symbol-level reference tables.
        await context.YearwiseDataEntities
            .Where(x => x.Symbol == symbol)
            .ExecuteDeleteAsync();

        var entities = data.Select(x => Mapper.ToEntity<Stock.Entity.YearwiseData, YearwiseDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        await context.YearwiseDataEntities.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }

    public async Task<List<YearwiseStockSummaryEntity>> GetYearwiseSummaryAsync()
    {
        await using var context = _contextFactory.CreateDbContext();

        return await (from y in context.YearwiseDataEntities
                      join t in context.TradeInfoEntities on y.Symbol equals t.Symbol into tJoin
                      from t in tJoin.DefaultIfEmpty()
                      join p in context.PriceInfoEntities on y.Symbol equals p.Symbol into pJoin
                      from p in pJoin.DefaultIfEmpty()
                      join s in context.SecInfoEntities on y.Symbol equals s.Symbol into sJoin
                      from s in sJoin.DefaultIfEmpty()
                      join m in context.MetaDataEntities on y.Symbol equals m.Symbol into mJoin
                      from m in mJoin.DefaultIfEmpty()
                      select new YearwiseStockSummaryEntity
                      {
                          YesterdayChangePercent = y.YesterdayChangePercent,
                          OneWeekChangePercent = y.OneWeekChangePercent,
                          OneMonthChangePercent = y.OneMonthChangePercent,
                          ThreeMonthChangePercent = y.ThreeMonthChangePercent,
                          SixMonthChangePercent = y.SixMonthChangePercent,
                          OneYearChangePercent = y.OneYearChangePercent,
                          TwoYearChangePercent = y.TwoYearChangePercent,
                          ThreeYearChangePercent = y.ThreeYearChangePercent,
                          FiveYearChangePercent = y.FiveYearChangePercent,
                          IndexYesterdayChangePercent = y.IndexYesterdayChangePercent,
                          IndexOneWeekChangePercent = y.IndexOneWeekChangePercent,
                          IndexOneMonthChangePercent = y.IndexOneMonthChangePercent,
                          IndexThreeMonthChangePercent = y.IndexThreeMonthChangePercent,
                          IndexSixMonthChangePercent = y.IndexSixMonthChangePercent,
                          IndexOneYearChangePercent = y.IndexOneYearChangePercent,
                          IndexTwoYearChangePercent = y.IndexTwoYearChangePercent,
                          IndexThreeYearChangePercent = y.IndexThreeYearChangePercent,
                          IndexFiveYearChangePercent = y.IndexFiveYearChangePercent,
                          IndexName = y.IndexName,
                          TotalTradedVolume = t == null ? 0L : t.TotalTradedVolume,
                          TotalTradedValue = t == null ? 0m : t.TotalTradedValue,
                          QuantityTraded = t == null ? 0L : t.QuantityTraded,
                          DeliveryQuantity = t == null ? 0L : t.DeliveryQuantity,
                          TotalMarketCap = t == null ? 0m : t.TotalMarketCap,
                          BasicIndustry = s == null ? string.Empty : s.BasicIndustry,
                          IsSuspended = s == null ? string.Empty : s.IsSuspended,
                          Macro = s == null ? string.Empty : s.Macro,
                          Sector = s == null ? string.Empty : s.Sector,
                          IndustryInfo = s == null ? string.Empty : s.IndustryInfo,
                          CompanyName = m == null ? string.Empty : m.CompanyName,
                          Symbol = y.Symbol
                      })
                      .AsNoTracking()
                      .ToListAsync();
    }
}
