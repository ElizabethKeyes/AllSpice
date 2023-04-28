<template>
  <div class="recipe-card elevation-4" data-bs-toggle="modal" data-bs-target="#recipeDetailsModal"
    :style="{ backgroundImage: `url(${recipe.img})`, backgroundPosition: 'center', backgroundSize: 'cover' }">
    <div class="mt-1 d-flex justify-content-between">
      <h6 class="category-card">{{ recipe.category }}</h6>
      <h6 class="favorites-card"><i class="mdi mdi-heart-outline"></i></h6>
    </div>
    <div class="title-card">
      <h6>{{ recipe.title }}</h6>
      <h6>{{ recipe.instructions }}</h6>
    </div>
  </div>
  <LargeModal />
</template>


<script>
import LargeModal from "./LargeModal.vue";
import { Recipe } from "../models/Recipe.js";

export default {
  props: {
    recipe: {
      type: Recipe,
      required: true
    }
  },

  setup() {
    return {
    };
  },
  components: { LargeModal }
}
</script>


<style lang="scss" scoped>
.recipe-card {
  min-height: 30vh;
  margin-bottom: 1.5em;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  position: relative;
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
  margin-left: .5em;
  border-radius: 10px
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
  margin-bottom: .3em;
  margin-left: .3em;
  margin-right: .3em;
  border-radius: 10px;
}
</style>