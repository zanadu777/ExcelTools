using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionTools.DataTypes;

namespace CollectionTools.Extensions;

public static class DataTableExtensions
{
  public static List<object> UniqueValues(this DataTable dt, DataColumn column)
  {
    if (dt == null)
      throw new ArgumentNullException(nameof(dt));

    if (column == null)
      throw new ArgumentNullException(nameof(column));

    if (!dt.Columns.Contains(column.ColumnName))
      throw new ArgumentException($"Column '{column.ColumnName}' does not exist in the DataTable.");

    return dt.AsEnumerable()
      .Select(row => row[column])
      .Distinct()
      .ToList();
  }

  public static ColumnStatistics ColumnStatistics(this DataTable dt, DataColumn column)
  {
    if (dt == null)
      throw new ArgumentNullException(nameof(dt));

    if (column == null)
      throw new ArgumentNullException(nameof(column));

    if (!dt.Columns.Contains(column.ColumnName))
      throw new ArgumentException($"Column '{column.ColumnName}' does not exist in the DataTable.");

    var uniqueValues = dt.UniqueValues(column);

    return new ColumnStatistics
    {
      Name = column.ColumnName,
      TotalRowCount = dt.Rows.Count,
      UniqueRowCount = uniqueValues.Count
    };
  }


  public static ColumnStatistics ColumnStatistics(this DataTable dt, DataColumn column1, DataColumn column2)
  {
    if (dt == null)
      throw new ArgumentNullException(nameof(dt));

    if (column1 == null)
      throw new ArgumentNullException(nameof(column1));

    if (column2 == null)
      throw new ArgumentNullException(nameof(column2));

    if (!dt.Columns.Contains(column1.ColumnName))
      throw new ArgumentException($"Column '{column1.ColumnName}' does not exist in the DataTable.");

    if (!dt.Columns.Contains(column2.ColumnName))
      throw new ArgumentException($"Column '{column2.ColumnName}' does not exist in the DataTable.");

    var combinedValues = dt.AsEnumerable()
      .Select(row => $"{row[column1]}-{row[column2]}")
      .Distinct()
      .ToList();

    return new ColumnStatistics
    {
      Name = $"{column1.ColumnName} - {column2.ColumnName}",
      TotalRowCount = dt.Rows.Count,
      UniqueRowCount = combinedValues.Count
    };
  }



  public static List<ColumnStatistics> ColumnStatistics(this DataTable dt)
  {
    var statistics = new List<ColumnStatistics>();
    foreach (DataColumn column in dt.Columns)
      statistics.Add(dt.ColumnStatistics(column));

    return statistics;
  }


  public static List<ColumnStatistics> ColumnPairStatistics(this DataTable dt)
  {
    var statistics = new List<ColumnStatistics>();
    for (int i = 0; i < dt.Columns.Count; i++)
     for (int j = 0; j < dt.Columns.Count; j++)
       if (i> j)
         statistics.Add(dt.ColumnStatistics(dt.Columns[j], dt.Columns[i]));

    return statistics;
  }

}