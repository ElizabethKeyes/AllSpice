namespace AllSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@Title, @Instructions, @Img, @Category, @CreatorId);

    SELECT
    recipes.*,
    creator.*
    FROM recipes
    JOIN accounts creator ON recipes.creatorId = creator.Id
    WHERE recipes.id = LAST_INSERT_ID();
    ";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;
  }

  internal void EditRecipe(Recipe ogRecipe)
  {
    string sql = @"
    UPDATE recipes
    SET
    Title = @Title,
    Instructions = @Instructions,
    Img = @Img,
    Category = @Category
    WHERE id = @Id;
    ";
    _db.Execute(sql, ogRecipe);
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT
    recipes.*,
    creator.*
    FROM recipes
    JOIN accounts creator ON recipes.creatorId = creator.id
    ";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT
    recipes.*,
    creator.*
    FROM recipes
    JOIN accounts creator ON recipes.creatorId = creator.id
    WHERE recipes.id = @recipeId;
    ";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }
}
