using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelToolsFramework.Core.Extensions;

namespace PSExcel.Cmdlets;

[Cmdlet(VerbsCommon.Get, "TabName")]
public class GetTabName:PSCmdlet
{
  [Parameter(
    Mandatory = true,
    Position = 0,
    ValueFromPipeline = true,
    ValueFromPipelineByPropertyName = true
  )]
  public XLWorkbook Workbook { get; set; }


  protected override void ProcessRecord()
  {
    var names = Workbook.WorksheetNames();
    WriteObject(names);
  }
}