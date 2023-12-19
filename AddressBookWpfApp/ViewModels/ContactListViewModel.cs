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
    
    public ContactListViewModel(IServiceProvider serviceProvider, 
                                IContactService contactService, 
                                Contact selectedContact, 
                                ObservableCollection<Contact> contactList)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _ = LoadContacts();
        _selectedContact = selectedContact;
        _contactList = contactList;
        
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList;

    [ObservableProperty]
    private Contact _selectedContact;

    [RelayCommand]
    private void NavigateToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }
       
    [RelayCommand]
    public void NavigateToDetailsContact() => NavigateToViewModel<ContactDetailsViewModel>();

    [RelayCommand]
    public void NavigateToUpdateContact() => NavigateToViewModel<ContactUpdateViewModel>();

    [RelayCommand]
    private async Task DeleteContact()
    {
        if (SelectedContact != null)
        {
            var result = await _contactService.DeleteContactByEmailAsync(SelectedContact.Email);
            if (result)
            {
                await LoadContacts();
            }
        }
    }
    
    private void NavigateToViewModel<T>() where T : class
    {
        if (SelectedContact != null)
        {
            var viewModel = _serviceProvider.GetRequiredService<T>() as ObservableObject;
            viewModel?.GetType().GetProperty("SelectedContact")?.SetValue(viewModel, SelectedContact);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = viewModel;
        }
    }

    public async Task LoadContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        ContactList = new ObservableCollection<Contact>(contacts);
    }

}
