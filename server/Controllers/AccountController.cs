namespace postItSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly CollaboratorsService _collaboratorsService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService collaboratorsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _collaboratorsService = collaboratorsService;
    }

    [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("collaborators")]
  [Authorize]
  public async Task<ActionResult<List<CollaboratorAlbum>>> GetAccountCollaborators(){
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<CollaboratorAlbum> collabAlbums = _collaboratorsService.GetAccountCollaborators(userInfo.Id);
      return Ok(collabAlbums);
    }
   catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
