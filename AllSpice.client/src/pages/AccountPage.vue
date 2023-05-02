<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="profile-pic" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <button class="btn btn-outline-dark" data-bs-toggle="modal" data-bs-target="#editAccountModal">Edit Account</button>
  </div>

  <SmallModal id="editAccountModal">
    <template #header>
      <h3>Edit Account Details</h3>
    </template>
    <template #body>
      <EditAccountForm />
    </template>
  </SmallModal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import SmallModal from "../components/SmallModal.vue";
import EditAccountForm from "../components/EditAccountForm.vue";
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: { SmallModal, EditAccountForm }
}
</script>

<style scoped>
.profile-pic {
  height: 100px;
  width: 100px;
  border-radius: 100%;
  object-fit: cover;
  object-position: center;
}
</style>
