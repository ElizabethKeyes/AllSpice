<template>
  <div class="modal-body row" v-if="recipe" :key="recipe?.id">
    <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal"
      aria-label="Close"></button>
    <!-- Recipe photo and favorites indicator -->
    <div class="col-md-5">
      <img :src="recipe.img" :alt="'a photo of ' + recipe.title" class="recipe-photo">
      <button v-if="isFavorite" class="favorites-card"><i class="mdi mdi-heart text-danger fav-btn fs-2"
          title="Remove Favorite" @click="removeFavorite()"></i></button>
      <button v-else class="favorites-card"><i class="mdi mdi-heart-outline text-dark fs-2 fav-btn" @click="addFavorite()"
          title="Add Favorite"></i>
      </button>
    </div>
    <!-- Recipe title -->
    <div class="col-md-7">
      <div class="d-flex align-items-center recipe-title">
        <h2 class="title-text mt-4">{{ recipe.title }}</h2>
        <h6 class="category-card">{{ recipe.category }}</h6>
      </div>
      <!-- Instructions/Ingredients Cards -->
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
                <button type="button" class="edit-text btn btn-outline-secondary"
                  v-if="recipe.creatorId == account.id && !edit" @click.stop="toggleEdit()">Edit
                  Instructions</button>
                <button type="button" class="edit-text btn btn-outline-secondary"
                  v-if="recipe.creatorId == account.id && edit" @click.stop="toggleEdit()">Cancel</button>
              </div>
            </div>
          </div>
          <button v-if="recipe.creatorId == account.id" class="btn btn-outline-danger mt-1 mb-md-0 mb-1"
            @click="deleteRecipe()">
            delete recipe</button>
        </div>

        <div class="col-md-6">
          <div class="details-cards elevation-3">
            <div class="card-title">
              <h2>Ingredients</h2>
            </div>
            <div class="card-body">
              <ul>
                <li v-for="i in ingredients" :key="i.id" class="ingredient"><span class="d-flex justify-content-between">
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
          <h6 class="text-end published-text">published by: {{ recipe.creator.name }}</h6>
        </div>
        <hr>
      </section>
    </div>
    <!-- Comments -->
    <div class="col-12">
      <div v-if="account.id">
        <h4 class="ms-3">Leave a Comment...</h4>
        <section class="row justify-content-center">
          <div class="col-2 d-flex justify-content-end">
            <img :src="account.picture" :alt="'a photo of ' + recipe.creator.name" class="profile-pic">
          </div>
          <div class="col-9">
            <form @submit.prevent="postComment()">
              <div class="d-flex justify-content-center">
                <textarea name="body" id="body" rows="5" class="form-control comment-box" placeholder="Leave a comment..."
                  v-model="editable.body"></textarea>
                <div class="d-flex align-items-center ms-2 mb-2">
                  <button type="submit" class="btn submit-btn-3"><i class="mdi mdi-check"></i></button>
                </div>
              </div>
            </form>
          </div>

        </section>
      </div>
      <h4 class="ms-3 mt-2 mb-3" v-if="comments.length">See What Others Are Saying...</h4>
      <section class="row justify-content-center">
        <div class="col-10" v-for="comment in comments" :key="comment.id">
          <CommentsComponent :comment="comment" />
        </div>
      </section>
    </div>
  </div>
</template>


<script>

import { computed, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { ingredientsService } from "../services/IngredientsService.js";
import Pop from "../utils/Pop.js";
import { favoritesService } from "../services/FavoritesService.js";
import { recipesService } from "../services/RecipesService.js";
import CommentsComponent from "./CommentsComponent.vue";
import { commentsService } from "../services/CommentsService.js";

export default {
  setup() {
    const editable = ref({});
    const editableInstructions = ref({});
    const isFavorite = computed(() => AppState.myFavorites.find(f => f.id == AppState.activeRecipe?.id));
    watchEffect(() => {
      if (AppState.activeRecipe) {
        editableInstructions.value = { ...AppState.activeRecipe };
      }
    });
    return {
      editable,
      editableInstructions,
      recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),
      edit: computed(() => AppState.edit),
      comments: computed(() => AppState.comments),
      isFavorite,
      async addIngredient() {
        try {
          const ingredient = editable.value;
          await ingredientsService.addIngredient(ingredient);
          await ingredientsService.getIngredientsByRecipeId(AppState.activeRecipe.id);
          editable.value = {};
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async addFavorite() {
        try {
          await favoritesService.addFavorite();
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async removeFavorite() {
        try {
          await favoritesService.removeFavorite(isFavorite.value);
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async editInstructions() {
        try {
          const instructions = editableInstructions.value.instructions;
          await recipesService.editInstructions(instructions);
          AppState.edit = false;
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async deleteIngredient(ingredientId) {
        try {
          if (await Pop.confirm("Are you sure you'd like to remove this ingredient?", "This cannot be undone", "Yes, I'm sure", "warning")) {
            await ingredientsService.deleteIngredient(ingredientId);
          }
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      async deleteRecipe() {
        try {
          if (await Pop.confirm("Are you sure you'd like to delete this recipe?", "This cannot be undone", "Yes, I'm sure", "warning")) {
            const recipeId = AppState.activeRecipe.id;
            await recipesService.deleteRecipe(recipeId);
          }
        }
        catch (error) {
          logger.log(error);
          Pop.error(error.message);
        }
      },
      toggleEdit() {
        AppState.edit = !AppState.edit;
      },

      async postComment() {
        try {
          const commentData = {}
          commentData.body = editable.value.body
          commentsService.postComment(commentData)
          editable.value.body = ""
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    };
  },
  components: { CommentsComponent }
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
  border: none;
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
  max-width: 12vw;
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

.submit-btn-3 {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
  max-height: 5vh;
}

.submit-btn:hover {
  border: solid 1px rgb(82, 115, 96);
}

.submit-btn-2:hover {
  border: solid 1px rgb(82, 115, 96);
}

.submit-btn-3:hover {
  border: solid 1px rgb(82, 115, 96);
}

.published-text {
  color: rgba(109, 109, 109, 1);
  margin-top: 1.25em;
  margin-bottom: .5em;
}

.comment-box {
  width: 90%;
  margin-bottom: .75em;
}

.profile-pic {
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
  height: 12vh;
  width: 12vh;
  margin-left: 1.5em;
}

h6 {
  pointer-events: none;

  i {
    pointer-events: all;
  }
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

  .profile-pic {
    height: 45px;
    width: 45px;
    margin-left: 0;
    margin-top: 3em;
  }
}
</style>