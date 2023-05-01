<template>
  <div class="modal fade" id="createRecipeModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false"
    role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header text-light">
          <h3 class="modal-title" id="modalTitleId">New Recipe</h3>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="postRecipe()">
          <div class="modal-body row">
            <div class="col-6">
              <label for="title">Title</label>
              <input type="text" id="title" name="title" class="form-control" minlength="5" maxlength="25" required
                v-model="editable.title">
            </div>
            <div class="col-6">
              <label for="category">Category</label>
              <select name="category" id="category" class="form-control" required v-model="editable.category">
                <option disabled selected>--choose a category--</option>
                <option value="Cheese">Cheese</option>
                <option value="Specialty Coffee">Specialty Coffee</option>
                <option value="Mexican">Mexican</option>
                <option value="Italian">Italian</option>
                <option value="Soup">Soup</option>
              </select>
            </div>
            <div class="col-12">
              <label for="imgUrl" class="mt-2">Image URL</label>
              <input type="url" id="imgUrl" name="imgUrl" class="form-control" required minlength="5" maxlength="500"
                v-model="editable.img">
            </div>
            <div class="col-12">
              <label for="instructions" class="mt-2">Instructions</label>
              <input type="text" id="instructions" name="instructions" class="form-control" required minlength="5"
                maxlength="250" v-model="editable.instructions">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
            <button type="submit" class="btn submit-btn">Submit</button>
          </div>
        </form>

      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async postRecipe() {
        try {
          const recipeData = editable.value
          await recipesService.postRecipe(recipeData)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-header {
  background-color: rgba(82, 115, 96, 1);
  font-family: 'Sahitya', serif;
  padding-top: .5em;
  padding-bottom: .5em;
}

.submit-btn {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
}
</style>