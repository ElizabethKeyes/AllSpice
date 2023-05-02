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

  async removeFavorite(favorite) {
    await api.delete(`api/favorites/${favorite.favoriteId}`)
    const index = AppState.myFavorites.findIndex(f => f.favoriteId == favorite.favoriteId)
    AppState.myFavorites.splice(index, 1)
  }
}

export const favoritesService = new FavoritesService();