namespace Stock.Repository;

public interface IStockRepository
{
    Task<IReadOnlyList<string>> GetAllSymbolsAsync();
    Task<IReadOnlyList<string>> GetAllSymbolsSeriesAsync();
    Task<int> SaveEquityListingsAsync(IEnumerable<Entity.EquityListing> listings);
    Task<   SymbolDataResponse?> SaveSymbolDataAsync(SymbolDataResponse response);
    Task<int> SaveYearwiseDataAsync(IEnumerable<Entity.YearwiseData> data, string symbol);
    Task<int> SaveHistoricalTradeDataAsync(IEnumerable<HistoricalTradeData> data, string symbol, DateTime fromDate, DateTime toDate, string series = "EQ");
    Task<IReadOnlyList<HistoricalTradeData>> GetSavedHistoricalTradeDataAsync(string symbol, DateTime fromDate, DateTime toDate, string series = "EQ");
    Task<int> SaveIndexDataAsync(IEnumerable<IndexData> data);
    Task<int> SaveAllIndicesAsync(IEnumerable<IndicesData> data);
    Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, ShareholdingPatternEntry>? data);
    Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<PeerComparisonData>? data);
    Task<int> AddFavoriteSymbolAsync(string symbol, string companyName);
    Task<int> RemoveFavoriteSymbolAsync(string symbol);
    Task<IReadOnlyList<FavoriteSymbolEntity>> GetFavoriteSymbolsAsync();
}
