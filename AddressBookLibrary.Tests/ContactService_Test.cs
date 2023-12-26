using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookLibrary.Services;
using Moq;

namespace AddressBookLibrary.Tests;

public class ContactService_Test
{
    private readonly ContactService contactService;
    private readonly Mock<IFileService> fileServiceMock;
        
    private readonly string filePath = @"C:\projects\contacts.json";
    public ContactService_Test()
    {
        fileServiceMock = new Mock<IFileService>();
        contactService = new ContactService(fileServiceMock.Object);
    }

    private readonly Contact testContact = new()
    {
        FirstName = "Elsa",
        LastName = "Olund",
        Email = "elsa@example.com",
        PhoneNumber = "07344344",
        Address = "st.Example, 4555"
    };

    private readonly Contact testContact2 = new()
    {
        FirstName = "Johan",
        LastName = "Olund",
        Email = "johan@example.com",
        PhoneNumber = "12345678",
        Address = "Another St., 123"
    };

    [Fact]
       
    public async Task AddContactAsync_ShouldAddContactToListAndWriteToFile()
    {
        //Arrange
        List<IContact> contacts = null!;

        fileServiceMock.Setup(fs => fs.WriteToJsonFile(It.IsAny<IEnumerable<IContact>>(), It.IsAny<string>()))
                       .Callback<IEnumerable<IContact>, string>((c, filePath) => contacts = c.ToList());

        var contactService = new ContactService(fileServiceMock.Object);

        // Act
        var result = await contactService.AddContactAsync(testContact);

        // Assert
        Assert.True(result); 
        Assert.NotNull(contacts);
        Assert.Single(contacts); 

        Assert.Equal("Elsa", contacts[0].FirstName);
        Assert.Equal("Olund", contacts[0].LastName);
                
        fileServiceMock.Verify(fs => fs.WriteToJsonFile(It.IsAny<IEnumerable<IContact>>(), It.IsAny<string>()), Times.Once);

    }


    [Fact]
    public async Task GetContactByEmailAsync_ShouldReturnContactIfExists()
    {
        // Arrange
        foreach (var contact in new[] { testContact, testContact2 })
        {
            await contactService.AddContactAsync(contact);
        }
       
        // Act
        var result = await contactService.GetContactByEmailAsync(testContact.Email);


        // Assert
        Assert.NotNull(result); 
        Assert.Equal(testContact.Email, result.Email); 
    }


    [Fact]
    public async Task DeleteContactByEmailAsync_ShouldReturnTrue()
    {
        // Arrange
        foreach (var contact in new[] { testContact, testContact2 })
        {
            await contactService.AddContactAsync(contact);
        }

        // Act
        var result = await contactService.DeleteContactByEmailAsync(testContact.Email);

        // Assert
        Assert.True(result);
                                                          
    }

    [Fact]
    public async Task UpdateContactAsync_ShouldUpdateExistingContact_ShouldReturnTrue()
    {
        // Arrange
        foreach (var contact in new[] { testContact, testContact2 })
        {
            await contactService.AddContactAsync(contact);
        }

        var updatedContact = new Contact
        {
            Id = "1",
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Email = "updated@example.com",
            PhoneNumber = "98765432",
            Address = "Updated Address"
        };

        // Act
        var result = await contactService.UpdateContactAsync(updatedContact);

        // Assert
        Assert.True(result); 
    }

    [Fact]
    public async Task GetAllContactsFromFileToList_ShouldReturnListOfContacts()
    {
        // Arrange
        
        fileServiceMock.Setup(fs => fs.ReadFromJsonFile(filePath))
            .Returns(new List<Contact> { testContact, testContact2 });

        var contactService = new ContactService(fileServiceMock.Object); 

        // Act
        var result = await contactService.GetAllContactsFromFileToList();

        // Assert
        Assert.NotNull(result);
        _ = Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
        Assert.Equal(2, result.ToList().Count); 

       fileServiceMock.Verify(fs => fs.ReadFromJsonFile(filePath), Times.Once);
    }

}
