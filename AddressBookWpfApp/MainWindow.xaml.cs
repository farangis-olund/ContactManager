using AddressBookWpfApp.ViewModels;
using System.Windows;


namespace AddressBookWpfApp;


public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}