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

  async deleteIngredient(ingredientId) {
    const res = await api.delete(`api/ingredients/${ingredientId}`)
    const foundIndex = AppState.ingredients.findIndex(i => i.id == ingredientId)
    AppState.ingredients.splice(foundIndex, 1)
  }
}

export const ingredientsService = new IngredientsService();