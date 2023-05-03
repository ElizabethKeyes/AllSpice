namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _auth;
  public RecipesController(RecipesService recipesService, Auth0Provider auth, CommentsService commentsService)
  {
    _recipesService = recipesService;
    _auth = auth;
    _commentsService = commentsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string message = _recipesService.DeleteRecipe(userId, recipeId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> EditRecipeAsync([FromBody] Recipe recipeData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo?.Id;
      Recipe recipe = _recipesService.EditRecipe(recipeData, recipeId, userId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes(string query)
  {
    try
    {
      if (query == null)
      {
        List<Recipe> recipes = _recipesService.GetAllRecipes();
        return Ok(recipes);
      }
      else
      {
        List<Recipe> recipes = _recipesService.SearchRecipes(query);
        return Ok(recipes);
      }
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/comments")]
  public ActionResult<List<Comment>> GetCommentsByRecipeId(int recipeId)
  {
    try
    {
      List<Comment> comments = _commentsService.GetCommentsByRecipeId(recipeId);
      return Ok(comments);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _recipesService.GetIngredientsByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
