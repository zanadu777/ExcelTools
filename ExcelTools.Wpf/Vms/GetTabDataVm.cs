using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ExcelTools.Core.Extensions;
using ExcelTools.Wpf.Helpers;
using ExcelTools.Wpf.Wpf;
using Newtonsoft.Json;

namespace ExcelTools.Wpf.Vms;

class GetTabDataVm : INotifyPropertyChanged
{
  private string worksheetRowCountsResult;
  private string getTabNamesResult;
  public event PropertyChangedEventHandler? PropertyChanged;

  [JsonIgnore]
  public ICommand GetTabNamesCommand { get; set; }

  public ICommand WorksheetRowCountsCommand { get; set; }

  public string ExcelPath1 { get; set; }

  [JsonIgnore]
  public string GetTabNamesResult
  {
    get => getTabNamesResult;
    set => SetField(ref getTabNamesResult, value);
  }


  [JsonIgnore]
  public string WorksheetRowCountsResult
  {
    get => worksheetRowCountsResult;
    set => SetField(ref worksheetRowCountsResult, value);
  }


  public GetTabDataVm()
  {
    GetTabNamesCommand = new DelegateCommand(OnGetTabNames);
    WorksheetRowCountsCommand = new DelegateCommand(OnWorksheetRowCounts);
  }

  private void OnWorksheetRowCounts(object obj)
  {
    var workbook = new FileInfo(ExcelPath1).ToXLWorkbook();
    var rowCounts = workbook.WorksheetRowCounts();
    WorksheetRowCountsResult = JsonConvert.SerializeObject(rowCounts);
    IsolatedStorageHelper.SerializeJson(this, "GetTabDataVm");
  }

  private void OnGetTabNames(object obj)
  {
    var workbook = new FileInfo(ExcelPath1).ToXLWorkbook();
    var tabs = workbook.WorksheetNames();
    GetTabNamesResult = JsonConvert.SerializeObject(tabs);
    IsolatedStorageHelper.SerializeJson(this, "GetTabDataVm");
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