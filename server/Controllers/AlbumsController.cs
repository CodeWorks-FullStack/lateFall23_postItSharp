namespace postItSharp.Controllers;




[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase{

  private readonly AlbumsService albumsService;
  private readonly PicturesService picturesService;
  private readonly CollaboratorsService collaboratorsService;
  private readonly Auth0Provider auth;

    public AlbumsController(Auth0Provider auth, AlbumsService albumsService, PicturesService picturesService, CollaboratorsService collaboratorsService)
    {
        this.auth = auth;
        this.albumsService = albumsService;
        this.picturesService = picturesService;
        this.collaboratorsService = collaboratorsService;
    }


    [HttpGet]
    public ActionResult<List<Album>> GetAllAlbums()
    {
      try
      {
        List<Album> albums = albumsService.GetAllAlbums();
        return Ok(albums);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{albumId}")]
    public ActionResult<Album> GetAlbumById(int albumId)
    {
      try
      {
        Album album = albumsService.GetAlbumById(albumId);
        return Ok(album);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
    {
      try
      {
        Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
        albumData.CreatorId = userInfo.Id;
        Album album = albumsService.CreateAlbum(albumData);
        return Ok(album);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpDelete("{albumId}")]
    [Authorize]
    public async Task<ActionResult<string>> ArchiveAlbum(int albumId)
    {
      try
      {
        Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
        string message = albumsService.ArchiveAlbum(albumId, userInfo.Id);
        return Ok(message);
      }
    catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{albumId}/pictures")]
    public ActionResult<List<Picture>> GetAlbumPictures(int albumId)
    {
      try
      {
        List<Picture> pictures = picturesService.GetAlbumPictures(albumId);
        return Ok(pictures);
      }
    catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{albumId}/collaborators")]
    public ActionResult<List<CollaboratorAccount>> GetAlbumCollaborators(int albumId)
    {
      try
      {
        List<CollaboratorAccount> collaboratingPeople = collaboratorsService.GetAlbumCollaborators(albumId);
        return Ok(collaboratingPeople);
      }
     catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

}