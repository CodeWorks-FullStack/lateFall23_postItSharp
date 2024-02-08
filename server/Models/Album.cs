namespace postItSharp.Models;

public class Album : RepoItem<int>{

  public string Title { get; set; } 
  public string CoverImg { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public bool Archived { get; set; }

  public Account Creator { get; set; }

}

// NOTE this is the VIEW MODEL, how we want to VIEW the many to many
public class CollaboratorAlbum : Album{
  public int CollaboratorId { get; set; }
}