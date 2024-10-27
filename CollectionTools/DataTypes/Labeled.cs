namespace CollectionTools.DataTypes;

public class Labeled<T, L> where L : ILabel
{
  private readonly T value;
  public Labeled(T value)
  {
    this.value = value;
  }

  public Labeled(T value, L label)
  {
    this.value = value;
    this.Label = label;
  }

  public L Label { get; set; }


  public T Value => value;

}