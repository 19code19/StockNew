namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NSEController(NSEService nSEService) : ControllerBase
{
    private readonly NSEService _nSEService = nSEService;

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
    /// <response code="500">NSE API unreachable or save failed</response>
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
    public async Task<IActionResult> GetHistoricalTradeData([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string series = "EQ", [FromQuery] bool forceRefresh = false)
    {
        if (fromDate > toDate)
        {
            return BadRequest("fromDate must be less than or equal to toDate");
        }

        var result = await _nSEService.GetHistoricalTradeData(fromDate, toDate, series, forceRefresh);
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
    public async Task<IActionResult> SaveHistoricalTradeData([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string series = "EQ")
    {
        if (fromDate > toDate)
        {
            return BadRequest("fromDate must be less than or equal to toDate");
        }

        var result = await _nSEService.SaveHistoricalTradeData(fromDate, toDate, series);
        return Ok(result);
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
