using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelToolsFramework.Core.Extensions
{
  public static class XLWorkSheetExtensions
  {
    public static DataTable ToDataTable(this IXLWorksheet worksheet)
    {
      var dataTable = new DataTable(worksheet.Name);

      var headerRow = worksheet.FirstRowUsed();
      foreach (var cell in headerRow.CellsUsed())
        dataTable.Columns.Add(cell.GetString());

      foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
      {
        var dataRow = dataTable.NewRow();
        foreach (var cell in row.CellsUsed())
          dataRow[cell.Address.ColumnNumber - 1] = cell.Value;
       
        dataTable.Rows.Add(dataRow);
      }

      return dataTable;
    }
  }
}
