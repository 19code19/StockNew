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
}
