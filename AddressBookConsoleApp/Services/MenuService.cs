using AddressBookLibrary.Interfaces;
using AddressBookConsoleApp.Interfaces;
using AddressBookLibrary.Models;

namespace AddressBookConsoleApp.Services;

public class MenuService : IMenuService
{
    private readonly Contact _contact;
    private readonly IContactService _contactService;
    private readonly IConsoleService _consoleService;

    public MenuService(IContactService contactService, Contact contact, IConsoleService consoleService)
    {
        _contactService = contactService;
        _contact = contact;
        _consoleService = consoleService;

    }

    public void ShowMenu()
    {
        bool exit = false;

        while (!exit)
        {
            DisplayMainMenu();

            var userInput = _consoleService.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ShowContact();
                    break;
                case "3":
                    DeleteContact();
                    break;
                case "4":
                    ShowAllContacts();
                    break;
                case "0":
                    exit = true;
                    _consoleService.WriteLine("Exiting...");
                    break;
                default:
                    _consoleService.WriteLine("\nInvalid option. Please select a valid option!");
                    _consoleService.ReadKey();
                    break;
            }
        }
    }

    private void DisplayMainMenu()
    {
        DisplayMenuTitle(" Address Book Menu");
        _consoleService.WriteLine("  1.  Add contact");
        _consoleService.WriteLine("  2.  Show contact details");
        _consoleService.WriteLine("  3.  Delete a contact");
        _consoleService.WriteLine("  4.  Show all contacts");
        _consoleService.WriteLine("  0.  Exit");

        _consoleService.Write("\nSelect an option ");
    }

    private void DisplayMenuTitle(string title)
    {
        _consoleService.Clear();
        _consoleService.WriteLine("------------------------------------------------------------------------");
        _consoleService.WriteLine(title);
        _consoleService.WriteLine("------------------------------------------------------------------------");
        _consoleService.WriteLine("");
    }

    private void AddContact()
    {
        DisplayMenuTitle($"Add New Contact");

        _consoleService.Write("Enter First Name: ");
        _contact.FirstName = _consoleService.ReadLine() ?? "";
        _consoleService.WriteLine("");

        _consoleService.Write("Enter Last Name: ");
        _contact.LastName = _consoleService.ReadLine() ?? "";
        _consoleService.WriteLine("");

        _consoleService.Write("Enter Email: ");
        _contact.Email = _consoleService.ReadLine() ?? "";
        _consoleService.WriteLine("");

        _consoleService.Write("Enter Phone Number: ");
        _contact.PhoneNumber = _consoleService.ReadLine() ?? "";
        _consoleService.WriteLine("");

        _consoleService.Write("Enter Address: ");
        _contact.Address = _consoleService.ReadLine() ?? "";
        _consoleService.WriteLine("");

        var result = _contactService.AddContactAsync(_contact).GetAwaiter().GetResult();

        if (result)
        {
            _consoleService.WriteLine("Contact was successfully added!");
        }
        else
        {
            _consoleService.WriteLine("Contact already exists!");
        }

        DisplayPressAnyKey();
    }

    private void ShowContact()
    {
        _consoleService.Write("Enter an Email: ");
        var option = _consoleService.ReadLine() ?? "";
        var result = _contactService.GetContactByEmailAsync(option).GetAwaiter().GetResult();

        if (result is Contact contact)
        {
            _consoleService.Clear();
            DisplayContactDetails(contact);
        }
        else
        {
            _consoleService.WriteLine("No contact found for the provided Email.");
        }

        DisplayPressAnyKey();

    }

    private void DeleteContact()
    {
        _consoleService.Write("Enter an Email: ");
        var option = _consoleService.ReadLine() ?? "";
        var result = _contactService.DeleteContactByEmailAsync(option).GetAwaiter().GetResult();

        if (result)
        {
            _consoleService.WriteLine("A Contact was successfully deleted!");
        }
        else
        {
            _consoleService.WriteLine("No contact found for the provided Email.");
        }
       
        DisplayPressAnyKey();
    }

    private void ShowAllContacts()
    {
        DisplayMenuTitle("Show All Contacts");

        var result = _contactService.GetAllContactsAsync();
       
            if (result.Result is List<Contact> contact)
            {
                if (!contact.Any())
                {
                    _consoleService.WriteLine($"There is no any contact in the list.");
                }
                else
                {
                    foreach (var item in contact)
                    {
                        _consoleService.WriteLine($"FirstName: {item.FirstName} LastName: {item.LastName} Email: {item.Email}");
                        _consoleService.WriteLine("");
                    }
                }
            }
        
        DisplayPressAnyKey();
    }

    private void DisplayContactDetails(IContact contact)
    {
        DisplayMenuTitle("Detail information of the contact");
        _consoleService.WriteLine($" First Name: {contact.FirstName}");
        _consoleService.WriteLine($" Last Name: {contact.LastName}");
        _consoleService.WriteLine($" Email: {contact.Email}");
        _consoleService.WriteLine($" Phone Number: {contact.PhoneNumber}");
        _consoleService.WriteLine($" Address: {contact.Address}");
    }

    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        _consoleService.WriteLine("Press any key to continue");
        _consoleService.ReadKey();
    }

}
