<template>
  <div class="modal fade" @blur="editFalse()" id="recipeDetailsModal" aria-labelledby="recipeDetailsModalLabel"
    data-bs-backdrop="false" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body row" v-if="recipe">
          <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal"
            aria-label="Close"></button>
          <!-- Recipe photo and favorites indicator -->
          <div class="col-md-5">
            <img :src="recipe.img" :alt="'a photo of ' + recipe.title" class="recipe-photo">
            <h6 v-if="isFavorite(recipe.id)" class="favorites-card"><i class="mdi mdi-heart text-danger fav-btn fs-2"
                title="Remove Favorite" @click="removeFavorite()"></i></h6>
            <h6 v-else class="favorites-card"><i class="mdi mdi-heart-outline text-dark fs-2 fav-btn"
                @click="addFavorite()" title="Add Favorite"></i>
            </h6>
          </div>
          <!-- Recipe title -->
          <div class="col-md-7">
            <div class="d-flex align-items-center recipe-title">
              <h2 class="title-text mt-4">{{ recipe.title }}</h2>
              <h6 class="category-card">{{ recipe.category }}</h6>
            </div>
            <!-- Steps/Ingredients Cards -->
            <section class="row details-cards-row">
              <div class="col-md-6">
                <div class="details-cards elevation-3">
                  <div class="card-title">
                    <h2>Instructions</h2>
                  </div>
                  <div class="card-body">
                    <h6 v-if="edit == false">{{ recipe.instructions }}</h6>

                    <form v-else @submit.prevent="editInstructions()">
                      <label for="instructions">Edit Instructions:</label>
                      <textarea name="instructions" id="instructions" rows="5" class="form-control"
                        v-model="editableInstructions.instructions"></textarea>
                      <div class="d-flex justify-content-end">
                        <button class="btn submit-btn-2" title="Submit"><i class="mdi mdi-check"></i></button>
                      </div>
                    </form>

                    <div class="d-flex justify-content-end">
                      <p class="edit-text text-secondary" v-if="recipe.creatorId == account.id && edit == false"
                        @click="toggleEdit()">Edit
                        Instructions</p>
                      <p class="edit-text text-secondary" v-if="recipe.creatorId == account.id && edit == true"
                        @click="toggleEdit()">Cancel</p>
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-md-6">
                <div class="details-cards elevation-3">
                  <div class="card-title">
                    <h2>Ingredients</h2>
                  </div>
                  <div class="card-body">
                    <ul>
                      <li v-for="i in ingredients" :key="i.id" class="ingredient"><span
                          class="d-flex justify-content-between">
                          {{ i.quantity }} {{ i.name }}<span><i class="mdi mdi-minus text-danger selectable"
                              title="Delete Ingredient" v-if="recipe.creatorId == account.id"
                              @click="deleteIngredient(i.id)"></i></span>
                        </span>
                      </li>
                    </ul>
                    <form v-if="recipe.creatorId == account.id" @submit.prevent="addIngredient()" class="row">
                      <div class="col-7 pe-0">
                        <label for="ingredient" class="ps-1">Add Ingredient...</label>
                        <input type="text" id="ingredient" minlength="3" maxlength="25" name="ingredient"
                          class="form-control ingredient-input" v-model="editable.name" required>
                      </div>
                      <div class="col-3 px-0">
                        <label for="quantity">Quantity</label>
                        <input type="text" id="quantity" minlength="3" maxlength="15" name="quantity"
                          class="form-control qty-input" v-model="editable.quantity" required>
                      </div>
                      <div class="col-1 px-0 pt-4">
                        <button class="btn submit-btn" title="Submit"><i class="mdi mdi-check"></i></button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
              <div class="published-text">
                <h6 class="mb-0 d-flex justify-content-between"><span v-if="recipe.creatorId == account.id"
                    class="text-danger selectable" @click="deleteRecipe()">delete
                    recipe</span><span>published by: {{ recipe.creator.name }}</span></h6>
              </div>

            </section>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref, watch, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { ingredientsService } from "../services/IngredientsService.js";
import Pop from "../utils/Pop.js";
import { favoritesService } from "../services/FavoritesService.js";
import { recipesService } from "../services/RecipesService.js";

