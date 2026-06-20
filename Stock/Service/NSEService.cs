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