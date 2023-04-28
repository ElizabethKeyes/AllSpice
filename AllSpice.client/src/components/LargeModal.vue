<template>
  <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="recipeDetailsModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body row" v-if="recipe">
          <button type="button" class="btn-close" data-bs-dismiss="modal" data-bs-target="#recipeDetailsModal"
            aria-label="Close"></button>
          <div class="col-5">
            <img :src="recipe.img" :alt="'a photo of ' + recipe.title" class="recipe-photo">
          </div>
          <div class="col-7">
            <div class="d-flex align-items-center">
              <h2 class="green-text mt-4">{{ recipe.title }}</h2>
              <h6 class="category-card">{{ recipe.category }}</h6>
            </div>
            <section class="row pe-4 mt-5">
              <div class="col-6">
                <div class="details-cards">
                  <div class="card-title">
                    <h2>Recipe Steps</h2>
                  </div>
                  <div class="card-body">
                    <h6>{{ recipe.instructions }}</h6>
                  </div>
                </div>
              </div>
              <div class="col-6">
                <div class="details-cards">
                  <div class="card-title">
                    <h2>Ingredients</h2>
                  </div>
                  <div class="card-body">
                    <!-- TODO add ingredients once they've been retrieved. -->
                    <!-- <h6>{{ recipe. }}</h6> -->
                  </div>
                </div>
              </div>
            </section>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";

export default {
  setup() {
    return {
      recipe: computed(() => AppState.activeRecipe)
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

.modal-body {
  padding: 0px !important
}

.green-text {
  color: rgba(33, 150, 83, 1)
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

.details-cards {
  height: 40vh;
  background-color: #F0F4F2;
  border-radius: 5px;
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
}
</style>