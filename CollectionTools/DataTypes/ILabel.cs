namespace CollectionTools.DataTypes;

public interface ILabel
{
  string Name { get; }

  int Id { get; }

  string Group { get; }

  int? GroupId { get; }
}