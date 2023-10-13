using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication3.Model;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaApplication3;

public partial class AddEditClientsWindows : Window
{
    private Client Client { get; set; }
    public AddEditClientsWindows()
    {
        InitializeComponent();
        Client = new Client();
        Client.DateBirth = DateTime.Now;
        DataContext = Client;

        
    }
    public AddEditClientsWindows(Client client)
    {
        InitializeComponent();
        Client = client;
        DataContext = Client;

    }

    
    private void BtnSave_OnClick(object? sender, RoutedEventArgs e)
    {
        if (TBoxFirstName.Text.Length <= 0 || TBoxLastName.Text.Length <= 0 || TBoxBirthDate.SelectedDate == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены", ButtonEnum.Ok).ShowAsync();
            return;
        }
        
        
        if(Client.Id == 0) 
            DataBaseManager.AddClients(Client);
        else
            DataBaseManager.UpdateClient(Client);

        ((TableClientsWindows)Owner).DownloadDataGrid();
        this.Close();

    }

    private void BtnBack_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}