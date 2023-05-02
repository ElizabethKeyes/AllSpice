<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 p-3">
        <div class="banner elevation-3">
          <form @submit.prevent="searchRecipes()">
            <input type="text" class="form-control search-bar" placeholder="Search..." v-model="editable.search">
          </form>
          <Login class="login" />
          <h1 class="title-text">All-Spice</h1>
          <h5 class="title-text">Cherish Your Family<br>And Their Cooking</h5>
        </div>
      </div>
    </section>
  </div>

  <!-- <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3 mb-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul> -->
  <!-- LOGIN COMPONENT HERE -->
  <!-- <Login />
    </div>
  </nav> -->
</template>

<script>
import { ref } from "vue";
import Login from './Login.vue'
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async searchRecipes() {
        try {
          const query = editable.value.search
          await recipesService.searchRecipes(query)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
.banner {
  background-image: url(../assets/img/bannerImg.png);
  background-position: center;
  background-size: cover;
  height: 40vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  position: relative;
  border-radius: 5px;
}

.search-bar {
  position: fixed;
  top: 28px;
  right: 86px;
  width: 230px;
}

.title-text {
  color: rgba(253, 253, 253, 1);
  font-family: 'Sahitya', serif;
  filter: drop-shadow(0px 2px 3px rgba(0, 0, 0, 1))
}

.login {
  position: absolute;
  top: 10px;
  right: 10px
}



a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}


@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
