using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelToolsFramework.Core.Extensions;

namespace PSExcel.Cmdlets
{
  [Cmdlet(VerbsCommon.Rename, "Tab")]
  public class RenameTab:PSCmdlet
  {
    [Parameter(
      Mandatory = true,
      Position = 0,
      ValueFromPipeline = true,
      ValueFromPipelineByPropertyName = true
    )]
    public XLWorkbook Workbook { get; set; }

    [Parameter ]
    public string OldName { get; set; }

    [Parameter]
    public string NewName { get; set; }


    protected override void ProcessRecord()
    {
      Workbook.RenameTab(OldName, NewName);
      WriteObject(Workbook);
    }
  }
}
