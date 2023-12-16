using AddressBookConsoleApp.Interfaces;
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AddressBookConsoleApp.Services;
public class ServiceConfigurator
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   // Registering Services and its dependencies
                  
                   services.AddSingleton<IFileService, FileService>();
                   services.AddSingleton<Contact>();
                   services.AddSingleton<IContactService, ContactService>();
                   services.AddSingleton<IMenuService, MenuService>();
                   services.AddSingleton<IConsoleService, ConsoleService>();
               });

}
