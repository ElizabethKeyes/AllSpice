<template>
  <div class="modal fade" id="recipeDetailsModal" aria-labelledby="recipeDetailsModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>