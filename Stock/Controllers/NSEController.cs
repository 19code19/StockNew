using Microsoft.Extensions.Caching.Memory;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NSEController(NSEService nSEService, IMemoryCache memoryCache) : ControllerBase
{
    private const string FavoritesCacheKey = "nse-favorites";
    private const string HistoricalTradeCachePrefix = "nse-historical-trade";
    private readonly NSEService _nSEService = nSEService;
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Fetches all NSE indices and saves them into the AllIndices table
    /// </summary>
    /// <remarks>
    /// Calls the NSE API to retrieve the full set of indices then persists
    /// the payload into the AllIndices table. Returns the number of records
    /// inserted or updated.
    /// </remarks>
    /// <returns>Number of indices inserted or updated</returns>
    /// <response code="200">Success - returns count</response>
    /// <response code="500">NSE API unreachable or save failed</response>z`
    [HttpPost("allIndices")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SaveAllIndices() => Ok(await _nSEService.SaveAllIndices());

    /// <summary>
    /// Fetches and persists the full list of equities listed on NSE
    /// </summary>
    /// <remarks>
    /// Downloads and parses the EQUITY_L.csv master file published by NSE,
    /// then saves all equity listings (symbol, company name, series, listing
    /// date, ISIN, face value) to the database.
    /// </remarks>
    /// <returns>Number of equity listings saved</returns>
    /// <response code="200">Success</response>
    /// <response code="500">NSE archive unreachable, CSV could not be parsed, or save failed</response>
    [HttpPost("save-equity-list")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SaveEquityList() => Ok(await _nSEService.SaveEquityList());


    /// <summary>
    /// Fetches symbol data for tracked symbols and persists it
    /// </summary>
    /// <remarks>
    /// Pulls the current symbol list (from DB, eventually) and calls
    /// GetSymbolData for each in batches, then saves the results.
    /// </remarks>
    /// <returns>Number of symbol records saved</returns>
    /// <response code="200">Success</response>
    /// <response code="500">NSE API unreachable</response>
    [HttpPost("save-symbol-data")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SaveSymbolData() => Ok(await _nSEService.SaveSymbolData());

    /// <summary>
    /// Fetches yearwise data for tracked symbols and persists it
    /// </summary>
    /// <remarks>
    /// Pulls the current symbol list (from DB, eventually) and calls
    /// GetYearwiseData for each in batches, then saves the results.
    /// </remarks>
    /// <returns>Number of yearwise data records saved</returns>
    /// <response code="200">Success</response>
    /// <response code="500">NSE API unreachable</response>
    [HttpPost("save-yearwise-data")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> SaveYearwiseData() => Ok(await _nSEService.SaveYearwiseData());

    /// <summary>
    /// Gets historical trade data from the database if present, otherwise fetches and persists it.
    /// </summary>
    /// <param name="fromDate">Start date (inclusive) in yyyy-MM-dd format</param>
    /// <param name="toDate">End date (inclusive) in yyyy-MM-dd format</param>
    /// <param name="series">Series code (default: EQ)</param>
    /// <param name="forceRefresh">When true, always fetches from the NSE API and replaces cached data</param>
    /// <returns>Flattened list of HistoricalTradeData</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid date parameters</response>
    [HttpGet("historical-trade-data")]
    [ProducesResponseType(typeof(List<HistoricalTradeData>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetHistoricalTradeData([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string series = "EQ", [FromQuery] bool forceRefresh = false, [FromQuery] string? symbol = null)
    {
        if (fromDate > toDate)
        {
            return BadRequest("fromDate must be less than or equal to toDate");
        }

        var cacheKey = $"{HistoricalTradeCachePrefix}:{fromDate:yyyy-MM-dd}:{toDate:yyyy-MM-dd}:{series}:{symbol ?? string.Empty}:{forceRefresh}";
        if (!forceRefresh && _memoryCache.TryGetValue(cacheKey, out List<HistoricalTradeData>? cachedRows) && cachedRows is not null)
        {
            return Ok(cachedRows);
        }

        if (!string.IsNullOrWhiteSpace(symbol))
        {
            var single = await _nSEService.GetHistoricalTradeDataForSymbol(symbol, fromDate, toDate, series, forceRefresh);
            if (!forceRefresh)
            {
                _memoryCache.Set(cacheKey, single, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(1),
                });
            }
            return Ok(single);
        }

        var result = await _nSEService.GetHistoricalTradeData(fromDate, toDate, series, forceRefresh);
        if (!forceRefresh)
        {
            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(1),
            });
        }
        return Ok(result);
    }

    /// <summary>
    /// Fetches historical trade data from the NSE API for the requested date range and persists it.
    /// </summary>
    /// <param name="fromDate">Start date (inclusive) in yyyy-MM-dd format</param>
    /// <param name="toDate">End date (inclusive) in yyyy-MM-dd format</param>
    /// <param name="series">Series code (default: EQ)</param>
    /// <returns>Flattened list of HistoricalTradeData saved</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid date parameters</response>
    [HttpPost("save-historical-trade-data")]
    [ProducesResponseType(typeof(List<HistoricalTradeData>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SaveHistoricalTradeData([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string series = "EQ", [FromQuery] string? symbol = null)
    {
        if (fromDate > toDate)
        {
            return BadRequest("fromDate must be less than or equal to toDate");
        }
        if (!string.IsNullOrWhiteSpace(symbol))
        {
            var single = await _nSEService.SaveHistoricalTradeDataForSymbol(symbol, fromDate, toDate, series);
            return Ok(single);
        }

        var result = await _nSEService.SaveHistoricalTradeData(fromDate, toDate, series);
        return Ok(result);
    }

    [HttpPost("favorites")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> AddFavorite([FromQuery] string symbol, [FromQuery] string companyName)
    {
        if (string.IsNullOrWhiteSpace(symbol) || string.IsNullOrWhiteSpace(companyName))
        {
            return BadRequest("symbol and companyName are required");
        }

        var count = await _nSEService.AddFavoriteSymbol(symbol, companyName);
        _memoryCache.Remove(FavoritesCacheKey);
        return Ok(count);
    }

    [HttpDelete("favorites")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> RemoveFavorite([FromQuery] string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return BadRequest("symbol is required");
        }

        var count = await _nSEService.RemoveFavoriteSymbol(symbol);
        _memoryCache.Remove(FavoritesCacheKey);
        return Ok(count);
    }

    [HttpGet("favorites")]
    [ProducesResponseType(typeof(List<FavoriteSymbolEntity>), 200)]
    public async Task<IActionResult> GetFavorites()
    {
        if (_memoryCache.TryGetValue(FavoritesCacheKey, out List<FavoriteSymbolEntity>? cachedFavorites) && cachedFavorites is not null)
        {
            return Ok(cachedFavorites);
        }

        var favorites = await _nSEService.GetFavoriteSymbols();
        _memoryCache.Set(FavoritesCacheKey, favorites, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });
        return Ok(favorites);
    }

    /// <summary>
    /// Fetches index data (optionally by type) and persists it
    /// </summary>
    /// <param name="type">Index type or 'All' for all indices (default: All)</param>
    /// <returns>IndexDataResponse returned from NSE</returns>
    /// <response code="200">Success</response>
    [HttpPost("save-index-data")]
    [ProducesResponseType(typeof(IndexDataResponse), 200)]
    public async Task<IActionResult> SaveIndexData([FromQuery] string type = "All") => Ok(await _nSEService.SaveIndexData(type));

}
