namespace Stock.Service;

public class NSEDataService
{
    private const string BaseUrl = "https://www.nseindia.com/api/";
    private const string EquityListUrl = "https://nsearchives.nseindia.com/content/equities/EQUITY_L.csv";
    private readonly HttpClient _httpClient;

    public NSEDataService(HttpClient httpClient)
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

    public async Task<IndicesResponse?> GetAllIndices() =>  await GetDataAsync<IndicesResponse>("allIndices");

    public async Task<List<Stock.Model.EquityListing>> GetEquityList()
    {
        var csvContent = await GetRawContentAsync(EquityListUrl);
        if (string.IsNullOrWhiteSpace(csvContent))
        {
            return new List<Stock.Model.EquityListing>();
        }
        return EquityListCsvParser.Parse(csvContent);
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

    public async Task<YearwiseDataBatchResult> GetYearwiseData(string symbol)
    {
        var endpoint = $"NextApi/apiClient/GetQuoteApi?functionName=getYearwiseData&symbol={Uri.EscapeDataString(symbol)}";
        var result = await GetDataAsync<List<Stock.Model.YearwiseData>>(endpoint);
        return new YearwiseDataBatchResult(symbol, result);
    }

    private async Task<T?> GetDataAsync<T>(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            using var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            return default;
        }
    }

    private async Task<string> GetRawContentAsync(string absoluteUrl)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, absoluteUrl);
            using var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return string.Empty;
        }
    }
}