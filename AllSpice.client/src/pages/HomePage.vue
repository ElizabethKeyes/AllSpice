<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-md-6 relative-col">
        <div class="filter-bar elevation-3">
          <button class="btn filter-btn" :class="{ selectedUnderline: filterCategory == c.toLowerCase() }"
            v-for="c in categories" @click="changeFilterCategory(c)">{{ c }}</button>
        </div>
      </div>
    </section>
    <section class="row mt-5 px-3">
      <div v-for="(recipe, i) in recipes" :key="recipe.id" class="col-md-4 px-4">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>
  <button v-if="account.id" class="btn btn-dark rounded-pill post-btn" data-bs-toggle="modal"
    data-bs-target="#createRecipeModal">Post a
    Recipe</button>

  <SmallModal id="createRecipeModal">
    <template #header>
      <h3>New Recipe</h3>
    </template>
    <template #body>
      <CreateRecipeForm />
    </template>
  </SmallModal>
</template>

<script>
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { favoritesService } from "../services/FavoritesService.js"
import { ingredientsService } from "../services/IngredientsService.js"
import { computed, onMounted, ref, watchEffect } from "vue"
import { AppState } from "../AppState.js"
import RecipeCard from "../components/RecipeCard.vue"
import SmallModal from "../components/SmallModal.vue"
import CreateRecipeForm from "../components/CreateRecipeForm.vue"

export default {
  setup() {
    const filterCategory = ref('')
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error.message);
      }
    }

    async function getMyFavorites() {
      try {
        await favoritesService.getMyFavorites()
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }

    async function getIngredientsByRecipeId(recipeId) {
      try {
        await ingredientsService.getIngredientsByRecipeId(recipeId)
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }

    watchEffect(() => {
      if (AppState.account.id) {
        getMyFavorites();
      }
    })

    watchEffect(() => {
      if (AppState.activeRecipe) {
        AppState.ingredients = []
        getIngredientsByRecipeId(AppState.activeRecipe.id)
      }
    })


    onMounted(() => {
      getAllRecipes();
    });


    return {
      filterCategory,
      categories: ["Home", "My Recipes", "Favorites"],
      account: computed(() => AppState.account),
      recipes: computed(() => {
        if (!filterCategory.value || filterCategory.value == "home") {
          return AppState.recipes
        } else if (filterCategory.value == "my recipes") {
          return AppState.recipes.filter(r => r.creatorId == AppState.account.id)
        } else if (filterCategory.value == "favorites") {
          return AppState.myFavorites
        }
      }),

      changeFilterCategory(categoryName) {
        filterCategory.value = categoryName.toLowerCase()
      }
    };
  },
  components: { RecipeCard, SmallModal, CreateRecipeForm }
}
</script>

<style scoped lang="scss">
.relative-col {
  position: relative
}

.filter-bar {
  background-color: rgba(252, 252, 252, 1);
  display: flex;
  justify-content: space-evenly;
  position: absolute;
  bottom: -7px;
  left: 0px;
  right: 0px;
  height: 6vh;
  border-radius: 5px;
}

.filter-btn {
  color: rgb(28 131 72);
  border-bottom-right-radius: 0px;
  border-bottom-left-radius: 0px;
  font-family: 'Sahitya', serif;
  font-size: large;
}

.selectedUnderline {
  color: rgba(33, 150, 83, 1);
  border-bottom-right-radius: 0px;
  border-bottom-left-radius: 0px;
  border-bottom: solid 5px rgba(33, 150, 83, 1)
}

.filter-btn:hover {
  color: rgba(33, 150, 83, 1);
  border-bottom-right-radius: 0px;
  border-bottom-left-radius: 0px;
  border-bottom: solid 5px rgba(33, 150, 83, 1)
}

.post-btn {
  position: fixed;
  bottom: 15px;
  right: 15px;
}
</style>
