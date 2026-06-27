namespace Stock.Repository;

public interface IStockRepository
{
    Task<IReadOnlyList<string>> GetAllSymbolsAsync();
    Task<int> SaveEquityListingsAsync(IEnumerable<Stock.Model.EquityListing> listings);
    Task<int> SaveSymbolDataAsync(Stock.Model.SymbolDataResponse response);
    Task<int> SaveYearwiseDataAsync(IEnumerable<Stock.Model.YearwiseData> data, string symbol);
    Task<int> SaveHistoricalTradeDataAsync(IEnumerable<Stock.Model.HistoricalTradeData> data, string symbol);
    Task<int> SaveIndexDataAsync(IEnumerable<Stock.Model.IndexData> data);
    Task<int> SaveShareholdingPatternAsync(string symbol, IDictionary<string, Stock.Model.ShareholdingPatternEntry>? data);
    Task<int> SavePeerComparisonDataAsync(string symbol, string quarter, IEnumerable<Stock.Model.PeerComparisonData>? data);
}
