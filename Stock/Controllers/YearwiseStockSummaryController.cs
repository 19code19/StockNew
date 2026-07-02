using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Stock.Data;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class YearwiseStockSummaryController(StockDbContext context, IMemoryCache memoryCache) : ControllerBase
{
    private const string CacheKey = "yearwise-stock-summary-rows";
    private readonly StockDbContext _context = context;
    private readonly IMemoryCache _memoryCache = memoryCache;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<YearwiseStockSummaryEntity>>> Get()
    {
        if (_memoryCache.TryGetValue(CacheKey, out List<YearwiseStockSummaryEntity>? cachedRows) && cachedRows is not null)
        {
            return Ok(cachedRows);
        }

        var rows = await _context.YearwiseStockSummaries
            .AsNoTracking()
            .ToListAsync();

        _memoryCache.Set(CacheKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });

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
