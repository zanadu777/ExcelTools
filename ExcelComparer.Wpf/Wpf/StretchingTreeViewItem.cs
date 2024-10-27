﻿using System.Windows;
using System.Windows.Controls;

namespace ExcelComparer.Wpf.Wpf;

public class StretchingTreeViewItem : TreeViewItem
{
    public StretchingTreeViewItem()
    {
        Loaded += new RoutedEventHandler(StretchingTreeViewItem_Loaded);
    }

    private void StretchingTreeViewItem_Loaded(object sender, RoutedEventArgs e)
    {
        // The purpose of this code is to stretch the Header Content all the way across the TreeView. 
        if (VisualChildrenCount > 0)
        {
            Grid grid = GetVisualChild(0) as Grid;
            if (grid != null && grid.ColumnDefinitions.Count == 3)
            {
                // Remove the middle column which is set to Auto and let it get replaced with the 
                // last column that is set to Star.
                grid.ColumnDefinitions.RemoveAt(1);
            }
        }
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new StretchingTreeViewItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is StretchingTreeViewItem;
    }
}