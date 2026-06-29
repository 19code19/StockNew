using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Helpers;

namespace Stock.Repository;

public class StockRepository(IDbContextFactory<StockDbContext> contextFactory) : IStockRepository
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
        var entities = listings.Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Entity.EquityListing, EquityListingEntity>(x)).ToList();
        context.EquityListings.RemoveRange(context.EquityListings);
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

        var entity = Stock.Helpers.Mapper.ToEntity<SymbolDataResponse, SymbolDataEntity>(response);
        entity.Symbol = response.Symbol;
        entity.Series = response.Series;
        entity.MarketType = response.MarketType;

        context.SymbolDataEntities.RemoveRange(context.SymbolDataEntities.Where(x => x.Symbol == response.Symbol));
        await context.SymbolDataEntities.AddAsync(entity);

        context.OrderBookEntities.RemoveRange(context.OrderBookEntities.Where(x => x.Symbol == response.Symbol));
        context.MetaDataEntities.RemoveRange(context.MetaDataEntities.Where(x => x.Symbol == response.Symbol));
        context.TradeInfoEntities.RemoveRange(context.TradeInfoEntities.Where(x => x.Symbol == response.Symbol));
        context.PriceInfoEntities.RemoveRange(context.PriceInfoEntities.Where(x => x.Symbol == response.Symbol));
        context.SecInfoEntities.RemoveRange(context.SecInfoEntities.Where(x => x.Symbol == response.Symbol));

        foreach (var equity in response.EquityResponse)
        {
            if (equity.OrderBook is not null)
            {
                var ob = Stock.Helpers.Mapper.ToEntity<Stock.Model.OrderBook, OrderBookEntity>(equity.OrderBook);
                ob.Symbol = response.Symbol;
                await context.OrderBookEntities.AddAsync(ob);
            }

            if (equity.MetaData is not null)
            {
                var md = Stock.Helpers.Mapper.ToEntity<Stock.Model.MetaData, MetaDataEntity>(equity.MetaData);
                md.Symbol = response.Symbol;
                await context.MetaDataEntities.AddAsync(md);
            }

            if (equity.TradeInfo is not null)
            {
                var ti = Stock.Helpers.Mapper.ToEntity<Stock.Model.TradeInfo, TradeInfoEntity>(equity.TradeInfo);
                ti.Symbol = response.Symbol;
                await context.TradeInfoEntities.AddAsync(ti);
            }

            if (equity.PriceInfo is not null)
            {
                var pi = Stock.Helpers.Mapper.ToEntity<Stock.Model.PriceInfo, PriceInfoEntity>(equity.PriceInfo);
                pi.Symbol = response.Symbol;
                await context.PriceInfoEntities.AddAsync(pi);
            }

            if (equity.SecInfo is not null)
            {
                var si = Stock.Helpers.Mapper.ToEntity<Stock.Model.SecInfo, SecInfoEntity>(equity.SecInfo);
                si.Symbol = response.Symbol;
                if (equity.SecInfo.IndexList is not null && equity.SecInfo.IndexList.Any())
                {
                    si.IndexListJson = System.Text.Json.JsonSerializer.Serialize(equity.SecInfo.IndexList);
                }

                await context.SecInfoEntities.AddAsync(si);
            }
        }

        await context.SaveChangesAsync();
        return response;
    }

    public async Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Entity.YearwiseData> data, string symbol)
    {
        await using var context = _contextFactory.CreateDbContext();
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Entity.YearwiseData, YearwiseDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        context.YearwiseDataEntities.RemoveRange(context.YearwiseDataEntities.Where(x => x.Symbol == symbol));
        await context.YearwiseDataEntities.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }

    public async Task<int> SaveHistoricalTradeDataAsync(IEnumerable<HistoricalTradeData> data, string symbol)
    {
        await using var context = _contextFactory.CreateDbContext();
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<HistoricalTradeData, HistoricalTradeDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        context.HistoricalTradeDataEntities.RemoveRange(context.HistoricalTradeDataEntities.Where(x => x.Symbol == symbol));
        await context.HistoricalTradeDataEntities.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }

    public async Task<int> SaveIndexDataAsync(IEnumerable<IndexData> data)
    {
        await using var context = _contextFactory.CreateDbContext();
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<IndexData, IndexDataEntity>(x)).ToList();
        context.IndexDataEntities.RemoveRange(context.IndexDataEntities);
        await context.IndexDataEntities.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }

    public async Task<int> SaveAllIndicesAsync(IEnumerable<IndicesData> data)
    {
        await using var context = _contextFactory.CreateDbContext();
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<IndicesData, AllIndicesEntity>(x)).ToList();
        context.AllIndices.RemoveRange(context.AllIndices);
        await context.AllIndices.AddRangeAsync(entities);
        return await context.SaveChangesAsync();
    }

    public async Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, ShareholdingPatternEntry>? data)
    {
        if (data is null)
        {
            return 0;
        }

        await using var context = _contextFactory.CreateDbContext();
        context.ShareholdingPatternEntries.RemoveRange(context.ShareholdingPatternEntries.Where(x => x.Symbol == symbol));
        foreach (var item in data)
        {
            var entity = Stock.Helpers.Mapper.ToEntity<ShareholdingPatternEntry, ShareholdingPatternEntryEntity>(item.Value);
            entity.Symbol = symbol;
            entity.CategoryName = item.Key;
            await context.ShareholdingPatternEntries.AddAsync(entity);
        }

        return await context.SaveChangesAsync();
    }

    public async Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<PeerComparisonData>? data)
    {
        if (data is null)
        {
            return 0;
        }

        await using var context = _contextFactory.CreateDbContext();
        context.PeerComparisonDataEntities.RemoveRange(context.PeerComparisonDataEntities.Where(x => x.Symbol == symbol && x.Quarter == quarter));
        foreach (var item in data)
        {
            var entity = Stock.Helpers.Mapper.ToEntity<PeerComparisonData, PeerComparisonDataEntity>(item);
            entity.Symbol = symbol;
            entity.Quarter = quarter;
            await context.PeerComparisonDataEntities.AddAsync(entity);
        }

        return await context.SaveChangesAsync();
    }
}
