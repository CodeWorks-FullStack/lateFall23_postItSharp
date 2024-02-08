


namespace postItSharp.Services;

public class PicturesService(PicturesRepository repo){
  private readonly PicturesRepository repo = repo;

    internal Picture CreatePicture(Picture pictureData)
    {
      Picture picture = repo.Create(pictureData);
      return picture;
    }

    internal string DeletePicture(int pictureId, string userId)
    {
      Picture original = repo.GetById(pictureId);
       if(original.CreatorId != userId) throw new Exception("Not your picture to delete");

      repo.Delete(pictureId);
      return $"picture {original.Id} was removed";


    }

    internal List<Picture> GetAlbumPictures(int albumId)
    {
        List<Picture> pictures = repo.GetAlbumPictures(albumId);
        return pictures;
    }
}