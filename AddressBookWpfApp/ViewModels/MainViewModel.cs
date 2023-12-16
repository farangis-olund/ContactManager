
using AddressBookLibrary.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;


namespace AddressBookWpfApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;
    
    [ObservableProperty]
    private ObservableObject? _currentViewModel;

      
    public MainViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _contactService = contactService;
      
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
              
    }

   
}
