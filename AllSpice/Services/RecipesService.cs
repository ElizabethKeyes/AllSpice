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

  internal Recipe EditRecipe(Recipe recipeData, int recipeId, string userId)
  {
    Recipe ogRecipe = this.GetRecipeById(recipeId);
    if (ogRecipe.CreatorId == userId)
    {
      ogRecipe.Title = recipeData.Title ?? ogRecipe.Title;
      ogRecipe.Instructions = recipeData.Instructions ?? ogRecipe.Instructions;
      ogRecipe.Img = recipeData.Img ?? ogRecipe.Img;
      ogRecipe.Category = recipeData.Category ?? ogRecipe.Category;
      _repo.EditRecipe(ogRecipe);
      return ogRecipe;
    }
    else
    {
      throw new Exception("You are not authorized to edit another user's recipe.");
    }
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
