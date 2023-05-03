<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="footer-bg text-light">
    Made with 🌈 by Elizabeth
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  font-family: 'Roboto', sans-serif;
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.footer-bg {
  background-color: rgb(33, 150, 83)
}
</style>
