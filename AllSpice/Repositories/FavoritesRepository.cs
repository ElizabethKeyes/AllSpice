namespace AllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (accountId, recipeId)
    VALUES
    (@AccountId, @RecipeId);

    SELECT *
    FROM favorites
    WHERE favorites.id = LAST_INSERT_ID();
    ";

    Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
    return favorite;
  }

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId;";
    _db.Execute(sql, new { favoriteId });
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal List<MyFavorite> GetMyFavorites(string userId)
  {
    string sql = @"
    SELECT
    favorites.*,
    accounts.*,
    recipes.*
    FROM favorites
    JOIN accounts ON accounts.id = @userId
    JOIN recipes ON recipes.id = favorites.recipeId
    WHERE accountId = @userId
    ;";
    List<MyFavorite> favorites = _db.Query<Favorite, Account, MyFavorite, MyFavorite>(sql, (favorite, account, myFavorite) =>
    {
      myFavorite.Creator = account;
      myFavorite.FavoriteId = favorite.Id;
      return myFavorite;
    }, new { userId }).ToList();
    return favorites;
  }
}
