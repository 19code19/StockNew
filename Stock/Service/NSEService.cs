namespace Stock.Service;

public class NSEService
{
    private const string BaseUrl = "https://www.nseindia.com/api/";
    private const string EquityListUrl = "https://nsearchives.nseindia.com/content/equities/EQUITY_L.csv";
    private readonly NSEDataService _nSEDataService;
    private const int batchSize = 50;

    public NSEService(NSEDataService nSEDataService)
    {
        _nSEDataService = nSEDataService;
    }

    public async Task<IndicesResponse?> GetAllIndices()
    {
        var data = await _nSEDataService.GetAllIndices();
        return data;
    }

    public async Task<int> SaveEquityList()
    {
        var data = await _nSEDataService.GetEquityList();
        return data.Count;
    }

    public async Task<int> SaveSymbolData()
    {
        string symbol = "RELIANCE";
        var result = await _nSEDataService.GetSymbolData(symbol);
        return 1;
    }

    public async Task<int> SaveYearwiseData()
    {
        string symbol = "RELIANCE";
        var result = await _nSEDataService.GetYearwiseData(symbol);
        return 1;
    }

    public async Task<List<HistoricalTradeData>> SaveHistoricalTradeData(string symbol, DateTime fromDate, DateTime toDate, string series = "EQ")
    {
       var result = await _nSEDataService.GetHistoricalTradeData(symbol, fromDate, toDate, series);
        return result;
    }

    public async Task<IndexDataResponse?> SaveIndexData(string type = "All")
    {
        var result = await _nSEDataService.GetIndexData();
        return result;
    }

    public async Task<ShareholdingPatternResult> SaveShareholdingPattern(string symbol, int noOfRecords = 50)
    {
        var result = await _nSEDataService.GetShareholdingPattern(symbol, noOfRecords);
        return result;
    }

    public async Task<PeerComparisonDataResult> SavePeerComparisonData( string symbol, string quarter, string type = "S", string param = "industry", string index = "")
    {
        var result = await _nSEDataService.GetPeerComparisonData(symbol, quarter, type, param, index);
        return result;
    }

    public async Task<CorpBoardMeetingResult> SaveCorpBoardMeeting(string symbol, string marketApiType = "equities", string type = "W", int noOfRecords = 4)
    {
        var result = await _nSEDataService.GetCorpBoardMeeting(symbol, marketApiType, type, noOfRecords);
        return result;
    }
    public async Task<FinancialStatusResult> SaveFinancialStatus(string symbol)
    {
        var result = await _nSEDataService.GetFinancialStatus(symbol);
        return result;
    }

    public async Task<CorpEventCalendarResult> SaveCorpEventCalendar(string symbol, int noOfRecords = 3, string marketApiType = "equities")
    {
        var result = await _nSEDataService.GetCorpEventCalendar(symbol, noOfRecords, marketApiType);
        return result;
    }
    public async Task<CorpAnnualReportResult> SaveCorpAnnualReport(string symbol, string marketApiType = "equities", int noOfRecords = 6)
    {
        var result = await _nSEDataService.GetCorpAnnualReport(symbol, marketApiType, noOfRecords);
        return result;
    }

    public async Task<CorpActionResult> SaveCorpActions(string symbol, string marketApiType = "equities", int noOfRecords = 6)
    {
        var result = await _nSEDataService.GetCorpActions(symbol, marketApiType, noOfRecords);
        return result;
    }

    public async Task<CorpAnnouncementResult> SaveCorpAnnouncements(string symbol, string marketApiType = "equities", int noOfRecords = 6)
    {
        var result = await _nSEDataService.GetCorpAnnouncements(symbol, marketApiType, noOfRecords);
        return result;
    }
}