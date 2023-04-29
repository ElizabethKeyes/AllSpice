import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class IngredientsService {
  async getIngredientsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.ingredients = res.data.map(i => new Ingredient(i))
  }

  async addIngredient(ingredient) {
    ingredient.recipeId = AppState.activeRecipe.id
    const res = await api.post(`api/ingredients`, ingredient)
  }
}

export const ingredientsService = new IngredientsService();