using postItSharp.Interfaces;

namespace postItSharp.Repositories;

public class AlbumsRepository(IDbConnection db) : IRepository<Album>
{
  private readonly IDbConnection db = db;

// NOTE abstracted populate creator MAP, certainly handy but can make it harder to understand how dapper is working under the hood.
    private Album populateCreator(Album album, Account account){
      album.Creator = account;
      return album;
    }

    public Album GetById(int albumId)
    {
        string sql = @"
        SELECT
          albums.*,
          accounts.*
        FROM albums
        JOIN accounts ON albums.creatorId = accounts.id
        WHERE albums.id = @albumId;
        ";
        // Album album = db.Query<Album, Account, Album>(sql, populateCreator, new{albumId}).FirstOrDefault();
        Album album = db.Query<Album, Account, Album>(sql, (album, account)=>{
          album.Creator = account;
          return album;
        }, new{albumId}).FirstOrDefault();
        return album;
    }

    public List<Album> GetAll()
    {
      string sql = @"
      SELECT
        albums.*,
        accounts.*
      FROM albums
      JOIN accounts ON albums.creatorId = accounts.id;
      ";
      // table data types ---------⬇️------⬇️ Follows the order of our select
      // -------------------------------------------⬇️ returned type
    List<Album> albums = db.Query<Album, Account, Album>(sql, (album, account)=> {
      // REVIEW this is populating our creator for the album
      album.Creator = account;
      return album; // -- returned type, or result of our map
      }).ToList();

    return albums;
    }

    public Album Create(Album createData) // NOTE your sql CANNOT Pull the data from this parameter UNTIL ⬇️
    {
        string sql = @"
        INSERT INTO albums
        (title, coverImg, category, creatorId)
        VALUES
        (@title, @coverImg, @category, @creatorId);

        Select
          albums.*,
          accounts.*
        FROM albums
        JOIN accounts ON albums.creatorId = accounts.id
        WHERE albums.id = LAST_INSERT_ID();
        ";

        Album album = db.Query<Album, Account, Album>(sql, (album, account)=>{
          album.Creator = account;
          return album;
        }, createData).FirstOrDefault();
        //  ⬆️ --- you pass it into your Query
        return album;
    }

    public Album Update(Album updateData)
    {
        string sql = @"
        UPDATE albums SET
        title = @title,
        coverImg = @coverImg,
        archived = @archived,
        category = @category
        WHERE id = @id;

        SELECT
          albums.*,
          accounts.*
        FROM albums
        JOIN accounts ON albums.creatorId = accounts.id
        WHERE albums.id = @id;
        ";
      Album album = db.Query<Album, Account, Album>(sql, (album, account)=>{
        album.Creator = account;
        return album;
      }, updateData).FirstOrDefault();
      return album;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}