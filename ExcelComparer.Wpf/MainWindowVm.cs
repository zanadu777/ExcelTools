using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClosedXML.Excel;
using CollectionTools.DataTypes;
using CollectionTools.Extensions;
using ExcelComparer.Wpf.Wpf;
using ExcelToolsFramework.Core.Extensions;


namespace ExcelComparer.Wpf;

internal class MainWindowVm : INotifyPropertyChanged
{
  private ExcelWorksheet sheetA = new();
  private ExcelWorksheet sheetB = new();
  private XLWorkbook workbookA1;
  private XLWorkbook workbookB1;

  public event EventHandler Updated;
  public event PropertyChangedEventHandler PropertyChanged;

  public ICommand CompareExcelCommand { get; set; }
  public ICommand LoadExcelCommand { get; set; }

  public MainWindowVm()
  {
    CompareExcelCommand = new DelegateCommand(OnCompareExcel);
    LoadExcelCommand = new DelegateCommand(OnLoadExcel);
  }

  private void OnLoadExcel(object obj)
  {

  }

  private void OnCompareExcel(object obj)
  {
    Updated?.Invoke(this, EventArgs.Empty);
    if (string.IsNullOrEmpty(SheetA.Path))
      return;
    if (string.IsNullOrEmpty(SheetB.Path))
      return;

    XLWorkbook workbookA = new XLWorkbook(SheetA.Path);
    XLWorkbook workbookB = new XLWorkbook(SheetB.Path);

    TableA = workbookA.ToDataTable(SheetA.SheetName);
    TableB = workbookB.ToDataTable(SheetA.SheetName);

    ColumnStatisticsA.Clear();
    foreach (var statistics in TableA.ColumnStatistics())
      ColumnStatisticsA.Add(statistics);

    var ColumnAHash = new HashSet<string>(ColumnStatisticsA.Where( x=> x.IsUnique).Select(x => x.Name));
    var ColumnBHash = new HashSet<string>(ColumnStatisticsB.Where(x => x.IsUnique).Select(x => x.Name));

    ColumnPairsStatisticsA.Clear();
    foreach (var statistics in TableA.ColumnPairStatistics(ColumnAHash))
      ColumnPairsStatisticsA.Add(statistics);

    ColumnStatisticsB.Clear();
    foreach (var statistics in TableB.ColumnStatistics())
      ColumnStatisticsB.Add(statistics);

    ColumnPairsStatisticsB.Clear();
    foreach (var statistics in TableB.ColumnPairStatistics(ColumnBHash))
      ColumnPairsStatisticsB.Add(statistics);
  }

  public ObservableCollection<ColumnStatistics> ColumnStatisticsA { get; set; } = new();
  public ObservableCollection<ColumnStatistics> ColumnStatisticsB { get; set; } = new();

  public ObservableCollection<ColumnStatistics> ColumnPairsStatisticsA { get; set; } = new();
  public ObservableCollection<ColumnStatistics> ColumnPairsStatisticsB { get; set; } = new();


  public ExcelWorksheet SheetA
  {
    get => sheetA;
    set => SetField(ref sheetA, value);
  }

  public ExcelWorksheet SheetB
  {
    get => sheetB;
    set => SetField(ref sheetB, value);
  }

  public XLWorkbook WorkbookA
  {
    get => workbookA1;
    set => SetField(ref workbookA1, value);
  }

  public XLWorkbook WorkbookB
  {
    get => workbookB1;
    set => SetField(ref workbookB1, value);
  }


  public DataTable TableA { get; set; }
  public DataTable TableB { get; set; }


  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    field = value;
    OnPropertyChanged(propertyName);
    return true;
  }
}