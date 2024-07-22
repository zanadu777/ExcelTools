using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ExcelTools.Core.Extensions;
using ExcelTools.Wpf.Helpers;
using ExcelTools.Wpf.Wpf;
using Newtonsoft.Json;

namespace ExcelTools.Wpf.Vms
{
  public class CombineTwoWorksheetsVm
  {
    [JsonIgnore]
    public ICommand CombineCommand { get; set; }

    public string Excel1Path { get; set; }
    public string Excel2Path { get; set; }

    public string ExcelCombinedPath { get; set; }

    public CombineTwoWorksheetsVm()
    {
      CombineCommand = new DelegateCommand(OnCombine);


    }
    private void OnCombine(object obj)
    {
      var workbook1 = new FileInfo(Excel1Path).ToXLWorkbook();
      var workbook2 = new FileInfo(Excel2Path).ToXLWorkbook();
      workbook1.Append(workbook2);
      workbook1.SaveAs(ExcelCombinedPath);
      IsolatedStorageHelper.SerializeJson(this, "CombineTwoWorksheetsVm");
    }
  }
}
