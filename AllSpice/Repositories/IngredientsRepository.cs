namespace AllSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId)
    VALUES
    (@Name, @Quantity, @RecipeId);
    
    SELECT * FROM ingredients WHERE id = LAST_INSERT_ID()";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal int DeleteIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId";
    int rowsAffected = _db.ExecuteScalar<int>(sql, new { ingredientId });
    return rowsAffected;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
    SELECT
    ingredients.*,
    recipes.*
    FROM ingredients
    JOIN recipes ON ingredients.recipeId = recipes.id
    WHERE ingredients.id = @ingredientId;";
    Ingredient ingredient = _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
    {
      ingredient.Recipe = recipe;
      return ingredient;
    }, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = "SELECT * FROM ingredients WHERE recipeId = @recipeId;";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
}
