import { AppState } from "../AppState.js";
import { Favorite, MyFavorite } from "../models/Favorite.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async getMyFavorites() {
    const res = await api.get("/account/favorites")
    AppState.myFavorites = res.data.map(f => new MyFavorite(f))
  }

  // TODO need to correct the way favorites are added. They need to be classed as a myFavorite object to avoid issues.
  // TODO afterwards, need to ensure that the user's favorites are properly drawing from the favorites filter
  async addFavorite() {
    if (!AppState.account.id) {
      Pop.toast("You must be logged in to add favorites", "error", "top")
    } else {
      const recipeId = AppState.activeRecipe.id
      const res = await api.post("api/favorites", { recipeId })
      AppState.newFavorites.push(new Favorite(res.data))
    }
  }

  async removeFavorite() {
    let favoriteId = null
    let favoritesIndex = null
    // This finds the favorite by looking through new and existing favorites, then assigns the id to favoriteId
    let favorite = AppState.myFavorites.find(f => f.id == AppState.activeRecipe.id)
    if (favorite == null || undefined) {
      favorite = AppState.newFavorites.find(f => f.recipeId == AppState.activeRecipe.id)
      favoriteId = favorite.id
      favoritesIndex = AppState.newFavorites.findIndex(f => f.id == favoriteId)
      AppState.newFavorites.splice(favoritesIndex, 1)
    } else {
      favoriteId = favorite.favoriteId
      favoritesIndex = AppState.myFavorites.findIndex(f => f.favoriteId == favoriteId)
      AppState.myFavorites.splice(favoritesIndex, 1)
    }

    await api.delete(`api/favorites/${favoriteId}`)
  }
}

export const favoritesService = new FavoritesService();