using System.Windows;
using System.Windows.Controls;

namespace ExcelComparer.Wpf.Wpf;

public class StretchingTreeView : TreeView
{
  public StretchingTreeView()
  {
    this.SelectedItemChanged += StretchingTreeView_SelectedItemChanged;

  }

  private void StretchingTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
  {
    var changedTo = e.NewValue;
    BindableSelectedItem = (e.NewValue);
  }

  public static readonly DependencyProperty BindableSelectedItemProperty =
    DependencyProperty.Register(
      nameof(BindableSelectedItem),
      typeof(object),
      typeof(StretchingTreeView),
      new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemChanged));

  public object BindableSelectedItem
  {
    get => GetValue(BindableSelectedItemProperty);
    set => SetValue(BindableSelectedItemProperty, value);
  }

  private static void OnBindableSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is StretchingTreeView treeView)
      treeView.BindableSelectedItem = treeView.SelectedItem;
    // d.SetValue(BindableSelectedItemProperty, treeView.SelectedItem);

    //try later
    //private void SetSelectedItem(object item)
    //{
    //  var selectedItemProperty = typeof(TreeView).GetProperty("SelectedItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    //  selectedItemProperty?.SetValue(this, item);
    //}
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    var item = new StretchingTreeViewItem();
    //item.IsExpanded = true;
    return item;
  }

  protected override bool IsItemItsOwnContainerOverride(object item)
  {
    return item is StretchingTreeViewItem;
  }
}