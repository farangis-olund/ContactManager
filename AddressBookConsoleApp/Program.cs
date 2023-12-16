using Microsoft.Extensions.DependencyInjection;
using AddressBookConsoleApp.Services;
using AddressBookConsoleApp.Interfaces;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();

using IServiceScope serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;

var menuService = serviceProvider.GetRequiredService<IMenuService>();

menuService.ShowMenu();

host.Run();

IHostBuilder CreateHostBuilder(string[] args)
{
    return ServiceConfigurator.CreateHostBuilder(args);
}
