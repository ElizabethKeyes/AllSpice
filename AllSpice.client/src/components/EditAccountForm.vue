<template>
  <form @submit.prevent="editAccount()">
    <div class="modal-body row">
      <div class="col-12">
        <label for="name">Name</label>
        <input type="text" id="name" name="name" class="form-control" v-model="editable.name">
      </div>
      <div class="col-12">
        <label for="picture" class="mt-2">Picture (Image URL)</label>
        <input type="url" id="picture" name="picture" class="form-control" v-model="editable.picture"
          @input="changePreview()">
        <div class="d-flex justify-content-center mt-3">
          <img :src="imagePreview" v-if="imagePreview" alt="Your profile photo" class="profile-pic">
        </div>
      </div>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
      <button type="submit" class="btn submit-btn">Submit</button>
    </div>
  </form>
</template>


<script>
import { ref, watchEffect } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const editable = ref({})
    const imagePreview = ref(null)

    watchEffect(() => {
      if (AppState.account.id) {
        editable.value = { ...AppState.account }
        imagePreview.value = AppState.account.picture
      }
    })
    return {
      editable,
      imagePreview,

      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      changePreview() {
        imagePreview.value = editable.value.picture
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.submit-btn {
  background-color: rgb(82, 115, 96);
  color: rgba(244, 244, 244, 1);
}

.profile-pic {
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
  height: 20vh;
  width: 20vh;
}
</style>