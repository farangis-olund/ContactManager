using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace AddressBookWpfApp.ViewModels;

public partial class ContactAddViewModel(IServiceProvider serviceProvider, IContactService contactService, ObservableCollection<Contact> contactList) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = contactList;
    
    [ObservableProperty]
    private Contact _contact = new();
    
    [RelayCommand]
    public async Task AddContactToList()
    {
        if (!string.IsNullOrWhiteSpace(Contact.FirstName) && !string.IsNullOrWhiteSpace(Contact.LastName) && !string.IsNullOrWhiteSpace(Contact.Email))
        {
            var result = await _contactService.AddContactAsync(Contact);
            if (result)
            {
                var contacts = await _contactService.GetAllContactsAsync();
                ContactList = new ObservableCollection<Contact>(contacts);
                Contact = new();
            }
        } else 
        {
            MessageBox.Show("Please enter all required fields and then Add Contact!");
        }
    }
    
    [RelayCommand]
    private void NavigateToContactList()
    {
        var contactListViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
        contactListViewModel.UpdateContactList(ContactList);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }


}
