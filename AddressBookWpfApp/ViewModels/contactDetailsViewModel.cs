
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace AddressBookWpfApp.ViewModels;

public partial class ContactDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
           
    [ObservableProperty]
    private Contact _selectedContact;
    
    public ContactDetailsViewModel(IServiceProvider serviceProvider, Contact selectedContact)
    {
        _serviceProvider = serviceProvider;
        _selectedContact = selectedContact;
    }

    [RelayCommand]
    private void NavigateToContactList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    public void UpdateSelectedContact(Contact selectedContact)
    {
        if (selectedContact != null)
        {
            SelectedContact = selectedContact;
            OnPropertyChanged(nameof(SelectedContact));
        }

    }

}
