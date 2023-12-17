using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace AddressBookWpfApp.ViewModels;

public partial class ContactUpdateViewModel : ObservableObject
{
    [ObservableProperty]
    private Contact _contact = new();

    [ObservableProperty]
    private Contact _selectedContact;

    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    public ContactUpdateViewModel(IServiceProvider serviceProvider, IContactService contactService, Contact selectedContact)
    {
        _selectedContact = selectedContact;
        _serviceProvider = serviceProvider;
        _contactService = contactService;

    }


    [RelayCommand]
    private async Task UpdateContact()
    {
        if (SelectedContact != null)
        {
            var result = await _contactService.UpdateContactAsync(SelectedContact);
            if (result)
            {
                NavigateToContactList();
            }
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
        SelectedContact = selectedContact;
        OnPropertyChanged(nameof(SelectedContact));
    }

}