import { AppState } from "../AppState.js";
import { Comment } from "../models/Comment.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class CommentsService {
  async GetCommentsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/comments`)
    AppState.comments = res.data.map(c => new Comment(c))
    logger.log('[APPSTATE COMMENTS]', AppState.comments)
  }

  async postComment(commentData) {
    commentData.recipeId = AppState.activeRecipe.id
    const res = await api.post(`api/comments`, commentData)
    AppState.comments.push(new Comment(res.data))
  }
}

export const commentsService = new CommentsService();