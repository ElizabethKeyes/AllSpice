import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Recipe.js').Recipe[]} */
  recipes: [],
  /** @type {import('./models/Recipe.js').Recipe[]} */
  myRecipes: [],
  /** @type {import('./models/MyFavorite.js').MyFavorite[]} */
  myFavorites: [],
  /** @type {import('./models/Favorite.js').Favorite[]} */
  newFavorites: [],
  /** @type {import('./models/Recipe.js').Recipe|null} */
  activeRecipe: null,
  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  ingredients: [],
  edit: false,
})
