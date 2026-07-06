using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NSEController(NSEService nSEService, StockDbContext context, IMemoryCache memoryCache) : ControllerBase
{
    private const string YearwiseCacheKey = "yearwise-stock-summary-rows";
    private readonly NSEService _nSEService = nSEService;
    private readonly StockDbContext _context = context;
    private readonly IMemoryCache _memoryCache = memoryCache;

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
    /// Gets yearwise stock summary data
    /// </summary>
    /// <returns>List of yearwise stock summaries</returns>
    /// <response code="200">Success</response>
    [HttpGet("yearwise-summary")]
    [ProducesResponseType(typeof(IEnumerable<YearwiseStockSummaryEntity>), 200)]
    public async Task<ActionResult<IEnumerable<YearwiseStockSummaryEntity>>> GetYearwiseSummary()
    {
        if (_memoryCache.TryGetValue(YearwiseCacheKey, out List<YearwiseStockSummaryEntity>? cachedRows) && cachedRows is not null)
        {
            return Ok(cachedRows);
        }

        // Convert vw_YearwiseStockSummary to LINQ query
        var rows = await (from y in _context.YearwiseDataEntities
                          join t in _context.TradeInfoEntities on y.Symbol.Replace("EQN", "") equals t.Symbol
                          join s in _context.SecInfoEntities on t.Symbol equals s.Symbol
                          join p in _context.PriceInfoEntities on s.Symbol equals p.Symbol
                          join m in _context.MetaDataEntities on p.Symbol equals m.Symbol
                          select new YearwiseStockSummaryEntity
                          {
                              YesterdayChangePercent = y.YesterdayChangePercent,
                              OneWeekChangePercent = y.OneWeekChangePercent,
                              OneMonthChangePercent = y.OneMonthChangePercent,
                              ThreeMonthChangePercent = y.ThreeMonthChangePercent,
                              SixMonthChangePercent = y.SixMonthChangePercent,
                              OneYearChangePercent = y.OneYearChangePercent,
                              TwoYearChangePercent = y.TwoYearChangePercent,
                              ThreeYearChangePercent = y.ThreeYearChangePercent,
                              FiveYearChangePercent = y.FiveYearChangePercent,
                              IndexYesterdayChangePercent = y.IndexYesterdayChangePercent,
                              IndexOneWeekChangePercent = y.IndexOneWeekChangePercent,
                              IndexOneMonthChangePercent = y.IndexOneMonthChangePercent,
                              IndexThreeMonthChangePercent = y.IndexThreeMonthChangePercent,
                              IndexSixMonthChangePercent = y.IndexSixMonthChangePercent,
                              IndexOneYearChangePercent = y.IndexOneYearChangePercent,
                              IndexTwoYearChangePercent = y.IndexTwoYearChangePercent,
                              IndexThreeYearChangePercent = y.IndexThreeYearChangePercent,
                              IndexFiveYearChangePercent = y.IndexFiveYearChangePercent,
                              IndexName = y.IndexName,
                              TotalTradedVolume = t.TotalTradedVolume,
                              TotalTradedValue = t.TotalTradedValue,
                              QuantityTraded = t.QuantityTraded,
                              DeliveryQuantity = t.DeliveryQuantity,
                              TotalMarketCap = t.TotalMarketCap,
                              BasicIndustry = s.BasicIndustry,
                              IsSuspended = s.IsSuspended,
                              Macro = s.Macro,
                              Sector = s.Sector,
                              IndustryInfo = s.IndustryInfo,
                              CompanyName = m.CompanyName,
                              Symbol = m.Symbol
                          })
                          .AsNoTracking()
                          .ToListAsync();

        _memoryCache.Set(YearwiseCacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });

        return Ok(rows);
    }

    /// <summary>
    /// Downloads yearwise stock summary data as JSON
    /// </summary>
    /// <returns>JSON file of yearwise stock summaries</returns>
    /// <response code="200">Success</response>
    [HttpGet("yearwise-summary/download")]
    public async Task<IActionResult> DownloadYearwiseSummary()
    {
        var rows = await _context.YearwiseStockSummaries
            .AsNoTracking()
            .ToListAsync();

        var json = JsonSerializer.Serialize(rows, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        });

        var bytes = Encoding.UTF8.GetBytes(json);
        var timestamp = DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-fff");
        return File(bytes, "application/json", $"yearwise-stock-summary-{timestamp}.json");
    }
}
