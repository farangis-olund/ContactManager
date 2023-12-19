using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;


namespace AddressBookWpfApp.ViewModels;

public partial class ContactUpdateViewModel(IServiceProvider serviceProvider, 
                                            IContactService contactService, 
                                            Contact selectedContact, 
                                            ContactListViewModel contactListViewModel) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;
    private readonly ContactListViewModel _contactListViewModel = contactListViewModel;

    [ObservableProperty]
    private Contact _selectedContact = selectedContact;
       
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
                    await _contactListViewModel.LoadContacts();
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

}