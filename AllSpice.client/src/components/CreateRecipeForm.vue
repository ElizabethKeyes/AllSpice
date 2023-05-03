<template>
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
          v-model="editable.img" @input="previewImage()">
        <div class="d-flex justify-content-center">
          <img :src="imagePreview" v-if="imagePreview" class="image-preview" alt="The selected photo for your recipe">
        </div>
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
</template>


<script>
import { ref, watchEffect } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const editable = ref({})
    const imagePreview = ref(null)

    watchEffect(() => {
      if (AppState.activeRecipe) {
        imagePreview.value = AppState.activeRecipe.img
      }
    })
    return {
      editable,
      imagePreview,

      async postRecipe() {
        try {
          const recipeData = editable.value
          await recipesService.postRecipe(recipeData)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      previewImage() {
        imagePreview.value = editable.value.img
        logger.log('changing the image preview', imagePreview.value, editable.value.img)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.submit-btn {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
}

.submit-btn:hover {
  border: solid 1px rgb(82, 115, 96);
}

.image-preview {
  object-fit: cover;
  object-position: center;
  max-height: 40vh;
  max-width: 100%;
  margin-top: .5em;
}
</style>