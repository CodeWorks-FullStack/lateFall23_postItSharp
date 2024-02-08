namespace postItSharp.Models;



public class Picture : RepoItem<int>{
  public int AlbumId { get; set; }
  public string CreatorId { get; set; }
  public string ImgUrl { get; set; }
  public Account Creator { get; set; }
  // public Album Album { get; set; } Never used in this app
}