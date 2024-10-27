using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExcelComparer.Wpf.Wpf;
using Newtonsoft.Json;

namespace ExcelComparer.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private MainWindowVm Vm;
  public MainWindow()
  {
    InitializeComponent();
    var mainWindowVmJson = IsolatedStorageHelper.ReadTextFromIsolatedStorage("MainWindowVm");
    Vm = mainWindowVmJson == null ? new MainWindowVm() : JsonConvert.DeserializeObject<MainWindowVm>(mainWindowVmJson);

    if (Vm.SheetA == null)
      Vm = new MainWindowVm();

    Vm.Updated += Vm_Updated;
    DataContext = Vm;
  }

  private void Vm_Updated(object sender, EventArgs e)
  {
    var mainWindowVmJson = JsonConvert.SerializeObject(Vm);
    IsolatedStorageHelper.WriteTextToIsolatedStorage("MainWindowVm", mainWindowVmJson);
  }
}