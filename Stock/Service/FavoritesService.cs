using Stock.Repository;

namespace Stock.Service;

public class FavoritesService(FavoritesRepository favoritesRepository)
{
    private readonly FavoritesRepository _favoritesRepository = favoritesRepository;

    public async Task<int> AddFavoriteSymbol(string symbol, string companyName)
    {
        return await _favoritesRepository.AddFavoriteSymbolAsync(symbol, companyName);
    }

    public async Task<int> RemoveFavoriteSymbol(string symbol)
    {
        return await _favoritesRepository.RemoveFavoriteSymbolAsync(symbol);
    }

    public async Task<IReadOnlyList<FavoriteSymbolEntity>> GetFavoriteSymbols()
    {
        return await _favoritesRepository.GetFavoriteSymbolsAsync();
    }
}
