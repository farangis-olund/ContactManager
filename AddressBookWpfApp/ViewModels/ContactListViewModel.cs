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
    private Contact _selectedContact;
   
    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = [];

    public ContactListViewModel(IServiceProvider serviceProvider, IContactService contactService, Contact selectedContact)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _ = LoadContacts();
        _selectedContact = selectedContact;

    }

    [RelayCommand]
    private void NavigateToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }


    [RelayCommand]
    private void NavigateToUpdateContact()
    {
        if (SelectedContact != null)
        {
            var contactUpdateViewModel = _serviceProvider.GetRequiredService<ContactUpdateViewModel>();
            contactUpdateViewModel.UpdateSelectedContact(SelectedContact);
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactUpdateViewModel>(); 
        
        }


    }

    [RelayCommand]
    public async Task LoadContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        ContactList = new ObservableCollection<Contact>(contacts);
    }

    [RelayCommand]
    private async Task DeleteContact()
    {
        if (SelectedContact != null)
        {
            var result = await _contactService.DeleteContactByEmailAsync(SelectedContact.Email);
            if (result)
            {
                _ = LoadContacts();
            }
        }
    }


}
