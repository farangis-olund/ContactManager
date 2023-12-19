
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace AddressBookWpfApp.ViewModels;

public partial class ContactDetailsViewModel(IServiceProvider serviceProvider, 
                                             Contact selectedContact) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
           
    [ObservableProperty]
    private Contact _selectedContact = selectedContact;

    [RelayCommand]
    private void NavigateToContactList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

}
