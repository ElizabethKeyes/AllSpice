namespace AllSpice.Models;

public class Favorite
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
}

// All favorite recipes for the logged in user
public class MyFavorite : Recipe
{
  public int FavoriteId { get; set; }
}

// All profiles for a given favorite recipe
public class FavoriteProfile : Profile
{
  public int FavoriteId { get; set; }
}