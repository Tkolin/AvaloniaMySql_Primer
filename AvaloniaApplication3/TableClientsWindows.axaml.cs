using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication3.Model;
using Tmds.DBus.Protocol;

namespace AvaloniaApplication3;

public partial class TableClientsWindows : Window
{
    private List<Client> DataClients { get; set; }
    private List<Client> ViewClients { get; set; }
    public TableClientsWindows()
    {
        InitializeComponent();
        DownloadDataGrid();

    }

    public void DownloadDataGrid()
    {
        DataClients = DataBaseManager.GetClients();
        UpdateDataGrid();
    }
    private void UpdateDataGrid()
    {
        ViewClients = DataClients;
        
        if (SearchBox.Text != null && SearchBox.Text.Length > 0)
        {
            ViewClients = ViewClients.Where(c => c.FirstName.ToLower().Contains(SearchBox.Text.ToLower()) ||
                                                 c.LastName.ToLower().Contains(SearchBox.Text.ToLower()) ||
                                                 c.DateBirth.Date.ToString().Contains(SearchBox.Text)).ToList();
        }
        
        ClientDG.ItemsSource = ViewClients;

    }
    private void SearchBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateDataGrid();
    }

    private void ResetBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        SearchBox.Text = "";
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddEditClientsWindows win = new AddEditClientsWindows();
        win.ShowDialog(this);
    }
    private void DataGridBtnEdit_OnClick(object? sender, RoutedEventArgs e)
    {
        if(ClientDG.SelectedItem == null)
            return;
        
        AddEditClientsWindows win = new AddEditClientsWindows(ClientDG.SelectedItem as Client);
        win.ShowDialog(this);
    }

    private void DataGridBtnRemove_OnClick(object? sender, RoutedEventArgs e)
    {
        if(ClientDG.SelectedItem == null)
            return;
        
        DataBaseManager.RemoveClient(ClientDG.SelectedItem as Client);
        
        DownloadDataGrid();
    }
}