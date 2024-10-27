using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using CollectionTools.DataTypes;
using CollectionTools.Extensions;

namespace CollectionTools.Comparers;

public class DataTableComparer<TKey>
{
  public KeyedComparisonResult<DataRow, TKey> Compare(DataTable dataTableA, DataTable dataTableB, string keyColumnName)
  {
    KeyedComparer<DataRow, TKey> comparer = new KeyedComparer<DataRow, TKey>(r => (TKey)r[keyColumnName],
      (a, b) => a.ItemArray.SequenceEqual(b.ItemArray));

    return comparer.Compare(from DataRow r in dataTableA.Rows select r, from DataRow r in dataTableB.Rows select r);
  }


  public List<KeyedComparisonResult<DataRow, TKey>> Compare(IEnumerable<Labeled<DataTable, Label>> labeledItems, string keyColumnName)
  {
    var results = new List<KeyedComparisonResult<DataRow, TKey>>();

    var sortedByGroup = new Dictionary<string, List<Labeled<DataTable, Label>>>();
    var sortedByGroupId = new Dictionary<int, List<Labeled<DataTable, Label>>>();

    foreach (var labeledItem in labeledItems)
    {
      if (!string.IsNullOrWhiteSpace(labeledItem.Label.Group))
        sortedByGroup.AddToIndex(labeledItem.Label.Group, labeledItem);

      if (labeledItem.Label.GroupId.HasValue)
        sortedByGroupId.AddToIndex(labeledItem.Label.GroupId.Value, labeledItem);
    }

    foreach (var indexed in sortedByGroup)
    {
      if (indexed.Value.Count == 2)
      {
        DataTable dataTableA = indexed.Value[0].Value;
        DataTable dataTableB = indexed.Value[0].Value;
        KeyedComparisonResult<DataRow, TKey> result = Compare(dataTableA, dataTableB, keyColumnName);
        results.Add(result);
      }

    }

    return results;
  }
}