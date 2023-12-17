
using AddressBookConsoleApp.Interfaces;
using AddressBookConsoleApp.Services;
using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using Moq;

namespace AddressBookConsoleApp.Tests;

public class MenuService_Test
{
    [Fact]
    public void ShowMenu_ShouldCallDisplayMainMenu()
    {
        // Arrange
        var mockContactService = new Mock<IContactService>();
        var mockContact = new Mock<Contact>();
        var mockConsoleService = new Mock<IConsoleService>();

        var menuService = new MenuService(mockContactService.Object,mockContact.Object, mockConsoleService.Object);

        mockConsoleService.SetupSequence(c => c.ReadLine())
                          .Returns("0"); 

        // Act
        menuService.ShowMenu();

        // Assert
        // Verify that DisplayMainMenu() method was called at least once
        mockConsoleService.Verify(c => c.WriteLine(" Address Book Menu"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.WriteLine("  1.  Add contact"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.WriteLine("  2.  Show contact details"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.WriteLine("  3.  Delete a contact"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.WriteLine("  4.  Show all contacts"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.WriteLine("  0.  Exit"), Times.AtLeastOnce);
        mockConsoleService.Verify(c => c.Write("\nSelect an option "), Times.AtLeastOnce);
    }

    [Fact]
    public void ShowMenu_WhenUserEntersZero_ShouldExitMenu()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactService>();
        var mockContact = new Mock<Contact>();
        var mockConsoleService = new Mock<IConsoleService>();
        var menuService = new MenuService(mockContactRepository.Object, mockContact.Object, mockConsoleService.Object);
        
        mockConsoleService.Setup(c => c.ReadLine()).Returns("0");

        // Act
        menuService.ShowMenu();

        // Assert
        mockConsoleService.Verify(c => c.WriteLine("Exiting..."), Times.Once);
    }

}

