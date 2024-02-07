namespace postItSharp.Models;

public class Album{

  public int Id { get; set; }
  public string Title { get; set; } 
  public string CoverImg { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public bool Archived { get; set; }

  public Account Creator { get; set; }

}