<template>
  <div class="container-fluid">
    <section class="row">
      <div v-for="recipe in recipes" class="col-md-4">
        <div class="recipe-card elevation-4"
          :style="{ backgroundImage: `url(${recipe.img})`, backgroundPosition: 'center', backgroundSize: 'cover' }">
          <div class="mt-1">
            <h6 class="category-card">{{ recipe.category }}</h6>
          </div>
          <div class="title-card">
            <h6>{{ recipe.title }}</h6>
            <h6>{{ recipe.instructions }}</h6>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { computed, onMounted } from "vue"
import { AppState } from "../AppState.js"

export default {
  setup() {

    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes()
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      getAllRecipes();
    })

    return {
      recipes: computed(() => AppState.recipes),


    }
  }
}
</script>

<style scoped lang="scss">
.recipe-card {
  min-height: 25vh;
  margin-bottom: 1.5em;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
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
