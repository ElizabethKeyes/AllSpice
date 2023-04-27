namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;

  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repo.CreateFavorite(favoriteData);
    return favorite;
  }

  internal string DeleteFavorite(int favoriteId, string userId)
  {
    Favorite favorite = this.GetFavoriteById(favoriteId);
    if (favorite.AccountId == userId)
    {
      _repo.DeleteFavorite(favoriteId);
      string message = $"The favorite at id {favoriteId} has been removed.";
      return message;
    }
    else throw new Exception("You cannot remove someone else's favorite");
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repo.GetFavoriteById(favoriteId);
    return favorite;
  }

  internal List<MyFavorite> GetMyFavorites(string userId)
  {
    List<MyFavorite> favorites = _repo.GetMyFavorites(userId);
    return favorites;
  }
}
