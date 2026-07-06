namespace Stock.Service;

public class NSEService
{
    private readonly NSEDataService _nSEDataService;
    private readonly StockRepository _stockRepository;

    public NSEService(NSEDataService nSEDataService, StockRepository stockRepository)
    {
        _nSEDataService = nSEDataService;
        _stockRepository = stockRepository;
    }

    #region Business Logic Methods

    public async Task<int> SaveEquityList()
    {
        var data = await _nSEDataService.GetEquityList();
        var entities = data.Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Model.EquityListing, Stock.Entity.EquityListing>(x)).ToList();
        return await _stockRepository.SaveEquityListingsAsync(entities);
    }

    public async Task<int> SaveSymbolData()
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            var result = await _nSEDataService.GetSymbolData(symbol);
            var saved = await _stockRepository.SaveSymbolDataAsync(result);
            return saved is null ? 0 : 1;
        });

        return results.Sum();
    }

    public async Task<int> SaveYearwiseData()
    {
        var symbols = await _stockRepository.GetAllSymbolsSeriesAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            var result = await _nSEDataService.GetYearwiseData(symbol);
            var entities = (result.Data ?? []).Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Model.YearwiseData, Stock.Entity.YearwiseData>(x)).ToList();
            return await _stockRepository.SaveYearwiseDataAsync(entities, result.Symbol);
        });

        return results.Sum();
    }

    #endregion

    #region Private Helpers

    private async Task<List<TResult>> ProcessInBatchesAsync<TResult>(IReadOnlyList<string> symbols, Func<string, Task<TResult>> processor, int batchSize = 50)
    {
        var results = new List<TResult>();
        for (var index = 0; index < symbols.Count; index += batchSize)
        {
            var batch = symbols.Skip(index).Take(batchSize).ToList();
            var batchResults = await Task.WhenAll(batch.Select(processor));
            results.AddRange(batchResults);
        }

        return results;
    }

    #endregion
}