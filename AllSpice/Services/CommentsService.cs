namespace AllSpice.Services;

public class CommentsService
{
  private readonly CommentsRepository _repo;

  public CommentsService(CommentsRepository repo)
  {
    _repo = repo;
  }

  internal string DeleteComment(string userId, int commentId)
  {
    Comment comment = GetCommentById(commentId);
    if (comment.AccountId != userId)
    {
      throw new Exception("You are not permitted to delete another user's comment.");
    }
    _repo.DeleteComment(commentId);
    return $"The comment at Id: {commentId} has been deleted.";
  }

  internal Comment GetCommentById(int commentId)
  {
    Comment comment = _repo.GetCommentById(commentId);
    return comment;
  }

  internal List<Comment> GetCommentsByRecipeId(int recipeId)
  {
    List<Comment> comments = _repo.GetCommentsByRecipeId(recipeId);
    return comments;
  }

  internal Comment PostComment(Comment commentData)
  {
    Comment comment = _repo.PostComment(commentData);
    return comment;
  }
}
