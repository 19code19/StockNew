namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MutualFundController(MutualFundService mutualFundService) : ControllerBase
{
    private readonly MutualFundService _mutualFundService = mutualFundService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MutualFundSchemeEntity>), 200)]
    public async Task<IActionResult> GetMutualFundSchemes()
    {
        var schemes = await _mutualFundService.GetMutualFundSchemesAsync();
        return Ok(schemes);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> RefreshMutualFundSchemes()
    {
        var savedCount = await _mutualFundService.RefreshMutualFundSchemesAsync();
        return Ok(new { saved = savedCount });
    }
}
