<template>
  <div @click="setActiveRecipe(recipe.id)" class="recipe-card elevation-4" data-bs-toggle="modal"
    data-bs-target="#recipeDetailsModal"
    :style="{ backgroundImage: `url(${recipe.img})`, backgroundPosition: 'center', backgroundSize: 'cover' }">
    <div class="mt-1 d-flex justify-content-between">
      <h6 class="category-card">{{ recipe.category }}</h6>
      <h6 v-if="isFavorite(recipe.id)" class="favorites-card"><i class="mdi mdi-heart text-danger fs-4"></i></h6>
      <h6 v-else class="favorites-card"><i class="mdi mdi-heart-outline text-dark fs-4"></i></h6>
    </div>
    <div class="title-card">
      <h6 class="fw-bold mb-0">{{ recipe.title }}</h6>
    </div>
  </div>
  <LargeModal id="recipeDetailsModal">
    <RecipeDetails />
  </LargeModal>
</template>


<script>
import LargeModal from "./LargeModal.vue";
import { Recipe } from "../models/Recipe.js";
import { AppState } from "../AppState.js";
import { computed } from "vue";
import { recipesService } from "../services/RecipesService.js";
import RecipeDetails from "./RecipeDetails.vue";

export default {
  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  setup() {
    return {
      myFavorites: computed(() => { AppState.myFavorites }),

      isFavorite(recipeId) {
        if (AppState.myFavorites.find(f => f.id == recipeId)) {
          return true
        } else return false
      },

      setActiveRecipe(recipeId) {
        recipesService.setActiveRecipe(recipeId)
      }
    };
  },
  components: { LargeModal, RecipeDetails }
}
</script>


<style lang="scss" scoped>
.recipe-card {
  min-height: 35vh;
  margin-bottom: 3em;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  position: relative;
  border-radius: 5px;
}

.recipe-card:hover {
  transform: scale(1.025);
  transition: all .1s ease-in-out;
  cursor: pointer;
}

.category-card {
  background-color: rgba(126, 126, 126, 0.6);
  color: rgba(253, 253, 253, 1);
  display: inline;
  padding-left: .35em;
  padding-right: .35em;
  padding-top: .25em;
  padding-bottom: .25em;
  margin-left: .5em;
  border-radius: 10px;
  font-weight: bold;
}

.favorites-card {
  background-color: rgba(126, 126, 126, 0.6);
  color: rgba(253, 253, 253, 1);
  display: inline;
  margin-right: .5em;
  padding-left: .35em;
  padding-right: .35em;
  border-top-left-radius: 0px;
  border-top-right-radius: 0px;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
  position: absolute;
  top: 0px;
  right: 5px;
}

.title-card {
  background-color: rgba(126, 126, 126, 0.6);
  color: rgba(253, 253, 253, 1);
  padding-left: .35em;
  padding-right: .35em;
  padding-top: .25em;
  padding-bottom: .25em;
  margin-bottom: .3em;
  margin-left: .3em;
  margin-right: .3em;
  border-radius: 10px;

}
</style>