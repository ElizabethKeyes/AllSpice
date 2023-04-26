namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    return recipe;
  }
}
