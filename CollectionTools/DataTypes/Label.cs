namespace CollectionTools.DataTypes;

public class Label : ILabel
{
  public string Name { get; set; }
  public int Id { get; set; }
  public string Group { get; set; }
  public int? GroupId { get; set; }
}