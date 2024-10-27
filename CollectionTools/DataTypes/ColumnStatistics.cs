using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionTools.DataTypes;

public class ColumnStatistics
{
  public string Name { get; set; }

  public int TotalRowCount { get; set; }

  public int UniqueRowCount { get; set; }

  public bool IsUnique => TotalRowCount == UniqueRowCount;
}