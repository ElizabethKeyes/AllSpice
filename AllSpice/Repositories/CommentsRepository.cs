namespace AllSpice.Repositories;

public class CommentsRepository
{
  private readonly IDbConnection _db;

  public CommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Comment PostComment(Comment commentData)
  {
    string sql = @"
    INSERT INTO comments
    (accountId, recipeId, body)
    VALUES
    (@AccountId, @RecipeId, @Body);

    SELECT
    comments.*,
    accounts.*
    FROM comments
    JOIN accounts ON accounts.id = comments.accountId
    WHERE comments.id = LAST_INSERT_ID();
    ";

    Comment comment = _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
    {
      comment.Creator = account;
      return comment;
    }, commentData).FirstOrDefault();

    return comment;
  }
}
