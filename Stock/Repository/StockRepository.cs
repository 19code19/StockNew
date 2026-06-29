using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Helpers;

namespace Stock.Repository;

public class StockRepository(StockDbContext context) : IStockRepository
{
    private readonly StockDbContext _context = context;

    public async Task<IReadOnlyList<string>> GetAllSymbolsAsync()
    {
        return await _context.EquityListings
            .AsNoTracking()
            .Select(x => x.Symbol)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<int> SaveEquityListingsAsync(IEnumerable<Stock.Entity.EquityListing> listings)
    {
        var entities = listings.Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Entity.EquityListing, EquityListingEntity>(x)).ToList();
        _context.EquityListings.RemoveRange(_context.EquityListings);
        await _context.EquityListings.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<SymbolDataResponse?> SaveSymbolDataAsync(SymbolDataResponse response)
    {
        if (response is null)
        {
            return null;
        }

        // Map and save main SymbolDataEntity as before
        var entity = Stock.Helpers.Mapper.ToEntity<SymbolDataResponse, SymbolDataEntity>(response);
        entity.Symbol = response.Symbol;
        entity.Series = response.Series;
        entity.MarketType = response.MarketType;

        _context.SymbolDataEntities.RemoveRange(_context.SymbolDataEntities.Where(x => x.Symbol == response.Symbol));
        await _context.SymbolDataEntities.AddAsync(entity);

        // Remove previous split-detail entries for this symbol
        _context.OrderBookEntities.RemoveRange(_context.OrderBookEntities.Where(x => x.Symbol == response.Symbol));
        _context.MetaDataEntities.RemoveRange(_context.MetaDataEntities.Where(x => x.Symbol == response.Symbol));
        _context.TradeInfoEntities.RemoveRange(_context.TradeInfoEntities.Where(x => x.Symbol == response.Symbol));
        _context.PriceInfoEntities.RemoveRange(_context.PriceInfoEntities.Where(x => x.Symbol == response.Symbol));
        _context.SecInfoEntities.RemoveRange(_context.SecInfoEntities.Where(x => x.Symbol == response.Symbol));

        // Insert split entries into their own tables with Symbol populated
        foreach (var equity in response.EquityResponse)
        {
            if (equity.OrderBook is not null)
            {
                var ob = Stock.Helpers.Mapper.ToEntity<Stock.Model.OrderBook, OrderBookEntity>(equity.OrderBook);
                ob.Symbol = response.Symbol;
                await _context.OrderBookEntities.AddAsync(ob);
            }

            if (equity.MetaData is not null)
            {
                var md = Stock.Helpers.Mapper.ToEntity<Stock.Model.MetaData, MetaDataEntity>(equity.MetaData);
                md.Symbol = response.Symbol;
                await _context.MetaDataEntities.AddAsync(md);
            }

            if (equity.TradeInfo is not null)
            {
                var ti = Stock.Helpers.Mapper.ToEntity<Stock.Model.TradeInfo, TradeInfoEntity>(equity.TradeInfo);
                ti.Symbol = response.Symbol;
                await _context.TradeInfoEntities.AddAsync(ti);
            }

            if (equity.PriceInfo is not null)
            {
                var pi = Stock.Helpers.Mapper.ToEntity<Stock.Model.PriceInfo, PriceInfoEntity>(equity.PriceInfo);
                pi.Symbol = response.Symbol;
                await _context.PriceInfoEntities.AddAsync(pi);
            }

            if (equity.SecInfo is not null)
            {
                var si = Stock.Helpers.Mapper.ToEntity<Stock.Model.SecInfo, SecInfoEntity>(equity.SecInfo);
                si.Symbol = response.Symbol;
                if (equity.SecInfo.IndexList is not null && equity.SecInfo.IndexList.Any())
                {
                    si.IndexListJson = System.Text.Json.JsonSerializer.Serialize(equity.SecInfo.IndexList);
                }

                await _context.SecInfoEntities.AddAsync(si);
            }
        }

        await _context.SaveChangesAsync();
        return response;
    }

    public async Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Entity.YearwiseData> data, string symbol)
    {
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Entity.YearwiseData, YearwiseDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        _context.YearwiseDataEntities.RemoveRange(_context.YearwiseDataEntities.Where(x => x.Symbol == symbol));
        await _context.YearwiseDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveHistoricalTradeDataAsync(IEnumerable<HistoricalTradeData> data, string symbol)
    {
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<HistoricalTradeData, HistoricalTradeDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        _context.HistoricalTradeDataEntities.RemoveRange(_context.HistoricalTradeDataEntities.Where(x => x.Symbol == symbol));
        await _context.HistoricalTradeDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveIndexDataAsync(IEnumerable<IndexData> data)
    {
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<IndexData, IndexDataEntity>(x)).ToList();
        _context.IndexDataEntities.RemoveRange(_context.IndexDataEntities);
        await _context.IndexDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveAllIndicesAsync(IEnumerable<IndicesData> data)
    {
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<IndicesData, AllIndicesEntity>(x)).ToList();
        _context.AllIndices.RemoveRange(_context.AllIndices);
        await _context.AllIndices.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, ShareholdingPatternEntry>? data)
    {
        if (data is null)
        {
            return 0;
        }

        _context.ShareholdingPatternEntries.RemoveRange(_context.ShareholdingPatternEntries.Where(x => x.Symbol == symbol));
        foreach (var item in data)
        {
            var entity = Stock.Helpers.Mapper.ToEntity<ShareholdingPatternEntry, ShareholdingPatternEntryEntity>(item.Value);
            entity.Symbol = symbol;
            entity.CategoryName = item.Key;
            await _context.ShareholdingPatternEntries.AddAsync(entity);
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<PeerComparisonData>? data)
    {
        if (data is null)
        {
            return 0;
        }

        _context.PeerComparisonDataEntities.RemoveRange(_context.PeerComparisonDataEntities.Where(x => x.Symbol == symbol && x.Quarter == quarter));
        foreach (var item in data)
        {
            var entity = Stock.Helpers.Mapper.ToEntity<PeerComparisonData, PeerComparisonDataEntity>(item);
            entity.Symbol = symbol;
            entity.Quarter = quarter;
            await _context.PeerComparisonDataEntities.AddAsync(entity);
        }

        return await _context.SaveChangesAsync();
    }
}
