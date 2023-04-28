import { Profile } from "./Account.js"
import { Recipe } from "./Recipe.js"

export class Favorite {
  constructor(data) {
    this.id = data.id
    this.recipeId = data.recipeId
    this.accountId = data.accountId
  }
}

// All favorite recipes for the logged in user
export class MyFavorite extends Recipe {
  constructor(data) {
    super(data)
    this.favoriteId = data.favoriteId
  }
}

// All profiles for a given favorite recipe
export class FavoriteProfile extends Profile {
  constructor(data) {
    super(data)
    this.favoriteId = data.favoriteId
  }
}