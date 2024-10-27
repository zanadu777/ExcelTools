using System;
using System.IO;
using System.Management.Automation;
using ClosedXML.Excel;

namespace PSExcel.Cmdlets;

[Cmdlet(VerbsData.Export, "Workbook")]
public class ExportWorkbook : PSCmdlet
{
  private const string FilePathParameterSet = "FilePath";
  private const string DirectoryFileNameParameterSet = "DirectoryFileName";

  [Parameter(
    Mandatory = true,
    Position = 0,
    ValueFromPipeline = true,
    ValueFromPipelineByPropertyName = true,
    ParameterSetName = FilePathParameterSet
  )]
  [Parameter(
    Mandatory = true,
    Position = 0,
    ValueFromPipeline = true,
    ValueFromPipelineByPropertyName = true,
    ParameterSetName = DirectoryFileNameParameterSet
  )]
  public XLWorkbook Workbook { get; set; }

  [Parameter(
    Mandatory = true,
    Position = 1,
    ParameterSetName = FilePathParameterSet
  )]
  public string FilePath { get; set; }

  [Parameter(
    Mandatory = true,
    Position = 1,
    ParameterSetName = DirectoryFileNameParameterSet
  )]
  public string Directory { get; set; }

  [Parameter(
    Mandatory = true,
    Position = 2,
    ParameterSetName = DirectoryFileNameParameterSet
  )]
  public string FileName { get; set; }

  protected override void ProcessRecord()
  {
    try
    {
      string filePath;

      if (this.ParameterSetName == FilePathParameterSet)
      {
        filePath = FilePath;
      }
      else if (this.ParameterSetName == DirectoryFileNameParameterSet)
      {
        filePath = Path.Combine(Directory, FileName);
      }
      else
      {
        throw new InvalidOperationException("Invalid parameter set.");
      }

      Workbook.SaveAs(filePath);
      WriteVerbose($"Workbook saved to {filePath}");
    }
    catch (Exception ex)
    {
      WriteError(new ErrorRecord(ex, "SaveWorkbookFailed", ErrorCategory.WriteError, Workbook));
    }
  }
}