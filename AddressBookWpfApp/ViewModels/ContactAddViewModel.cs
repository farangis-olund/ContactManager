using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace AddressBookWpfApp.ViewModels;

public partial class ContactAddViewModel(IContactService contactService, 
                                         ContactListViewModel contactListViewModel, 
                                         MainViewModel mainViewModel) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly ContactListViewModel _contactListViewModel = contactListViewModel;
    private readonly MainViewModel _mainViewModel = mainViewModel;
   
    [ObservableProperty]
    private Contact _contact = new();
    
    [RelayCommand]
    public async Task AddContactToList()
    {
        if (!string.IsNullOrWhiteSpace(Contact.FirstName) && 
            !string.IsNullOrWhiteSpace(Contact.LastName) && 
            !string.IsNullOrWhiteSpace(Contact.Email))
        {
            var result = await _contactService.AddContactAsync(Contact);
            if (result)
            {
                await _contactListViewModel.LoadContacts();
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
        _mainViewModel.CurrentViewModel = _contactListViewModel;
    }
   
}
