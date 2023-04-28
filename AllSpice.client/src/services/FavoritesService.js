import { AppState } from "../AppState.js";
import { MyFavorite } from "../models/Favorite.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async getMyFavorites() {
    const res = await api.get("/account/favorites")
    AppState.myFavorites = res.data.map(f => new MyFavorite(f))
    logger.log("[appstate myFavorites]", AppState.myFavorites)
  }
}

export const favoritesService = new FavoritesService();