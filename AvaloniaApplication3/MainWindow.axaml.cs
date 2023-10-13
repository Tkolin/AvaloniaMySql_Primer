using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaApplication3;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Btn1_OnClick(object? sender, RoutedEventArgs e)
    {
        TableClientsWindows clientsWindows = new TableClientsWindows();
        clientsWindows.ShowDialog(this);
    }
    

    private void Btn2_OnClick(object? sender, RoutedEventArgs e)
    {
        TableOrdersWindows clientsWindows = new TableOrdersWindows();
        clientsWindows.ShowDialog(this);
    }

    private void Btn3_OnClick(object? sender, RoutedEventArgs e)
    {
        TableReviewsWindows tableReviewsWindows = new TableReviewsWindows();
        tableReviewsWindows.ShowDialog(this);
    }
}