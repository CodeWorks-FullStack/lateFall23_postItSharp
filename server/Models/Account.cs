namespace postItSharp.Models;

public class Account : RepoItem<string>
{
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
}

// NOTE this is the VIEW MODEL, how we want to VIEW the many to many
public class CollaboratorAccount : Account{
  public int CollaboratorId { get; set; }
}
