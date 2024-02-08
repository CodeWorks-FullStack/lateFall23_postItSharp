namespace postItSharp.Controllers;

[ApiController]
[Route("api/pictures")]
public class PicturesController : ControllerBase
{
  private readonly PicturesService picturesService;
  private readonly Auth0Provider auth;
  public PicturesController(PicturesService picturesService, Auth0Provider auth)
  {
    this.picturesService = picturesService;
    this.auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id; // SET creatorId to who is making the request
      Picture picture = picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
   catch (Exception error)
      {
        return BadRequest(error.Message);
      }
  }

  [HttpDelete("{pictureId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeletePicture(int pictureId)
  {
    try
    {
      Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
      string message = picturesService.DeletePicture(pictureId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception error)
      {
        return BadRequest(error.Message);
      }
  }
}