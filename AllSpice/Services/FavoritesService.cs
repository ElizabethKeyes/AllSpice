namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _favoritesRepository.CreateFavorite(favoriteData);
    return favorite;
  }
}
