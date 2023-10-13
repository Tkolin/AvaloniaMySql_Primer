using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication3.Model;

namespace AvaloniaApplication3;

public partial class TableReviewsWindows : Window
{
    private List<Review> ViewReviews { get; set; } 
    private List<Review> DataReviews { get; set; } 
    private List<Order> Orders { get; set; }
    private List<Client> Clients { get; set; }
    public TableReviewsWindows()
    {
        InitializeComponent();
        DownloadDataGrid();
        UpdateComboBox();
    }
    public void DownloadDataGrid()
    {
        DataReviews = DataBaseManager.GetReview();
        UpdateDataGrid();
    }
    private void UpdateComboBox()
    {
        Orders = DataBaseManager.GetOrder();
        Clients = DataBaseManager.GetClients();
        
        OrdersCBox.ItemsSource = Orders;
        ClientCBox.ItemsSource = Clients;
    }
    private void UpdateDataGrid()
    {
        ViewReviews = DataReviews; 
        
        if (ClientCBox.SelectedItem != null)
            ViewReviews = ViewReviews.Where(o => o.Clietn_id.Equals(((Client)ClientCBox.SelectedItem).Id)).ToList();
        if (OrdersCBox.SelectedItem != null) 
            ViewReviews = ViewReviews.Where(o => o.Order_id.Equals(((Order)OrdersCBox.SelectedItem).Id)).ToList();
        ReviewDG.ItemsSource = ViewReviews;
    }
    private void ResetBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        OrdersCBox.SelectedItem = null;
        ClientCBox.SelectedItem = null;
        UpdateDataGrid();
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddEditReviewsWindows win = new AddEditReviewsWindows();
        win.ShowDialog(this);
    }



    private void DataGridBtnEdit_OnClick(object? sender, RoutedEventArgs e)
    {
        if(ReviewDG.SelectedItem == null)
            return;
        
        AddEditReviewsWindows win = new AddEditReviewsWindows(ReviewDG.SelectedItem as Review);
        win.ShowDialog(this);
    }
    private void DataGridBtnRemove_OnClick(object? sender, RoutedEventArgs e)
    {
        if(ReviewDG.SelectedItem == null)
            return;
        
        DataBaseManager.RemoveReview(ReviewDG.SelectedItem as Review);
        DownloadDataGrid();
    }

    private void ClientCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateDataGrid();
    }

    private void OrdersCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateDataGrid();
    }
}