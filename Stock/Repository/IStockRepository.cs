namespace Stock.Repository;

public interface IStockRepository
{
    Task<IReadOnlyList<string>> GetAllSymbolsAsync();
    Task<int> SaveEquityListingsAsync(IEnumerable<EquityListing> listings);
    Task<   SymbolDataResponse?> SaveSymbolDataAsync(SymbolDataResponse response);
    Task<int> SaveYearwiseDataAsync(IEnumerable<YearwiseData> data, string symbol);
    Task<int> SaveHistoricalTradeDataAsync(IEnumerable<HistoricalTradeData> data, string symbol);
    Task<int> SaveIndexDataAsync(IEnumerable<IndexData> data);
    Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, ShareholdingPatternEntry>? data);
    Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<PeerComparisonData>? data);
}
