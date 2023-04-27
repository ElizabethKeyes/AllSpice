<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 p-3">
        <div class="banner">
          <Login class="login" />
          <h2 class="title-text">All-Spice</h2>
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
import Login from './Login.vue'
export default {
  setup() {
    return {}
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
  align-items: center;
  justify-content: center;
  position: relative;
}

.title-text {
  color: rgba(253, 253, 253, 1);
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
