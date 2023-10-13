using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication3.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaApplication3;

public partial class AddEditOrdersWindows : Window
{
    private List<Table> _tables { get; set; } = new List<Table>();
    private List<Dish> _dishes { get; set; } = new List<Dish>();
    
    private Order Order { get; set; }
    
    public AddEditOrdersWindows()
    {
        InitializeComponent();
        UpdateCBox();
        Order = new Order();
        DataContext = Order;
        
    }
    public AddEditOrdersWindows(Order order)
    {
        InitializeComponent();
        UpdateCBox();
        Order = order;
        DataContext = Order;

        CBoxDish.SelectedItem = _dishes.Where(d => d.Id == order.Dishe_id).First();
        CBoxTable.SelectedItem = _tables.Where(t => t.Number == order.Table_id).First();
    }
    
    public void UpdateCBox()
    {
        _tables = DataBaseManager.GetTables();
        _dishes = DataBaseManager.GetDish();
        
        CBoxTable.ItemsSource = _tables;
        CBoxDish.ItemsSource = _dishes;
    }
    private void BtnSave_OnClick(object? sender, RoutedEventArgs e)
    {
        if (CBoxDish.SelectedItem == null || CBoxTable.SelectedItem == null || NUpDownCount.Value <= 0)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены",ButtonEnum.Ok).ShowAsync();;
            return;
        }

        Order.Table_id = ((Table)CBoxTable.SelectedItem).Number;
        Order.Dishe_id = ((Dish)CBoxDish.SelectedItem).Id;
        Order.DateCreate = DateTime.Now;
        
        if(Order.Id == 0) 
            DataBaseManager.AddOrder(Order);
        else
            DataBaseManager.UpdateOrder(Order);
        
        ((TableOrdersWindows)Owner).DownloadDataGrid();
        this.Close();
    }

    private void BtnBack_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}