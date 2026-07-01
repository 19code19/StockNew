using Microsoft.EntityFrameworkCore;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class YearwiseStockSummaryController(StockDbContext context) : ControllerBase
{
    private readonly StockDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<YearwiseStockSummaryDto>>> Get()
    {
        var rows = await _context.Database.SqlQueryRaw<YearwiseStockSummaryDto>(@"
            SELECT
                Y.Symbol,
                M.CompanyName,
                S.Sector,
                S.IndustryInfo,
                S.BasicIndustry,
                T.TotalTradedVolume,
                T.TotalTradedValue,
                T.DeliveryToTradedQuantity,
                T.QuantityTraded,
                T.DeliveryQuantity AS DeliveryQuantity,
                T.TotalMarketCap,
                P.YearHigh,
                P.YearLow,
                Y.YesterdayChangePercent,
                Y.OneWeekChangePercent,
                Y.OneMonthChangePercent,
                Y.ThreeMonthChangePercent,
                Y.SixMonthChangePercent,
                Y.OneYearChangePercent,
                Y.IndexName
            FROM dbo.vw_YearwiseStockSummary Y
            INNER JOIN dbo.TradeInfoEntities T ON REPLACE(Y.Symbol, 'EQN', '') = T.Symbol
            INNER JOIN dbo.SecInfoEntities S ON S.Symbol = T.Symbol
            INNER JOIN dbo.PriceInfoEntities P ON S.Symbol = P.Symbol
            INNER JOIN dbo.MetaDataEntities M ON M.Symbol = P.Symbol")
            .ToListAsync();

        return Ok(rows);
    }
}
