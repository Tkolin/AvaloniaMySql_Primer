using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication3.Model;

namespace AvaloniaApplication3;

public partial class TableOrdersWindows : Window
{
    private List<Order> ViewOrders { get; set; }
    private List<Order> DataOrders { get; set; }
    private List<Dish> Dishes { get; set; }
    private List<Table> Tables { get; set; }
    public TableOrdersWindows()
    {
        InitializeComponent();
        DownloadDataGrid();
        UpdateComboBox();
    }
    public void DownloadDataGrid()
    {
        DataOrders = DataBaseManager.GetOrder();
        UpdateDataGrid();
    }
    private void UpdateDataGrid()
    {
        ViewOrders = DataOrders;
        
        if (DishCBox.SelectedItem != null)
            ViewOrders = ViewOrders.Where(o => o.Dishe_id.Equals(((Dish)DishCBox.SelectedItem).Id)).ToList();
        if (TableCBox.SelectedItem != null) 
            ViewOrders = ViewOrders.Where(o => o.Table_id.Equals(((Table)TableCBox.SelectedItem).Number)).ToList();
        
        OrdersDG.ItemsSource = ViewOrders;
        
    }

    private void UpdateComboBox()
    {
        Dishes = DataBaseManager.GetDish();
        Tables = DataBaseManager.GetTables();

        
        
        DishCBox.ItemsSource = Dishes;
        TableCBox.ItemsSource = Tables;
    }
    
    private void TableCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateDataGrid();
    }

    private void DishCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateDataGrid();
    }
    private void ResetBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        DishCBox.SelectedItem = null;
        TableCBox.SelectedItem = null;
        UpdateDataGrid();
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddEditOrdersWindows win = new AddEditOrdersWindows();
        win.ShowDialog(this);
    }

    private void DataGridBtnEdit_OnClick(object? sender, RoutedEventArgs e)
    {
        if(OrdersDG.SelectedItem == null)
            return;
        
        AddEditOrdersWindows win = new AddEditOrdersWindows(OrdersDG.SelectedItem as Order);
        win.ShowDialog(this);
    }

    private void DataGridBtnRemove_OnClick(object? sender, RoutedEventArgs e)
    {
        if(OrdersDG.SelectedItem == null)
            return;
        
        DataBaseManager.RemoveOrder(OrdersDG.SelectedItem as Order);
        DownloadDataGrid();
    }


}