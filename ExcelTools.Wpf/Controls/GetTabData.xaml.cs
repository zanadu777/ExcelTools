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
    /// Interaction logic for GetTabData.xaml
    /// </summary>
    public partial class GetTabData : UserControl
    {
        public GetTabData()
        {
            InitializeComponent();
            var vm = IsolatedStorageHelper.DeserializeJson<GetTabDataVm>("GetTabDataVm");
            this.DataContext = vm ?? new GetTabDataVm();
    }
    }
}
