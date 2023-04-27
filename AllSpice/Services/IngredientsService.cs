namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;

  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal string DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = this.GetIngredientById(ingredientId);
    if (ingredient.Recipe.CreatorId == userId)
    {
      int rowsAffected = _repo.DeleteIngredient(ingredientId);
      return $"The ingredient at id {ingredientId} has been removed from the recipe.";
    }
    else throw new Exception("You cannot delete ingredients from someone else's recipe.");
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetIngredientById(ingredientId);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }
}
