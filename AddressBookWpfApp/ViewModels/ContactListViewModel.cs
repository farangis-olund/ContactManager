using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace AddressBookWpfApp.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = [];

    public ContactListViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _ = LoadContacts();

    }

    [RelayCommand]
    private void NavigateToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }
       
    
    [RelayCommand]
    public async Task LoadContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        ContactList = new ObservableCollection<Contact>(contacts);
    }

}
