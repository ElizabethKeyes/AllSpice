<template>
  <div class="comments-card elevation-3 row">
    <div class="col-2 d-flex align-items-center">
      <img :src="comment.creator.picture" :alt="'a photo of ' + comment.creator.name" class="profile-pic">
    </div>
    <div class="col-10">
      {{ comment.body }}
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Comment } from "../models/Comment.js";

export default {
  props: {
    comment: { type: Comment, required: true }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped>
.comments-card {
  min-height: 15vh;
  border-radius: 5px;
  margin-bottom: 1.5em;

}

.profile-pic {
  object-fit: cover;
  object-position: center;
  height: 10vh;
  width: 10vh;
  border-radius: 100%;
}
</style>