using System.Management.Automation;
using ExcelToolsFramework.Core;

namespace PSExcel.Cmdlets;

[Cmdlet(VerbsCommon.New, "Workbook")]
public class NewWorkbook : PSCmdlet
{

  [Parameter(
    Mandatory = false,
    Position = 0,
    ValueFromPipeline = true,
    ValueFromPipelineByPropertyName = true
  )]
  public string[] Tabs { get; set; }
 

  protected override void ProcessRecord()
  {

    var builder = new WorkbookBuilder();
    builder.WithTabs(Tabs);

    WriteObject(builder.Build());
 
    WriteVerbose("Finished creating the new workbook.");
  }
}