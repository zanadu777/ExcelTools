using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExcelTools.Wpf;

public  class Tool
{
  public string Name { get; set; }

  public Func<UserControl> ControlGenerator { get; set; }

  private UserControl? control;
  public UserControl Control => control ??= ControlGenerator();
}