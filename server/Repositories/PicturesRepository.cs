using Microsoft.AspNetCore.Http.HttpResults;
using postItSharp.Interfaces;

namespace postItSharp.Repositories;

public class PicturesRepository(IDbConnection db) : IRepository<Picture>
{
  private readonly IDbConnection db = db;
    public Picture Create(Picture pictureData)
    {
        string sql = @"
          INSERT INTO pictures
          (imgUrl, creatorId, albumId)
          VALUES
          (@imgUrl, @creatorId, @albumId);

          SELECT
            pictures.*,
            accounts.*
          FROM pictures
          JOIN accounts ON pictures.creatorId = accounts.id
          WHERE pictures.id = LAST_INSERT_ID();
        ";

        Picture picture = db.Query<Picture, Account, Picture>(sql, (picture, account)=>{
          picture.Creator = account;
          return picture;
        }, pictureData).FirstOrDefault();
        return picture;
    }

    public void Delete(int pictureId)
    {
      string sql = @"
        DELETE FROM pictures
        WHERE pictures.id = @pictureId;
      ";

      db.Execute(sql, new{pictureId});
    }

    public List<Picture> GetAll()
    {
        throw new NotImplementedException();
    }

    public Picture GetById(int pictureId)
    {
        string sql = @"
          SELECT 
            pictures.*,
            accounts.*
          FROM pictures
          JOIN accounts ON pictures.creatorId = accounts.id
          WHERE pictures.id = @pictureId;
        ";
        Picture picture = db.Query<Picture, Account, Picture>(sql, (picture, account)=>{
          picture.Creator = account;
          return picture;
        }, new{pictureId}).FirstOrDefault();
        return picture;
    }

    public Picture Update(Picture updateData)
    {
        throw new NotImplementedException();
    }

    internal List<Picture> GetAlbumPictures(int albumId)
    {
        string sql = @"
        SELECT
          pictures.*,
          accounts.*
        FROM pictures
        JOIN accounts ON pictures.creatorId = accounts.id
        WHERE pictures.albumId = @albumId;
        ";
        List<Picture> pictures = db.Query<Picture, Account, Picture>(sql, (picture, account)=>{
          picture.Creator = account;
          return picture;
        }, new{albumId}).ToList();
        return pictures;
    }
}