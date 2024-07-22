﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ExcelTools.Core.Extensions;

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

}