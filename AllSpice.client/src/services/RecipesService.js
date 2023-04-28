import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {
  async getAllRecipes() {
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(r => new Recipe(r))
    if (AppState.account && AppState.recipes) {
      AppState.myRecipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id)
      logger.log("[My Recipes]", AppState.myRecipes)
    }
  }


}

export const recipesService = new RecipesService();