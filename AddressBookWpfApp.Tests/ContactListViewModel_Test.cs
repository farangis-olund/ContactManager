
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookWpfApp.ViewModels;
using Moq;
using System.Collections.ObjectModel;

namespace AddressBookWpfApp.Tests;

public class ContactListViewModel_Test
{
    [Fact]
    public async Task AddContactToList_WhenValidContactInfo_AddsContactToList()
    {
        // Arrange
        var contactServiceMock = new Mock<IContactService>();
        var serviceProviderMock = new Mock<IServiceProvider>();

        var contactList = new ObservableCollection<Contact>();

        var viewModel = new ContactAddViewModel(serviceProviderMock.Object, contactServiceMock.Object, contactList);

        viewModel.Contact.FirstName = "Elsa";
        viewModel.Contact.LastName = "Olund";
        viewModel.Contact.Email = "elsa@example.com";

        // Set up mock behavior for AddContactAsync
        contactServiceMock.Setup(x => x.AddContactAsync(It.IsAny<Contact>()))
                          .ReturnsAsync(true)
                          .Verifiable();

        // Set up mock behavior for GetAllContactsAsync
        contactServiceMock.Setup(x => x.GetAllContactsAsync())
                          .ReturnsAsync(new List<Contact>()) 
                          .Verifiable();

        // Act
        await viewModel.AddContactToList();

        // Assert
        contactServiceMock.Verify(x => x.AddContactAsync(It.IsAny<Contact>()), Times.Once);
        contactServiceMock.Verify(x => x.GetAllContactsAsync(), Times.Once);

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
