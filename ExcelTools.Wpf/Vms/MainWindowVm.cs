using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ExcelTools.Core.Extensions;
using ExcelTools.Wpf.Controls;
using ExcelTools.Wpf.Helpers;
using ExcelTools.Wpf.Wpf;
using Newtonsoft.Json;

namespace ExcelTools.Wpf.Vms
{
    public class MainWindowVm: INotifyPropertyChanged
    {
      public event PropertyChangedEventHandler? PropertyChanged;

      public ObservableCollection<Tool> Tools { get; set; } = new();

    public MainWindowVm()
    {
      Tools.Add(new Tool
      {
        Name = "Combine Two Worksheets",
        ControlGenerator = ()=> new CombineTwoWorksheets()
      });

      Tools.Add(new Tool
      {
        Name = "Get Tab Data",
        ControlGenerator = () => new GetTabData()
      });

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
