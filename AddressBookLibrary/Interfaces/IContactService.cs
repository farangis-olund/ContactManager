
using AddressBookLibrary.Models;

namespace AddressBookLibrary.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByEmailAsync(string email);
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactByEmailAsync(string email);
       
    }
}
