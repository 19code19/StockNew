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

    public async Task<int> SaveEquityListingsAsync(IEnumerable<Stock.Model.EquityListing> listings)
    {
        var entities = listings.Select(x => Mapper.ToEntity<Stock.Model.EquityListing, EquityListingEntity>(x)).ToList();
        _context.EquityListings.RemoveRange(_context.EquityListings);
        await _context.EquityListings.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveSymbolDataAsync(Stock.Model.SymbolDataResponse response)
    {
        var entity = Mapper.ToEntity<Stock.Model.SymbolDataResponse, SymbolDataEntity>(response);
        entity.Symbol = response.Symbol;
        entity.Series = response.Series;
        entity.MarketType = response.MarketType;

        _context.SymbolDataEntities.RemoveRange(_context.SymbolDataEntities.Where(x => x.Symbol == response.Symbol));
        await _context.SymbolDataEntities.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Model.YearwiseData> data, string symbol)
    {
        var entities = data.Select(x => Mapper.ToEntity<Stock.Model.YearwiseData, YearwiseDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        _context.YearwiseDataEntities.RemoveRange(_context.YearwiseDataEntities.Where(x => x.Symbol == symbol));
        await _context.YearwiseDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveHistoricalTradeDataAsync(IEnumerable<Stock.Model.HistoricalTradeData> data, string symbol)
    {
        var entities = data.Select(x => Mapper.ToEntity<Stock.Model.HistoricalTradeData, HistoricalTradeDataEntity>(x)).ToList();
        foreach (var entity in entities)
        {
            entity.Symbol = symbol;
        }

        _context.HistoricalTradeDataEntities.RemoveRange(_context.HistoricalTradeDataEntities.Where(x => x.Symbol == symbol));
        await _context.HistoricalTradeDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveIndexDataAsync(IEnumerable<Stock.Model.IndexData> data)
    {
        var entities = data.Select(x => Mapper.ToEntity<Stock.Model.IndexData, IndexDataEntity>(x)).ToList();
        _context.IndexDataEntities.RemoveRange(_context.IndexDataEntities);
        await _context.IndexDataEntities.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, Stock.Model.ShareholdingPatternEntry>? data)
    {
        if (data is null)
        {
            return 0;
        }

        _context.ShareholdingPatternEntries.RemoveRange(_context.ShareholdingPatternEntries.Where(x => x.Symbol == symbol));
        foreach (var item in data)
        {
            var entity = Mapper.ToEntity<Stock.Model.ShareholdingPatternEntry, ShareholdingPatternEntryEntity>(item.Value);
            entity.Symbol = symbol;
            entity.CategoryName = item.Key;
            await _context.ShareholdingPatternEntries.AddAsync(entity);
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<Stock.Model.PeerComparisonData>? data)
    {
        if (data is null)
        {
            return 0;
        }

        _context.PeerComparisonDataEntities.RemoveRange(_context.PeerComparisonDataEntities.Where(x => x.Symbol == symbol && x.Quarter == quarter));
        foreach (var item in data)
        {
            var entity = Mapper.ToEntity<Stock.Model.PeerComparisonData, PeerComparisonDataEntity>(item);
            entity.Symbol = symbol;
            entity.Quarter = quarter;
            await _context.PeerComparisonDataEntities.AddAsync(entity);
        }

        return await _context.SaveChangesAsync();
    }
}
