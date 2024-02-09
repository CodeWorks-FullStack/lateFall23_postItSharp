using postItSharp.Interfaces;

namespace postItSharp.Repositories;


public class CollaboratorsRepository(IDbConnection db)
{

  private readonly IDbConnection db = db;

// NOTE what this create returns is VERY VERY VERY APP SPECIFIC.
    public CollaboratorAccount CreateCollab(Collaborator collaboratorData)
    {
      string sql = @"
      INSERT INTO collaborators
      (accountId, albumId)
      VALUES
      (@accountId, @albumId);

      SELECT
        accounts.*,
        collaborators.id
      FROM collaborators
      JOIN accounts ON collaborators.accountId = accounts.id
      WHERE collaborators.id = LAST_INSERT_ID();
      ";
      CollaboratorAccount collaboratorAccount = db.Query<CollaboratorAccount, Collaborator, CollaboratorAccount>(sql, (collabAccount, collaborator)=> {
        collabAccount.CollaboratorId = collaborator.Id;
        return collabAccount;
      }, collaboratorData).FirstOrDefault();
      return collaboratorAccount;
    }

    public void Delete(int collaboratorId)
    {
      string sql = @"
      DELETE FROM collaborators WHERE id = @collaboratorId
      ";
      db.Execute(sql, new{collaboratorId});
    }


    public Collaborator GetById(int collaboratorId)
    {
      string sql = @"
      SELECT
        *
      FROM collaborators 
      WHERE id = @collaboratorId;
      ";
      Collaborator collaborator = db.Query<Collaborator>(sql, new{collaboratorId}).FirstOrDefault();
      return collaborator;
    }

    internal List<CollaboratorAlbum> GetAccountCollaborators(string userId)
    {
      string sql = @"
      SELECT
        collaborators.*,
        albums.*
      FROM collaborators
      JOIN albums ON collaborators.albumId = albums.id
      WHERE collaborators.accountId = @userId;
      ";
      List<CollaboratorAlbum> collabAlbums = db.Query<Collaborator, CollaboratorAlbum, CollaboratorAlbum>(sql, (collaborator, collabAlbum) => {
        collabAlbum.CollaboratorId = collaborator.Id;
        return collabAlbum;
      }, new{userId}).ToList();
      return collabAlbums;
    }

    internal List<CollaboratorAccount> GetAlbumCollaborators(int albumId)
    {
      string sql = @"
      SELECT
        collaborators.*,
        accounts.*
      FROM collaborators 
      JOIN accounts ON collaborators.accountId = accounts.id
      WHERE collaborators.albumId = @albumId;
      "; 
      List<CollaboratorAccount> collaboratingPeople = db.Query<Collaborator, CollaboratorAccount, CollaboratorAccount>(sql, (collaborator, collabAccount)=> {
        collabAccount.CollaboratorId = collaborator.Id;
        return collabAccount;
      }, new{albumId}).ToList();
      return collaboratingPeople;
    }
}
