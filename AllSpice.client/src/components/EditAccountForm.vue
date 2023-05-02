<template>
  <form @submit.prevent="editAccount()">
    <div class="modal-body row">
      <div class="col-12">
        <label for="name">Name</label>
        <input type="text" id="name" name="name" class="form-control" v-model="editable.name">
      </div>
      <div class="col-12">
        <label for="picture" class="mt-2">Picture (Image URL)</label>
        <input type="url" id="picture" name="picture" class="form-control" v-model="editable.picture">
      </div>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
      <button type="submit" class="btn submit-btn">Submit</button>
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    const editable = ref({})
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


<style lang="scss" scoped></style>