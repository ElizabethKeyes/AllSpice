<template>
  <div class="modal fade" id="createRecipeModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false"
    role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header text-light">
          <h3 class="modal-title" id="modalTitleId">
            <slot name="header"></slot>
            <!-- New Recipe -->
          </h3>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <slot name="body"></slot>

      </div>
    </div>
  </div>
</template>


<script>

export default {
  setup() {
    return {


    }
  }
}
</script>


<style lang="scss" scoped>
.modal-header {
  background-color: rgba(82, 115, 96, 1);
  font-family: 'Sahitya', serif;
  padding-top: .5em;
  padding-bottom: .5em;
}
</style>