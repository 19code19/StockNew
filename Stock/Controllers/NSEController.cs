using Microsoft.AspNetCore.Mvc;
using Stock.Model;
using Stock.Service;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NSEController(NSEService nSEService) : ControllerBase
    {
        private readonly NSEService _nSEService = nSEService;

        /// <summary>
        /// Returns all NSE indices
        /// </summary>
        /// <returns>List of indices grouped by category</returns>
        /// <response code="200">Success</response>
        /// <response code="500">NSE API unreachable</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IndicesResponse), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllIndices() => Ok(await _nSEService.GetAllIndices());

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
    }
}