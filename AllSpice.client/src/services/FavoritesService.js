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

  async addFavorite() {
    if (!AppState.account.id) {
      Pop.toast("You must be logged in to add favorites", "error", "top")
    } else {
      const recipeId = AppState.activeRecipe.id
      const res = await api.post("api/favorites", { recipeId })
      const newFavorite = new MyFavorite(AppState.activeRecipe)
      newFavorite.favoriteId = res.data.id
      AppState.myFavorites.push(newFavorite)
    }
  }

  async removeFavorite() {
    let favoriteId = null
    let favoritesIndex = null
    let favorite = AppState.myFavorites.find(f => f.id == AppState.activeRecipe.id)
    favoriteId = favorite.favoriteId
    favoritesIndex = AppState.myFavorites.findIndex(f => f.favoriteId == favoriteId)
    AppState.myFavorites.splice(favoritesIndex, 1)
    await api.delete(`api/favorites/${favoriteId}`)
  }
}

export const favoritesService = new FavoritesService();