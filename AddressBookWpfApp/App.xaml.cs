using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookLibrary.Services;
using AddressBookWpfApp.ViewModels;
using AddressBookWpfApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AddressBookWpfApp;

public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        builder = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<Contact>();
                services.AddSingleton<ContactListViewModel>();
                services.AddSingleton<ContactListView>();
                services.AddSingleton<ContactAddViewModel>();
                services.AddSingleton<ContactDetailsViewModel>();
                services.AddSingleton<ContactDetailsView>();
                services.AddSingleton<ContactAddView>();
                services.AddSingleton<IContactService, ContactService>();
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<Contact>();


            }).Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        builder!.Start();
        var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

    }
}
