



namespace postItSharp.Services;


public class CollaboratorsService(CollaboratorsRepository repo)
{
  private readonly CollaboratorsRepository repo = repo;

    internal CollaboratorAccount CreateCollaborator(Collaborator collaboratorData)
    {
      CollaboratorAccount collabAccount = repo.CreateCollab(collaboratorData);
      return collabAccount;
    }
      // This takes in two ids and 
    internal string DeleteCollaborator(int collaboratorId, string userId)
    {
      Collaborator original = repo.GetById(collaboratorId);
      if(original == null) throw new Exception($"no collab at id: {collaboratorId}");
      if(original.AccountId != userId) throw new Exception($"You can't do that you don't own it!");

      repo.Delete(collaboratorId);
      return $"Collab {collaboratorId} was deleted";
    }

    internal List<CollaboratorAlbum> GetAccountCollaborators(string userId)
    {
      List<CollaboratorAlbum> collabAlbums = repo.GetAccountCollaborators(userId);
      return collabAlbums;
    }

    internal List<CollaboratorAccount> GetAlbumCollaborators(int albumId)
    {
      List<CollaboratorAccount> collaboratingPeople = repo.GetAlbumCollaborators(albumId);
      return collaboratingPeople;
    }
}