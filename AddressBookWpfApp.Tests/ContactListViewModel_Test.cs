
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookWpfApp.ViewModels;
using Moq;
using System.Collections.ObjectModel;

namespace AddressBookWpfApp.Tests;

public class ContactListViewModel_Test
{

    [Fact]
    public async Task DeleteContact_WhenSelectedContactNotNull_DeletesContactAndReloadsContacts()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var contactServiceMock = new Mock<IContactService>();

        var selectedContact = new Contact { Email = "elsa@example.com" };
        var contactList = new ObservableCollection<Contact>
            {
                new() { Email = "elsa@example.com" },
                new() { Email = "another@example.com" }
            };

        var viewModel = new ContactListViewModel(serviceProviderMock.Object, contactServiceMock.Object, selectedContact, contactList);
        
        contactServiceMock.Setup(x => x.DeleteContactByEmailAsync(selectedContact.Email)).ReturnsAsync(true);

        // Act
        await viewModel.DeleteContact();

        // Assert
        contactServiceMock.Verify(x => x.DeleteContactByEmailAsync(selectedContact.Email), Times.Once);
        Assert.Empty(viewModel.ContactList); 
    }

    [Fact]
    public async Task LoadContacts_ShouldRetrieveContactsAndUpdateContactList()
    {
        // Arrange
        var mockContactService = new Mock<IContactService>();
        var mockServiceProvider = new Mock<IServiceProvider>();

        var contacts = new List<Contact>
        {
           new() { Id="1", FirstName = "Elsa", LastName = "Olund", Email = "elsa@example.com", PhoneNumber = "07344344", Address = "st.Example, 4555" },
           new() { Id="2", FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "54654656", Address = "st.Example, 4555" }
        };

        mockContactService.Setup(cs => cs.GetAllContactsAsync())
            .ReturnsAsync(contacts);

        var contactListViewModel = new ContactListViewModel(mockServiceProvider.Object, mockContactService.Object, null!, null!);

        // Act
        await contactListViewModel.LoadContacts();

        // Assert
        Assert.NotNull(contactListViewModel.ContactList);
        Assert.Equal(contacts.Count, contactListViewModel.ContactList.Count);
       
    }


}
