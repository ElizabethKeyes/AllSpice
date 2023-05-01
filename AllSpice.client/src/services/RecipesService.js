import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class RecipesService {
  async getAllRecipes() {
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(r => new Recipe(r))
    if (AppState.account && AppState.recipes) {
      AppState.myRecipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id)
    }
  }

  setActiveRecipe(recipeId) {
    AppState.activeRecipe = AppState.recipes.find(r => r.id == recipeId)
  }

  async editInstructions(instructions) {
    const res = await api.put(`api/recipes/${AppState.activeRecipe.id}`, { instructions })
    AppState.activeRecipe.instructions = res.data.instructions
    Pop.toast("Instructions have been edited!", "success", "top")
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete(`api/recipes/${recipeId}`)
    logger.log(res.data)
    const foundIndex = AppState.recipes.findIndex(r => r.id == recipeId)
    AppState.recipes.splice(foundIndex, 1)
    Modal.getOrCreateInstance("#recipeDetailsModal").hide()
  }


}

export const recipesService = new RecipesService();