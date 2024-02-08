
namespace postItSharp.Services;


public class CollaboratorsService(CollaboratorsRepository repo)
{
  private readonly CollaboratorsRepository repo = repo;

    internal CollaboratorAccount CreateCollaborator(Collaborator collaboratorData)
    {
      CollaboratorAccount collabAccount = repo.CreateCollab(collaboratorData);
      return collabAccount;
    }
}