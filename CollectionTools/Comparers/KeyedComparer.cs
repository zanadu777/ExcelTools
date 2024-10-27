using System;
using System.Collections.Generic;
using System.Linq;
using CollectionTools.DataTypes;

namespace CollectionTools.Comparers;

public class KeyedComparer<T, TKey>
{
  public KeyedComparer(Func<T, TKey> uniqueIndexer, Func<T, T, bool> equalityTester)
  {
    UniqueIndexer = uniqueIndexer;
    EqualityTester = equalityTester;
  }

  private readonly Func<T, TKey> UniqueIndexer;
  private readonly Func<T, T, bool> EqualityTester;

  public KeyedComparisonResult<T, TKey> Compare(IEnumerable<T> itemsA, IEnumerable<T> itemsB)
  {
    var result = new KeyedComparisonResult<T, TKey>();
    var itemsADict = itemsA.ToDictionary(UniqueIndexer);
    var itemsBDict = itemsB.ToDictionary(UniqueIndexer);

    foreach (var key in itemsADict.Keys)
    {
      var itemA = itemsADict[key];
      if (itemsBDict.ContainsKey(key))
      {
        var itemB = itemsBDict[key];

        if (EqualityTester(itemA, itemB))
        {
          result.Same.Add(itemsADict[key]);
          result.SameKeys.Add(key);
        }
        else
        {
          result.Different.Add(new KeyValuePair<T, T>(itemA, itemB));
          result.DifferentKeys.Add(key);
        }
      }
      else
      {
        result.OnlyA.Add(itemA);
        result.OnlyAKeys.Add(key);
      }

    }

    foreach (var key in itemsBDict.Keys)
    {
      if (itemsADict.ContainsKey(key))
        continue;

      var itemB = itemsBDict[key];
      result.OnlyB.Add(itemB);
      result.OnlyBKeys.Add(key);
    }

    return result;
  }

  //public List<KeyedComparisonResult<T, TKey>> Compare<T, TKey>(IEnumerable<Labeled<T, Label>> labeledItems)
  //{
  //    var results = new List<KeyedComparisonResult<T, TKey>>();

  //    var sortedByGroup  = new Dictionary<string,List< Labeled<T, Label>>>();
  //    var sortedByGroupId = new Dictionary<int, List<Labeled<T, Label>>>();

  //    foreach (var labeledItem in labeledItems)
  //    {
  //        if (!string.IsNullOrWhiteSpace(labeledItem.Label.Group))
  //            sortedByGroup.AddToIndex(labeledItem.Label.Group, labeledItem );

  //        if (labeledItem.Label.GroupId.HasValue) 
  //            sortedByGroupId.AddToIndex(labeledItem.Label.GroupId.Value, labeledItem);
  //    }

  //    foreach (var indexed in sortedByGroup)
  //    {
  //        if (indexed.Value.Count == 2)
  //        {
  //            var result = Compare()
  //        }

  //    }

  //    return results;
  //}
}