namespace postItSharp.Models;


public class Collaborator : RepoItem<int>{
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
  
  // NOTE i am not going to have an account, or album here
}