namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  private readonly IngredientsService _ingredientsService;
  public RecipesService(RecipesRepository repo, IngredientsService ingredientsService)
  {
    _repo = repo;
    _ingredientsService = ingredientsService;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal string DeleteRecipe(string userId, int recipeId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId == userId)
    {
      int rowsAffected = _repo.DeleteRecipe(recipeId);
      return $"{recipe.Title} has been successfully deleted.";
    }
    else
    {
      throw new Exception("You do not have permission to delete another user's recipe.");
    }
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

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("No recipe found at id @recipeId");
    }
    return recipe;
  }
}
