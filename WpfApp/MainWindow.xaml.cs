﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp;
using KooliProjekt.PublicAPI.Api;
using System.Net.Http;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7136/api/") };
        var apiClient = new ApiClient(httpClient);

        var viewModel = new MainWindowViewModel(apiClient);

        viewModel.ConfirmDelete = _ =>
        {
            var result = MessageBox.Show(
                            "Are you sure you want to delete selected item?",
                            "Delete list",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Stop
                            );
            return (result == MessageBoxResult.Yes);
        };

        viewModel.OnError = (error) =>
        {
            MessageBox.Show(
                    error,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        };

        DataContext = viewModel;

        await viewModel.Load();
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}