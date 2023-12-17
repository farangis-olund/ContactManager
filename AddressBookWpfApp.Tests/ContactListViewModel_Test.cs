
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookWpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AddressBookWpfApp.Tests;

public class ContactListViewModel_Test
{
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
