﻿using System.Text;
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

namespace ExcelTools.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    var mainWindowVm = IsolatedStorageHelper.DeserializeJson<MainWindowVm>("MainWindowVm");
    this.DataContext = mainWindowVm ?? new MainWindowVm();
  }
}