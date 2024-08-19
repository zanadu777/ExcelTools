using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace ExcelToolsFramework.Core.Extensions;

// ReSharper disable once InconsistentNaming
public static  class XLWorkbookExtensions
{

  public static XLWorkbook Append(this XLWorkbook workbook1, XLWorkbook workbook2)
  {
    foreach (var ws2 in workbook2.Worksheets)
    {
      var ws1 = workbook1.Worksheets.FirstOrDefault(ws => ws.Name == ws2.Name);

      if (ws1 != null)
      {
        var lastRowUsed = ws1.LastRowUsed().RowNumber();
        foreach (var row in ws2.RowsUsed())
        {
          foreach (var cell in row.CellsUsed())
          {
            ws1.Cell(lastRowUsed + row.RowNumber(), cell.Address.ColumnNumber).Value = cell.Value;
            ws1.Cell(lastRowUsed + row.RowNumber(), cell.Address.ColumnNumber).Style = cell.Style;
          }
        }
      }
      else
      {
        ws2.CopyTo(workbook1, ws2.Name);
      }
    }

    return workbook1;
  }

  public static List<string> WorksheetNames(this XLWorkbook workbook)
  {
    var worksheetNames = new List<string>();

    foreach (var worksheet in workbook.Worksheets)
    {
      worksheetNames.Add(worksheet.Name);
    }

    return worksheetNames;
  }


  public static Dictionary<string, int> WorksheetRowCounts(this XLWorkbook workbook)
  {
    var worksheetRowCounts = new Dictionary<string, int>();

    foreach (var worksheet in workbook.Worksheets)
    {
      int rowCount = worksheet.RowsUsed().Count();
      worksheetRowCounts.Add(worksheet.Name, rowCount);
    }

    return worksheetRowCounts;
  }


  public static XLWorkbook ExtractWorksheet(this XLWorkbook sourceWorkbook, string worksheetName)
  {
    var newWorkbook = new XLWorkbook();

    var worksheet = sourceWorkbook.Worksheets.FirstOrDefault(ws => ws.Name == worksheetName);

    if (worksheet != null)
      worksheet.CopyTo(newWorkbook, worksheetName);
    else
    {
      throw new Exception($"Worksheet '{worksheetName}' not found.");
    }

    return newWorkbook;
  }

  public static XLWorkbook RenameTab(this XLWorkbook workbook, string originalTabName, string newTabName)
  {
    var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == originalTabName);

    if (worksheet != null)
      worksheet.Name = newTabName;
    
    return workbook;
  }

}