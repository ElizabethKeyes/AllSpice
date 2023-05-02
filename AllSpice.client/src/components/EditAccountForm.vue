<template>
  <form @submit.prevent="editAccount()">
    <div class="modal-body row">
      <div class="col-12">
        <label for="name">Name</label>
        <input type="text" id="name" name="name" class="form-control" v-model="editable.Name">
      </div>
      <div class="col-12">
        <label for="picture" class="mt-2">Picture (Image URL)</label>
        <input type="url" id="picture" name="picture" class="form-control" v-model="editable.Picture">
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

    watchEffect(() => {
      if (AppState.account.id) {
        editable.value = { ...AppState.account }
      }
    })
    return {
      editable,

      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
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
</style>