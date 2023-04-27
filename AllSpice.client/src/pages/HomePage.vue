<template>
  <div class="container-fluid">
    <section class="row">
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

<style scoped lang="scss"></style>
