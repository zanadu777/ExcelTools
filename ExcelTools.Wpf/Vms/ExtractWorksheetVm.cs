using System.ComponentModel;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Core.Extensions;
using ExcelTools.Wpf.Helpers;
using ExcelTools.Wpf.Wpf;
using Path = DocumentFormat.OpenXml.Drawing.Path;

namespace ExcelTools.Wpf.Vms;

public class ExtractWorksheetVm : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;
  public string ExcelPath { get; set; }
  public string WorksheetName { get; set; }

  public string ExtractedPath { get; set; }

  public string SourceFolderPath { get; set; }

  public string DestinationFolderPath { get; set; }

  public string FolderWorksheetName { get; set; }

  public ICommand ExtractWorksheetCommand { get; set; }
  public ICommand ExtractWorksheetFolderCommand { get; set; }


  public ExtractWorksheetVm()
  {
    ExtractWorksheetCommand = new DelegateCommand(OnExtractWorksheet);
    ExtractWorksheetFolderCommand = new DelegateCommand(OnExtractWorksheetFolder);
  }

  private void OnExtractWorksheetFolder(object obj)
  {
    IsolatedStorageHelper.SerializeJson(this, "ExtractWorksheetVm");

    var source = new DirectoryInfo(SourceFolderPath);
    var destination = new DirectoryInfo(DestinationFolderPath);
    var excelFiles = source.GetFiles("*.xlsx", SearchOption.TopDirectoryOnly);

    var filteredExcel = FilterExcel(excelFiles);
    foreach (var excel in filteredExcel)
    {
      var workbook = new FileInfo(excel.FullName).ToXLWorkbook();
      var extracted =  workbook.ExtractWorksheet(FolderWorksheetName.Trim());
      var extractedFileName = ExtractedFileName(excel);
      var extractedPath = System.IO.Path.Combine(destination.FullName, extractedFileName);
      extracted.SaveAs(extractedPath);
    }

  }

  private List<FileInfo> FilterExcel(FileInfo[] excelFiles)
  {
    return excelFiles.ToList();
  }

  private string ExtractedFileName(FileInfo file)
  {
    return file.Name;
  }

  private void OnExtractWorksheet(object obj)
  {
    var workbook = new FileInfo(ExcelPath).ToXLWorkbook();
    var extracted = workbook.ExtractWorksheet(WorksheetName.Trim());
    extracted.SaveAs(ExtractedPath);
    IsolatedStorageHelper.SerializeJson(this, "ExtractWorksheetVm");
  }


  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    field = value;
    OnPropertyChanged(propertyName);
    return true;
  }
}