using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Stock.Data;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class YearwiseStockSummaryController(StockDbContext context) : ControllerBase
{
    private readonly StockDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<YearwiseStockSummaryEntity>>> Get()
    {
        var rows = await _context.YearwiseStockSummaries
            .AsNoTracking()
            .ToListAsync();

        return Ok(rows);
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadJson()
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
