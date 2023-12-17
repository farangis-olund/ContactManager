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
    private ObservableCollection<Contact> _contactList;

    [ObservableProperty]
    private Contact _selectedContact;
   
    public ContactListViewModel(IServiceProvider serviceProvider, IContactService contactService, Contact selectedContact, ObservableCollection<Contact> contactList)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _ = LoadContacts();
        _selectedContact = selectedContact;
        _contactList = contactList;
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
    private void NavigateToDetailsContact()
    {
        if (SelectedContact != null)
        {
            var contactDetailsViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
            contactDetailsViewModel.UpdateSelectedContact(SelectedContact);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();

        }
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
    public void UpdateContactList(ObservableCollection<Contact> contactList)
    {
        if (contactList == null || (contactList.Count == 1 && string.IsNullOrEmpty(contactList[0]?.FirstName)))
            _ = LoadContacts();
        else
            ContactList = contactList;
        OnPropertyChanged(nameof(ContactList));
    }
    private async Task LoadContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        ContactList = new ObservableCollection<Contact>(contacts);
    }

}
