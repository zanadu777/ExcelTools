using System.Windows;
using System.Windows.Controls;

namespace ExcelComparer.Wpf.Wpf;

public static class TreeViewItemBehavior
{
  public static readonly DependencyProperty IsExpandedProperty =
    DependencyProperty.RegisterAttached(
      "IsExpanded",
      typeof(bool),
      typeof(TreeViewItemBehavior),
      new UIPropertyMetadata(false, OnIsExpandedChanged));

  public static bool GetIsExpanded(DependencyObject obj)
  {
    return (bool)obj.GetValue(IsExpandedProperty);
  }

  public static void SetIsExpanded(DependencyObject obj, bool value)
  {
    obj.SetValue(IsExpandedProperty, value);
  }

  private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is TreeViewItem item)
    {
      item.IsExpanded = (bool)e.NewValue;
    }
  }
}