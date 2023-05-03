namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _auth;
  public CommentsController(Auth0Provider auth, CommentsService commentsService)
  {
    _auth = auth;
    _commentsService = commentsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Comment>> PostComment([FromBody] Comment commentData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      commentData.AccountId = userInfo.Id;
      Comment comment = _commentsService.PostComment(commentData);
      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
