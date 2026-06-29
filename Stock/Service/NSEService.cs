namespace Stock.Service;

public class NSEService
{
    private readonly NSEDataService _nSEDataService;
    private readonly IStockRepository _stockRepository;

    public NSEService(NSEDataService nSEDataService, IStockRepository stockRepository)
    {
        _nSEDataService = nSEDataService;
        _stockRepository = stockRepository;
    }

    public async Task<int> SaveAllIndices()
    {
        var data = await _nSEDataService.GetAllIndices();
        return await _stockRepository.SaveAllIndicesAsync(data?.Data ?? []);
    }

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
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            var result = await _nSEDataService.GetYearwiseData(symbol);
            var entities = (result.Data ?? []).Select(x => Stock.Helpers.Mapper.ToEntity<Stock.Model.YearwiseData, Stock.Entity.YearwiseData>(x)).ToList();
            return await _stockRepository.SaveYearwiseDataAsync(entities, result.Symbol);
        });

        return results.Sum();
    }

    public async Task<List<HistoricalTradeData>> SaveHistoricalTradeData(DateTime fromDate, DateTime toDate, string series = "EQ")
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            var data = await _nSEDataService.GetHistoricalTradeData(symbol, fromDate, toDate, series);
            await _stockRepository.SaveHistoricalTradeDataAsync(data, symbol);
            return data;
        });

        return results.SelectMany(x => x).ToList();
    }

    public async Task<IndexDataResponse?> SaveIndexData(string type = "All")
    {
        var result = await _nSEDataService.GetIndexData();
        if (result?.Data is not null)
        {
            await _stockRepository.SaveIndexDataAsync(result.Data);
        }

        return result;
    }

    public async Task<ShareholdingPatternResult> SaveShareholdingPattern(int noOfRecords = 50)
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async currentSymbol =>
        {
            var result = await _nSEDataService.GetShareholdingPattern(currentSymbol, noOfRecords);
            await _stockRepository.SaveShareholdingPatternAsync(currentSymbol, result.Data);
            return result;
        });

        return new ShareholdingPatternResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToDictionary(x => x.Key, x => x.Value));
    }

    public async Task<PeerComparisonDataResult> SavePeerComparisonData(string quarter, string type = "S", string param = "industry", string index = "")
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async currentSymbol =>
        {
            var result = await _nSEDataService.GetPeerComparisonData(currentSymbol, quarter, type, param, index);
            await _stockRepository.SavePeerComparisonDataAsync(currentSymbol, quarter, result.Data);
            return result;
        });

        return new PeerComparisonDataResult(string.Empty, quarter, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<CorpBoardMeetingResult> SaveCorpBoardMeeting(string marketApiType = "equities", string type = "W", int noOfRecords = 4)
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetCorpBoardMeeting(symbol, marketApiType, type, noOfRecords);
        });

        return new CorpBoardMeetingResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<FinancialStatusResult> SaveFinancialStatus()
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetFinancialStatus(symbol);
        });

        return new FinancialStatusResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<CorpEventCalendarResult> SaveCorpEventCalendar(int noOfRecords = 3, string marketApiType = "equities")
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetCorpEventCalendar(symbol, noOfRecords, marketApiType);
        });

        return new CorpEventCalendarResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<CorpAnnualReportResult> SaveCorpAnnualReport(string marketApiType = "equities", int noOfRecords = 6)
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetCorpAnnualReport(symbol, marketApiType, noOfRecords);
        });

        return new CorpAnnualReportResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<CorpActionResult> SaveCorpActions(string marketApiType = "equities", int noOfRecords = 6)
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetCorpActions(symbol, marketApiType, noOfRecords);
        });

        return new CorpActionResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    public async Task<CorpAnnouncementResult> SaveCorpAnnouncements(string marketApiType = "equities", int noOfRecords = 6)
    {
        var symbols = await _stockRepository.GetAllSymbolsAsync();
        var results = await ProcessInBatchesAsync(symbols, async symbol =>
        {
            return await _nSEDataService.GetCorpAnnouncements(symbol, marketApiType, noOfRecords);
        });

        return new CorpAnnouncementResult(string.Empty, results.SelectMany(x => x.Data ?? []).ToList());
    }

    #region Private Helpers

    private async Task<List<TResult>> ProcessInBatchesAsync<TResult>(IReadOnlyList<string> symbols,  Func<string, Task<TResult>> processor, int batchSize = 50)
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