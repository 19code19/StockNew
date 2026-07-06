using Microsoft.Extensions.Caching.Memory;
using Stock.Service;

namespace Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController(FavoritesService favoritesService, IMemoryCache memoryCache) : ControllerBase
{
    private const string CacheKey = "favorites";
    private readonly FavoritesService _favoritesService = favoritesService;
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Adds a symbol to the user's favorites
    /// </summary>
    /// <param name="symbol">Stock symbol</param>
    /// <param name="companyName">Company name</param>
    /// <returns>Result of the add operation</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Missing required parameters</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddFavorite([FromQuery] string symbol, [FromQuery] string companyName)
    {
        if (string.IsNullOrWhiteSpace(symbol) || string.IsNullOrWhiteSpace(companyName))
        {
            return BadRequest("symbol and companyName are required");
        }

        var count = await _favoritesService.AddFavoriteSymbol(symbol, companyName);
        _memoryCache.Remove(CacheKey);
        return Ok(count);
    }

    /// <summary>
    /// Removes a symbol from the user's favorites
    /// </summary>
    /// <param name="symbol">Stock symbol to remove</param>
    /// <returns>Result of the remove operation</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Missing required parameter</response>
    [HttpDelete]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RemoveFavorite([FromQuery] string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return BadRequest("symbol is required");
        }

        var count = await _favoritesService.RemoveFavoriteSymbol(symbol);
        _memoryCache.Remove(CacheKey);
        return Ok(count);
    }

    /// <summary>
    /// Gets all favorite symbols with caching
    /// </summary>
    /// <returns>List of favorite symbols</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<FavoriteSymbolEntity>), 200)]
    public async Task<IActionResult> GetFavorites()
    {
        if (_memoryCache.TryGetValue(CacheKey, out List<FavoriteSymbolEntity>? cachedFavorites) && cachedFavorites is not null)
        {
            return Ok(cachedFavorites);
        }

        var favorites = await _favoritesService.GetFavoriteSymbols();
        _memoryCache.Set(CacheKey, favorites, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1),
        });
        return Ok(favorites);
    }
}
