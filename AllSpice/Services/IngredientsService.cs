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
    // TODO Should check that userId match the creatorId on the recipe. Will need to add recipe virtual to ingredient.
    // Make GetIngredientById(), then use the recipeId from that ingredient to GetRecipeById()
    // Check that the userId matches the recipe.creatorId.
    int rowsAffected = _repo.DeleteIngredient(ingredientId);
    return $"The ingredient at id {ingredientId} has been removed from the recipe.";
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }
}
