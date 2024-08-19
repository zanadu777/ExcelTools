using System;
using System.IO;
using System.Management.Automation;
using ClosedXML.Excel;

namespace PSExcel.Cmdlets
{
  [Cmdlet(VerbsCommon.Open, "Workbook")]
  public class OpenWorkbook : PSCmdlet
  {
    private const string FilePathParameterSet = "FilePath";
    private const string DirectoryFileNameParameterSet = "DirectoryFileName";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = FilePathParameterSet
    )]
    public string FilePath { get; set; }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = DirectoryFileNameParameterSet
    )]
    public string Directory { get; set; }

    [Parameter(
        Mandatory = true,
        Position = 1,
        ParameterSetName = DirectoryFileNameParameterSet
    )]
    public string FileName { get; set; }

    protected override void ProcessRecord()
    {
      string filePath = string.Empty;
      try
      {
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

        if (!File.Exists(filePath))
        {
          throw new FileNotFoundException($"The file '{filePath}' does not exist.");
        }

        var workbook = new XLWorkbook(filePath);
        WriteObject(workbook);
        WriteVerbose($"Workbook opened from {filePath}");
      }
      catch (Exception ex)
      {
        WriteError(new ErrorRecord(ex, "OpenWorkbookFailed", ErrorCategory.OpenError, filePath));
      }
    }
  }
}