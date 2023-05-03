namespace AllSpice.Services;

public class CommentsService
{
  private readonly CommentsRepository _repo;

  public CommentsService(CommentsRepository repo)
  {
    _repo = repo;
  }

  internal Comment PostComment(Comment commentData)
  {
    Comment comment = _repo.PostComment(commentData);
    return comment;
  }
}
