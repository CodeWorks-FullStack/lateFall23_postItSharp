

namespace postItSharp.Services;


public class AlbumsService(AlbumsRepository repo)
{
 private readonly AlbumsRepository repo = repo;

    internal string ArchiveAlbum(int albumId, string userId)
    {
      Album original = GetAlbumById(albumId);
      if(original.CreatorId != userId) throw new Exception("Not your's can't let you do that");

      original.Archived = true;
      repo.Update(original);
      return $"archived {original.Title}";
    }

    internal Album CreateAlbum(Album albumData)
    {
        Album album = repo.Create(albumData);
        return album;
    }

    internal Album GetAlbumById(int albumId)
    {
      Album album = repo.GetById(albumId);
      if(album == null) throw new Exception($"no album at id: {albumId}");
      return album;
    }

    internal List<Album> GetAllAlbums()
    {
        List<Album> albums = repo.GetAll();
        return albums;
    }
}