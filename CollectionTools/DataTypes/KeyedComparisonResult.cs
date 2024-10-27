using System.Collections.Generic;
using System.Linq;

namespace CollectionTools.DataTypes;

public class KeyedComparisonResult<T, TKey>
{
  public KeyedComparisonResult() { }

  public List<T> OnlyA { get; } = new List<T>();
  public List<T> OnlyB { get; } = new List<T>();
  public List<T> Same { get; } = new List<T>();

  public List<TKey> OnlyAKeys { get; } = new List<TKey>();
  public List<TKey> OnlyBKeys { get; } = new List<TKey>();
  public List<TKey> SameKeys { get; } = new List<TKey>();
  public List<TKey> DifferentKeys { get; } = new List<TKey>();
  public List<KeyValuePair<T, T>> Different { get; set; } = new List<KeyValuePair<T, T>>();

  public bool IsASubsetOfB => !OnlyA.Any() && Different.Count == 0;

  public bool IsBSubsetOfA => OnlyB.Count == 0 && Different.Count == 0;

  public bool AreSameExcludingOrder => OnlyA.Count == 0 && Different.Count == 0 && OnlyB.Count == 0;

  public string Summary { get; set; }
}