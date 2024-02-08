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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }


    public Collaborator GetById(int id)
    {
        throw new NotImplementedException();
    }

}
