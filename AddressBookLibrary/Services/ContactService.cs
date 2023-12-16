using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using System.Diagnostics;

namespace AddressBookLibrary.Services
{
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

        public async Task<bool> DeleteContactByEmailAsync(string email)
        {
            try
            {
                var contactToRemove = _contacts.FirstOrDefault(c => c.Email == email);
                if (contactToRemove != null)
                {
                    _contacts.Remove(contactToRemove);
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
    
        }

        public async Task<List<Contact>> GetAllContactsAsync()
        {
            try
            {
                
                if (_contacts.Count == 0)
                {
                    List<Contact> contacts = await GetAllContactsFromFileToList();
                    _contacts = contacts;
                }
                await Task.Yield();
                return _contacts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
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
                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return await Task.FromResult(false);
            }
        }

        private async Task<List<Contact>> GetAllContactsFromFileToList()
        {
            try
            {
                _contacts.AddRange((IEnumerable<Contact>)_fileService.ReadFromJsonFile(filePath));
                await Task.Yield();
                return _contacts;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null!;
            }
           
        }

    }
}