export default {
  setup() {
    const editable = ref({})
    const editableInstructions = ref({})

    watchEffect(() => {
      if (AppState.activeRecipe) {
        editableInstructions.value = { ...AppState.activeRecipe }
      }
    })

    return {
      editable,
      editableInstructions,
      recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),
      edit: computed(() => AppState.edit),

      isFavorite(recipeId) {
        if (AppState.myFavorites.find(f => f.id == recipeId)) {
          return true
        } else return false
      },

      async addIngredient() {
        try {
          const ingredient = editable.value
          await ingredientsService.addIngredient(ingredient)
          await ingredientsService.getIngredientsByRecipeId(AppState.activeRecipe.id)
          editable.value = {}
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async addFavorite() {
        try {
          await favoritesService.addFavorite()
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async removeFavorite() {
        try {
          await favoritesService.removeFavorite()
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async editInstructions() {
        try {
          const instructions = editableInstructions.value.instructions
          await recipesService.editInstructions(instructions)
          AppState.edit = false
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async deleteIngredient(ingredientId) {
        try {
          if (await Pop.confirm("Are you sure you'd like to remove this ingredient?", "This cannot be undone", "Yes, I'm sure", "warning")) {
            await ingredientsService.deleteIngredient(ingredientId)
          }
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async deleteRecipe() {
        try {
          if (await Pop.confirm("Are you sure you'd like to delete this recipe?", "This cannot be undone", "Yes, I'm sure", "warning")) {
            const recipeId = AppState.activeRecipe.id
            await recipesService.deleteRecipe(recipeId)
          }
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      editFalse() {
        AppState.edit = false
      },

      toggleEdit() {
        AppState.edit = !AppState.edit
      },

    }
  }
}
</script>


<style lang="scss" scoped>
.btn-close {
  position: absolute;
  top: 8px;
  right: 17px
}

.modal-body {
  position: relative;
}

.recipe-photo {
  object-fit: cover;
  object-position: center;
  height: 60vh;
  width: 100%;
  border-top-left-radius: 0.45rem;
  border-bottom-left-radius: 0.45rem;
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
  left: 420px;
}

.fav-btn:hover {
  cursor: pointer;
}

.modal-body {
  padding: 0px !important
}

.title-text {
  color: rgba(33, 150, 83, 1);
  max-width: 80%;
}

.category-card {
  background-color: rgba(126, 126, 126, 0.6);
  color: rgba(253, 253, 253, 1);
  display: inline;
  padding-left: .35em;
  padding-right: .35em;
  margin-left: 1em;
  margin-top: 1em;
  border-radius: 10px;
  height: 3vh;
}

.details-cards-row {
  padding-right: 1.5em;
  margin-top: 3em;
}

.details-cards {
  height: 40vh;
  background-color: #F0F4F2;
  border-radius: 5px;
  width: 100%
}

.card-title {
  background-color: rgba(82, 115, 96, 1);
  color: rgba(244, 244, 244, 1);
  height: 7vh;
  display: flex;
  justify-content: center;
  align-items: center;
  border-top-left-radius: 5px;
  border-top-right-radius: 5px
}

.card-body {
  padding: 0.5em;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  height: 80%
}

.edit-text {
  margin-bottom: 0;
  max-width: 10vw;
}

.edit-text:hover {
  cursor: pointer;
}

.ingredient-input {
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
  border: 1px solid rgb(82, 115, 96) !important;
}

.qty-input {
  border-radius: 0px;
  border: 1px solid rgb(82, 115, 96) !important;
}

.submit-btn {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
}

.submit-btn-2 {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
  margin-top: .5em;
}

.submit-btn:hover {
  border: solid 1px rgb(82, 115, 96);
}

.submit-btn-2:hover {
  border: solid 1px rgb(82, 115, 96);
}

.published-text {
  text-align: end;
  color: rgba(109, 109, 109, 1);
  margin-top: 1.25em;
  margin-bottom: .5em;
}


@media screen and (max-width: 768px) {
  .recipe-photo {
    object-fit: cover;
    object-position: center;
    height: 40vh;
    width: 100%;
    border-top-left-radius: 0.45rem;
    border-top-right-radius: 0.45rem;
    border-bottom-left-radius: 0rem;
  }

  .favorites-card {
    position: absolute;
    top: 0px;
    left: 30px;
  }

  .recipe-title {
    flex-direction: column;
  }

  .title-text {
    color: rgba(33, 150, 83, 1);
    max-width: 100%;
    margin-bottom: 0;
    padding-left: .5em;
    padding-right: .5em;
  }

  .category-card {
    margin-top: .5em
  }

  .details-cards-row {
    justify-content: center;
    margin-top: 1.5em;
    padding-right: .5em;
    padding-left: .5em;
  }

  .details-cards {
    margin-bottom: 1em;
  }

  .edit-text {
    margin-bottom: 0;
    max-width: 100vw;
  }

  .published-text {
    margin-bottom: .5em;
    padding-right: 1em;
  }


}
</style>