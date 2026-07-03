using Microsoft.Extensions.Caching.Memory;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CorporateController(NSEService nSEService, IMemoryCache memoryCache) : ControllerBase
{
    private const string FavoritesCacheKey = "nse-favorites";
    private const string HistoricalTradeCachePrefix = "nse-historical-trade";
    private readonly NSEService _nSEService = nSEService;
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Fetches shareholding pattern for tracked symbols and persists it
    /// </summary>
    /// <param name="noOfRecords">Number of records to fetch per symbol (default: 50)</param>
    /// <returns>Aggregated ShareholdingPatternResult</returns>
    /// <response code="200">Success</response>
    [HttpPost("save-shareholding-pattern")]
    [ProducesResponseType(typeof(ShareholdingPatternResult), 200)]
    public async Task<IActionResult> SaveShareholdingPattern([FromQuery] int noOfRecords = 50) => Ok(await _nSEService.SaveShareholdingPattern(noOfRecords));

    /// <summary>
    /// Fetches peer comparison data for tracked symbols for a given quarter and persists it
    /// </summary>
    /// <param name="quarter">Quarter identifier (required)</param>
    /// <param name="type">Type filter (default: S)</param>
    /// <param name="param">Parameter for comparison (default: industry)</param>
    /// <param name="index">Optional index filter</param>
    /// <returns>PeerComparisonDataResult with aggregated data</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Missing quarter parameter</response>
    [HttpPost("save-peer-comparison")]
    [ProducesResponseType(typeof(PeerComparisonDataResult), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SavePeerComparisonData([FromQuery] string quarter, [FromQuery] string type = "S", [FromQuery] string param = "industry", [FromQuery] string index = "")
    {
        if (string.IsNullOrWhiteSpace(quarter))
        {
            return BadRequest("quarter is required");
        }

        var result = await _nSEService.SavePeerComparisonData(quarter, type, param, index);
        return Ok(result);
    }

    /// <summary>
    /// Fetches corporate board meetings for tracked symbols and persists them
    /// </summary>
    /// <param name="marketApiType">Market type value used by NSE API (default: equities)</param>
    /// <param name="type">Frequency/type (default: W)</param>
    /// <param name="noOfRecords">Number of records per symbol (default: 4)</param>
    /// <returns>Aggregated CorpBoardMeetingResult</returns>
    [HttpPost("save-corp-board-meetings")]
    [ProducesResponseType(typeof(CorpBoardMeetingResult), 200)]
    public async Task<IActionResult> SaveCorpBoardMeeting([FromQuery] string marketApiType = "equities", [FromQuery] string type = "W", [FromQuery] int noOfRecords = 4)
        => Ok(await _nSEService.SaveCorpBoardMeeting(marketApiType, type, noOfRecords));

    /// <summary>
    /// Fetches financial status entries for tracked symbols and persists them
    /// </summary>
    /// <returns>Aggregated FinancialStatusResult</returns>
    [HttpPost("save-financial-status")]
    [ProducesResponseType(typeof(FinancialStatusResult), 200)]
    public async Task<IActionResult> SaveFinancialStatus() => Ok(await _nSEService.SaveFinancialStatus());

    /// <summary>
    /// Fetches corporate event calendar entries for tracked symbols and persists them
    /// </summary>
    /// <param name="noOfRecords">Records per symbol (default: 3)</param>
    /// <param name="marketApiType">Market type (default: equities)</param>
    /// <returns>Aggregated CorpEventCalendarResult</returns>
    [HttpPost("save-corp-event-calendar")]
    [ProducesResponseType(typeof(CorpEventCalendarResult), 200)]
    public async Task<IActionResult> SaveCorpEventCalendar([FromQuery] int noOfRecords = 3, [FromQuery] string marketApiType = "equities")
        => Ok(await _nSEService.SaveCorpEventCalendar(noOfRecords, marketApiType));

    /// <summary>
    /// Fetches corporate annual reports for tracked symbols and persists them
    /// </summary>
    /// <param name="marketApiType">Market type (default: equities)</param>
    /// <param name="noOfRecords">Records per symbol (default: 6)</param>
    /// <returns>Aggregated CorpAnnualReportResult</returns>
    [HttpPost("save-corp-annual-reports")]
    [ProducesResponseType(typeof(CorpAnnualReportResult), 200)]
    public async Task<IActionResult> SaveCorpAnnualReport([FromQuery] string marketApiType = "equities", [FromQuery] int noOfRecords = 6)
        => Ok(await _nSEService.SaveCorpAnnualReport(marketApiType, noOfRecords));

    /// <summary>
    /// Fetches corporate actions for tracked symbols and persists them
    /// </summary>
    /// <param name="marketApiType">Market type (default: equities)</param>
    /// <param name="noOfRecords">Records per symbol (default: 6)</param>
    /// <returns>Aggregated CorpActionResult</returns>
    [HttpPost("save-corp-actions")]
    [ProducesResponseType(typeof(CorpActionResult), 200)]
    public async Task<IActionResult> SaveCorpActions([FromQuery] string marketApiType = "equities", [FromQuery] int noOfRecords = 6)
        => Ok(await _nSEService.SaveCorpActions(marketApiType, noOfRecords));

    /// <summary>
    /// Fetches corporate announcements for tracked symbols and persists them
    /// </summary>
    /// <param name="marketApiType">Market type (default: equities)</param>
    /// <param name="noOfRecords">Records per symbol (default: 6)</param>
    /// <returns>Aggregated CorpAnnouncementResult</returns>
    [HttpPost("save-corp-announcements")]
    [ProducesResponseType(typeof(CorpAnnouncementResult), 200)]
    public async Task<IActionResult> SaveCorpAnnouncements([FromQuery] string marketApiType = "equities", [FromQuery] int noOfRecords = 6)
        => Ok(await _nSEService.SaveCorpAnnouncements(marketApiType, noOfRecords));
}
