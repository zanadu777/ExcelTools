using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ExcelToolsFramework.Core
{
    public class WorkbookBuilder
    {

      public XLWorkbook Build()
      {var workbook = new XLWorkbook();

        foreach (var name in tabNames)
          workbook.Worksheets.Add(name);

        return workbook;
      }

      private List<string>  tabNames = new List<string>();
      public WorkbookBuilder WithTabs(IEnumerable< string> tabNames)
      {
        this.tabNames.AddRange(tabNames);
        return this;
      }
    }
}
