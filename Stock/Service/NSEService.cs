using Stock.Helpers;
using Stock.Model;

namespace Stock.Service
{
    public class NSEService
    {
        private const string BaseUrl = "https://www.nseindia.com/api/";
        private const string EquityListUrl = "https://nsearchives.nseindia.com/content/equities/EQUITY_L.csv";
        private readonly HttpClient _httpClient;
        private const int batchSize = 50;

        public NSEService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept",
                "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("Referer",
                "https://www.nseindia.com/");
        }

        public async Task<IndicesResponse?> GetAllIndices()
        {
            var data = await GetDataAsync<IndicesResponse>("allIndices");
            return data;
        }

        public async Task<int> SaveEquityList()
        {
            var csvContent = await GetRawContentAsync(EquityListUrl);
            var data = EquityListCsvParser.Parse(csvContent);
            return data.Count;
        }

        public async Task<int> SaveSymbolData()
        {
            var symbolsToFetch = new List<string> { "RELIANCE", "TCS", "INFY" }; // Example symbols will be fetch from db later
            var symbolDataResponseList = new List<SymbolDataResponse>();

            foreach (var batch in symbolsToFetch.Chunk(batchSize))
            {
                var tasks = batch.Select(symbol => GetSymbolData(symbol));
                var results = await Task.WhenAll(tasks);
                symbolDataResponseList.AddRange(results);
            }

            // TODO: persist symbolDataResponseList to db

            return symbolDataResponseList.Count;
        }

        public async Task<int> SaveYearwiseData()
        {
            var symbolsToFetch = new List<string> { "RELIANCE", "TCS", "INFY" }; // Example symbols will be fetch from db later
            var yearwiseDataBatchResultList = new List<YearwiseDataBatchResult>();

            foreach (var batch in symbolsToFetch.Chunk(batchSize))
            {
                var tasks = batch.Select(symbol => GetYearwiseData(symbol));
                var results = await Task.WhenAll(tasks);
                yearwiseDataBatchResultList.AddRange(results);
            }

            // TODO: persist symbolDataResponseList to db

            return yearwiseDataBatchResultList.Count;
        }
        public async Task<YearwiseDataBatchResult> GetYearwiseData(string symbol)
        {
            var endpoint = $"NextApi/apiClient/GetQuoteApi?functionName=getYearwiseData&symbol={Uri.EscapeDataString(symbol)}";
            var result = await GetDataAsync<List<YearwiseData>>(endpoint);
            return new YearwiseDataBatchResult
            {
                Data = result,
                Symbol = symbol
            };
        }

        public async Task<SymbolDataResponse> GetSymbolData(string symbol, string series = "EQ", string marketType = "N")
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getSymbolData&marketType={Uri.EscapeDataString(marketType)}" +
                $"&series={Uri.EscapeDataString(series)}&symbol={Uri.EscapeDataString(symbol)}";

            var response = await GetDataAsync<SymbolDataResponse>(endpoint) ?? new();
            response.Symbol = symbol;
            response.Series = series;
            response.MarketType = marketType;
            return response;
        }

        public async Task<List<HistoricalTradeData>> GetHistoricalTradeData(string symbol, DateTime fromDate, DateTime toDate, string series = "EQ")
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getHistoricalTradeData&symbol={Uri.EscapeDataString(symbol)}" +
                $"&series={Uri.EscapeDataString(series)}" +
                $"&fromDate={fromDate:dd-MM-yyyy}&toDate={toDate:dd-MM-yyyy}";

            var result = await GetDataAsync<List<HistoricalTradeData>>(endpoint);
            return result;
        }

        public async Task<int> SaveHistoricalTradeData(DateTime fromDate, DateTime toDate, string series = "EQ")
        {
            var symbolsToFetch = new List<string> { "RELIANCE", "TCS", "INFY" };
            var historicalTradeDataBatchResultList = new List<List<HistoricalTradeData>>();

            foreach (var batch in symbolsToFetch.Chunk(batchSize))
            {
                var tasks = batch.Select(symbol => GetHistoricalTradeData(symbol, fromDate, toDate, series));
                var results = await Task.WhenAll(tasks);
                historicalTradeDataBatchResultList.AddRange(results);
            }

            // TODO: persist historicalTradeDataBatchResultList to db

            return historicalTradeDataBatchResultList.Count;
        }

        public async Task<IndexDataResponse?> GetIndexData(string type = "All")
        {
            var endpoint = $"NextApi/apiClient?functionName=getIndexData&type={Uri.EscapeDataString(type)}";
            var result = await GetDataAsync<IndexDataResponse>(endpoint);
            return result;
        }

        public async Task<ShareholdingPatternResult> GetShareholdingPattern(string symbol, int noOfRecords = 50)
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getShareholdingPattern&symbol={Uri.EscapeDataString(symbol)}" +
                $"&noOfRecords={noOfRecords}";

            var result = await GetDataAsync<Dictionary<string, ShareholdingPatternEntry>>(endpoint);

            return new ShareholdingPatternResult
            {
                Symbol = symbol,
                Data = result
            };
        }

        public async Task<PeerComparisonDataResult> GetPeerComparisonData(
    string symbol, string quarter, string type = "S", string param = "industry", string index = "")
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getPeerComparisonData&symbol={Uri.EscapeDataString(symbol)}" +
                $"&type={Uri.EscapeDataString(type)}" +
                $"&quarter={Uri.EscapeDataString(quarter)}" +
                $"&param={Uri.EscapeDataString(param)}" +
                $"&index={Uri.EscapeDataString(index)}";

            var result = await GetDataAsync<List<PeerComparisonData>>(endpoint);

            return new PeerComparisonDataResult
            {
                Symbol = symbol,
                Quarter = quarter,
                Data = result
            };
        }

        public async Task<CorpBoardMeetingResult> GetCorpBoardMeeting(
    string symbol, string marketApiType = "equities", string type = "W", int noOfRecords = 4)
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getCorpBoardMeeting&symbol={Uri.EscapeDataString(symbol)}" +
                $"&marketApiType={Uri.EscapeDataString(marketApiType)}" +
                $"&type={Uri.EscapeDataString(type)}" +
                $"&noOfRecords={noOfRecords}";

            var result = await GetDataAsync<List<CorpBoardMeeting>>(endpoint);

            return new CorpBoardMeetingResult
            {
                Symbol = symbol,
                Data = result
            };
        }
        public async Task<FinancialStatusResult> GetFinancialStatus(string symbol)
        {
            var endpoint = $"NextApi/apiClient/GetQuoteApi?functionName=getFinancialStatus&symbol={Uri.EscapeDataString(symbol)}";

            var result = await GetDataAsync<List<FinancialStatus>>(endpoint);

            return new FinancialStatusResult
            {
                Symbol = symbol,
                Data = result
            };
        }

        public async Task<CorpEventCalendarResult> GetCorpEventCalendar( string symbol, int noOfRecords = 3, string marketApiType = "equities")
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getCorpEventCalender&symbol={Uri.EscapeDataString(symbol)}" +
                $"&noOfRecords={noOfRecords}" +
                $"&marketApiType={Uri.EscapeDataString(marketApiType)}";

            var result = await GetDataAsync<List<CorpEventCalendar>>(endpoint);

            return new CorpEventCalendarResult
            {
                Symbol = symbol,
                Data = result
            };
        }
        public async Task<CorpAnnualReportResult> GetCorpAnnualReport( string symbol, string marketApiType = "equities", int noOfRecords = 6)
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getCorpAnnualReport&symbol={Uri.EscapeDataString(symbol)}" +
                $"&marketApiType={Uri.EscapeDataString(marketApiType)}" +
                $"&noOfRecords={noOfRecords}";

            var result = await GetDataAsync<List<CorpAnnualReport>>(endpoint);

            return new CorpAnnualReportResult
            {
                Symbol = symbol,
                Data = result
            };
        }

        public async Task<CorpActionResult> GetCorpActions( string symbol, string marketApiType = "equities", int noOfRecords = 6)
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getCorpActions&symbol={Uri.EscapeDataString(symbol)}" +
                $"&marketApiType={Uri.EscapeDataString(marketApiType)}" +
                $"&noOfRecords={noOfRecords}";

            var result = await GetDataAsync<List<CorpAction>>(endpoint);

            return new CorpActionResult
            {
                Symbol = symbol,
                Data = result
            };
        }

        public async Task<CorpAnnouncementResult> GetCorpAnnouncements(string symbol, string marketApiType = "equities", int noOfRecords = 6)
        {
            var endpoint = "NextApi/apiClient/GetQuoteApi" +
                $"?functionName=getCorpAnnouncement&symbol={Uri.EscapeDataString(symbol)}" +
                $"&marketApiType={Uri.EscapeDataString(marketApiType)}" +
                $"&noOfRecords={noOfRecords}";

            var result = await GetDataAsync<List<CorpAnnouncement>>(endpoint);

            return new CorpAnnouncementResult
            {
                Symbol = symbol,
                Data = result
            };
        }

        private async Task<T?> GetDataAsync<T>(string endpoint)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            using var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task<string> GetRawContentAsync(string absoluteUrl)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, absoluteUrl);
            using var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}