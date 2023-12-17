using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace AddressBookWpfApp.ViewModels;

public partial class ContactUpdateViewModel : ObservableObject
{
    [ObservableProperty]
    private Contact _selectedContact;

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList;

    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    public ContactUpdateViewModel(IServiceProvider serviceProvider, IContactService contactService, Contact selectedContact, ObservableCollection<Contact> contactList)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _contactList = contactList;
        _selectedContact = selectedContact;

    }

    [RelayCommand]
    private async Task UpdateContact()
    {
        try
        {
            if (SelectedContact != null)
            {
                var result = await _contactService.UpdateContactAsync(SelectedContact);
                if (result)
                {
                    var contacts = await _contactService.GetAllContactsAsync();
                    ContactList = new ObservableCollection<Contact>(contacts);
                    NavigateToContactList();
                }
            }
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message); 
        }
        
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