<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-md-6 relative-col">
        <div class="filter-bar elevation-3">
          <button class="btn filter-btn">
            Home
          </button>
          <button class="btn filter-btn">
            My Recipes
          </button>
          <button class="btn filter-btn">
            Favorites
          </button>
        </div>
      </div>
    </section>
    <section class="row mt-5">
      <RecipeCard />
    </section>
  </div>
</template>

<script>
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { computed, onMounted } from "vue"
import { AppState } from "../AppState.js"
import RecipeCard from "../components/RecipeCard.vue"

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getAllRecipes();
    });
    return {
    };
  },
  components: { RecipeCard }
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
  color: rgba(33, 150, 83, 1);
  border-bottom-right-radius: 0px;
  border-bottom-left-radius: 0px;
}

.filter-btn:hover {
  color: rgba(33, 150, 83, 1);
  border-bottom-right-radius: 0px;
  border-bottom-left-radius: 0px;
  border-bottom: solid 5px rgba(33, 150, 83, 1)
}
</style>
