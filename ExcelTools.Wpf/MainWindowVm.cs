using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddIn.Core.Helpers;
using ExcelTools.Core.Extensions;
using ExcelTools.Wpf.Wpf;
using Newtonsoft.Json;

namespace ExcelTools.Wpf
{
    public class MainWindowVm: INotifyPropertyChanged
    {
      public event PropertyChangedEventHandler? PropertyChanged;

      [JsonIgnore]
      public ICommand CombineCommand { get; set; }

      public string Excel1Path { get; set; }
      public string Excel2Path { get; set; }

      public string ExcelCombinedPath { get; set; }




    public MainWindowVm()
      {
        CombineCommand = new DelegateCommand(OnCombine);
      }

      private void OnCombine(object obj)
      {
        var workbook1 = new FileInfo(Excel1Path).ToXLWorkbook();
        var workbook2 = new FileInfo(Excel2Path).ToXLWorkbook();
        workbook1.Append(workbook2);
        workbook1.SaveAs(ExcelCombinedPath);
      IsolatedStorageHelper.SerializeJson(this, "MainWindowVm");
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
}
