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
    const foundIndex = AppState.recipes.findIndex(r => r.id == recipeId)
    AppState.recipes.splice(foundIndex, 1)
    Modal.getOrCreateInstance("#recipeDetailsModal").hide()
  }

  async postRecipe(recipeData) {
    const res = await api.post(`api/recipes`, recipeData)
    const createdRecipe = new Recipe(res.data)
    AppState.activeRecipe = createdRecipe
    AppState.recipes.push(createdRecipe)
    Modal.getOrCreateInstance("#createRecipeModal").hide()
    Pop.toast("Your recipe has been created!", "success", "top")
    window.scrollTo({ left: 0, top: document.body.scrollHeight, behavior: "smooth" });
  }

  async searchRecipes(query) {
    logger.log('[SEARCHING FROM THE SERVICE]', query)
    const res = await api.get(`api/recipes?query=${query}`)
    AppState.recipes = res.data.map(r => new Recipe(r))
  }


}

export const recipesService = new RecipesService();