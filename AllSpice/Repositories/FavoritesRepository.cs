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

    SELECT
    favorites.*,
    accounts.*,
    recipes.*

    FROM favorites
    JOIN accounts ON accounts.id = favorites.accountId
    JOIN recipes ON recipes.id = favorites.recipeId
    WHERE favorites.id = LAST_INSERT_ID();
    ";

    Favorite favorite = _db.Query<Favorite, Account, Recipe, Favorite>(sql, (favorite, account, recipe) =>
    {
      favorite.Account = account;
      favorite.Recipe = recipe;
      return favorite;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }

  internal List<Favorite> GetFavoritesByAccountId(string userId)
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
    List<Favorite> favorites = _db.Query<Favorite, Account, Recipe, Favorite>(sql, (favorite, account, recipe) =>
    {
      favorite.Account = account;
      favorite.Recipe = recipe;
      return favorite;
    }, new { userId }).ToList();
    return favorites;
  }
}
