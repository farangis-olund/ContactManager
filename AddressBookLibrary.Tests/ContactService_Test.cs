using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookLibrary.Services;
using Moq;

namespace AddressBookLibrary.Tests;

public class ContactService_Test
{
    private ContactService contactService;
    private Mock<IFileService> _fileServiceMock;
   
    private List<Contact> contacts;
    private readonly string filePath = @"C:\projects\contacts.json";
    public ContactService_Test()
    {
        // Initializing mock and repository before test
        contacts = new List<Contact>();
        _fileServiceMock = new Mock<IFileService>();
        contactService = new ContactService(_fileServiceMock.Object);
    }

    [Fact]
       
    public async Task AddContactAsync_ShouldAddContactToListAndWriteToFile()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        List<IContact> contacts = null!; 

        fileServiceMock.Setup(fs => fs.WriteToJsonFile(It.IsAny<IEnumerable<IContact>>(), It.IsAny<string>()))
                       .Callback<IEnumerable<IContact>, string>((c, filePath) => contacts = c.ToList());

        var contactService = new ContactService(fileServiceMock.Object);


        var contact = new Contact
        {
            FirstName = "Elsa",
            LastName = "Olund",
            Email = "example@example.com",
            PhoneNumber = "07344344",
            Address = "st.Example, 4555"
        };

        // Act
        var result = await contactService.AddContactAsync(contact);

        // Assert
        Assert.True(result); 

        // Check if the contact was added to the list
        Assert.NotNull(contacts);
        Assert.Single(contacts); 

        // Check if the added contact details match the expected values
        Assert.Equal("Elsa", contacts[0].FirstName);
        Assert.Equal("Olund", contacts[0].LastName);
                
        fileServiceMock.Verify(fs => fs.WriteToJsonFile(It.IsAny<IEnumerable<IContact>>(), It.IsAny<string>()), Times.Once);

    }


    [Fact]
    public async Task GetContactByEmailAsync_ShouldReturnContactIfExists()
    {
        // Arrange
        var contacts = new List<Contact>
            {
                new() {
                    FirstName = "Elsa",
                    LastName = "Olund",
                    Email = "elsa@example.com",
                    PhoneNumber = "07344344",
                    Address = "st.Example, 4555"
                },
                new() {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    PhoneNumber = "12345678",
                    Address = "Another St., 123"
                }
                
            };

        foreach (var item in contacts)
        {
            await contactService.AddContactAsync(item);
        }
      
        string emailToFind = "elsa@example.com";

        // Act
        var result = await contactService.GetContactByEmailAsync(emailToFind);

        // Assert
        Assert.NotNull(result); 
        Assert.Equal(emailToFind, result.Email); 
    }


    [Fact]
    public async Task DeleteContactByEmailAsync_ShouldReturnTrue()
    {
        // Arrange
        Contact contact = new ()
        {
            FirstName = "Elsa",
            LastName = "Olund",
            Email = "example@example.com",
            PhoneNumber = "07344344",
            Address = "st.Example, 4555"
        };
        contacts.Add(contact);

        // Act
        var result = await contactService.DeleteContactByEmailAsync(contact.Email);

        // Assert
        Assert.True(result);
                                                          
    }

    [Fact]
    public async Task UpdateContactAsync_ShouldUpdateExistingContact_ShouldReturnTrue()
    {
        // Arrange
        var contacts = new List<Contact>
            {
                new() {
                    Id="1",
                    FirstName = "Elsa",
                    LastName = "Olund",
                    Email = "elsa@example.com",
                    PhoneNumber = "07344344",
                    Address = "st.Example, 4555"
                },
                new() {
                    Id="2",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    PhoneNumber = "12345678",
                    Address = "Another St., 123"
                }
                
            };

        foreach (var item in contacts)
        {
            await contactService.AddContactAsync(item);
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
        
        _fileServiceMock.Setup(fs => fs.ReadFromJsonFile(filePath))
            .Returns(new List<Contact>
            {
               new() { Id="1", FirstName = "Elsa", LastName = "Olund", Email = "elsa@example.com", PhoneNumber = "07344344", Address = "st.Example, 4555" },
               new() { Id="2", FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "54654656", Address = "st.Example, 4555" }
            });

        var contactService = new ContactService(_fileServiceMock.Object); 

        // Act
        var result = await contactService.GetAllContactsFromFileToList();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Contact>>(result);
        Assert.Equal(2, result.Count); 

       _fileServiceMock.Verify(fs => fs.ReadFromJsonFile(filePath), Times.Once);
    }

}
