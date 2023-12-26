using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using System.Diagnostics;

namespace AddressBookLibrary.Services;

public class ContactService (IFileService fileService) : IContactService
{
    private List<Contact> _contacts = [];
    private readonly IFileService _fileService = fileService;
    private readonly string filePath = @"C:\projects\contacts.json";

    public async Task<bool> AddContactAsync(Contact contact)
    {
        try
        {
            _contacts.Add(contact);
            _fileService.WriteToJsonFile(_contacts, filePath);
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);    
            return await Task.FromResult(false);
        }
        
    }


    public async Task<Contact> GetContactByEmailAsync(string email)
    {
        try
        {
            var contact = _contacts.FirstOrDefault(x => x.Email == email);
            if (contact != null)
            {
               return contact;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        await Task.Yield();
        return null!;

    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        try
        {
            if (_contacts.Count == 0)
            {
                var contacts = await GetAllContactsFromFileToList();
                _contacts = contacts.ToList();
            }
            
            return _contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }
                   
    public async Task<bool> UpdateContactAsync(Contact contact)
    {
        try
        {
            var existingContact = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Address = contact.Address;
            }
            _fileService.WriteToJsonFile(_contacts, filePath);
            return await Task.FromResult(true);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> DeleteContactByEmailAsync(string email)
    {
        try
        {
            var contactToRemove = _contacts.FirstOrDefault(c => c.Email == email);
            if (contactToRemove != null)
            {
                _contacts.Remove(contactToRemove);
                _fileService.WriteToJsonFile(_contacts, filePath);
            }

            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return await Task.FromResult(false);
        }

    }

    public async Task<IEnumerable<Contact>> GetAllContactsFromFileToList()
    {
        try
        {
            if (_fileService.ReadFromJsonFile(filePath) is IEnumerable<Contact> contactsFromFile)
            {
                _contacts.AddRange(contactsFromFile);
            }
            else
            {
                Debug.WriteLine("Error: Unable to read contacts from the file.");
            }

            await Task.Yield();
            return _contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception occurred: {ex}");
            return Enumerable.Empty<Contact>();
        }
    }
}
