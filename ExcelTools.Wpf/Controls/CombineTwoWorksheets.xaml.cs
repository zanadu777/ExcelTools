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
using ExcelTools.Wpf.Helpers;
using ExcelTools.Wpf.Vms;

namespace ExcelTools.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for CombineTwoWorksheets.xaml
    /// </summary>
    public partial class CombineTwoWorksheets : UserControl
    {
        public CombineTwoWorksheets()
        {
            InitializeComponent();
            var vm = IsolatedStorageHelper.DeserializeJson<CombineTwoWorksheetsVm>("CombineTwoWorksheetsVm");
            this.DataContext = vm ?? new CombineTwoWorksheetsVm();
    }
    }
}
