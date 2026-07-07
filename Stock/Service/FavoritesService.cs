using Stock.Repository;

namespace Stock.Service;

public class FavoritesService(FavoritesRepository favoritesRepository)
{
    private readonly FavoritesRepository _favoritesRepository = favoritesRepository;

    public async Task<int> AddFavoriteSymbol(string symbol, string companyName, string assetType = "stock")
    {
        return await _favoritesRepository.AddFavoriteSymbolAsync(symbol, companyName, assetType);
    }

    public async Task<int> RemoveFavoriteSymbol(string symbol, string assetType = "stock")
    {
        return await _favoritesRepository.RemoveFavoriteSymbolAsync(symbol, assetType);
    }

    public async Task<IReadOnlyList<FavoriteSymbolEntity>> GetFavoriteSymbols()
    {
        return await _favoritesRepository.GetFavoriteSymbolsAsync();
    }
}
