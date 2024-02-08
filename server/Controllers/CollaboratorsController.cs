namespace postItSharp.Controllers;



[ApiController]
[Route("api/collaborators")]
public class CollaboratorsController : ControllerBase
{

  private readonly CollaboratorsService collaboratorsService;
  private readonly Auth0Provider auth;

    public CollaboratorsController(CollaboratorsService collaboratorsService, Auth0Provider auth)
    {
        this.collaboratorsService = collaboratorsService;
        this.auth = auth;
    }



  [HttpPost]
  [Authorize]
  public async Task<ActionResult<CollaboratorAccount>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
      collaboratorData.AccountId = userInfo.Id;
      CollaboratorAccount collabAccount = collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collabAccount);
    }
     catch (Exception error)
      {
        return BadRequest(error.Message);
      }
  }
}