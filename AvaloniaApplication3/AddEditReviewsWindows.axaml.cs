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

public partial class AddEditReviewsWindows : Window
{
    private List<Client> _clients { get; set; } = new List<Client>();
    private List<Order> _orders { get; set; } = new List<Order>();
     
    private Review Review { get; set; }
    public AddEditReviewsWindows()
    {
        InitializeComponent();
        Review = new Review();
        DataContext = Review;
        UpdateCBox();
    }
    public AddEditReviewsWindows(Review review)
    {
        InitializeComponent();
        Review = review;
        DataContext = Review;
        UpdateCBox();
        
        CBoxClient.SelectedItem = _clients.Where(c => c.Id == review.Clietn_id).First();
        CBoxOrder.SelectedItem = _orders.Where(o => o.Id == review.Order_id).First();
    }

    public void UpdateCBox()
    {
        _clients = DataBaseManager.GetClients();
        _orders = DataBaseManager.GetOrder();
        
        CBoxClient.ItemsSource = _clients;
        CBoxOrder.ItemsSource = _orders;
    }
    
    private void BtnBack_OnClick(object? sender, RoutedEventArgs e)
    {
        if (CBoxClient.SelectedItem == null || CBoxOrder.SelectedItem == null  || TBoxComents.Text.Length <= 0)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены",ButtonEnum.Ok).ShowAsync();;
            return;
        }
        
        if(Review.Id == 0) 
            DataBaseManager.AddReviews(Review);
        else
            DataBaseManager.UpdateReview(Review);

        ((TableReviewsWindows)Owner).DownloadDataGrid();
        this.Close();
    }

    private void BtnSave_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();

    }
}