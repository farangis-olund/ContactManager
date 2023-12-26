using AddressBookLibrary.Models;

namespace AddressBookLibrary.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Adds a contact asynchronously to the collection and writes the updated contacts to a JSON file.
    /// </summary>
    /// <param name="contact">The contact object to be added.</param>
    /// <returns>A task representing the operation's success (true) or failure (false).</returns>
    Task<bool> AddContactAsync(Contact contact);

    /// <summary>
    /// Retrieves a contact asynchronously based on the provided email address.
    /// </summary>
    /// <param name="email">The email address of the contact to be retrieved.</param>
    /// <returns>
    /// A task representing the contact object if found; otherwise, returns null.
    /// </returns>
    Task<Contact> GetContactByEmailAsync(string email);

    /// <summary>
    /// Retrieves all contacts asynchronously. If the contacts list is empty,
    /// loads contacts from a file and returns the list of contacts.
    /// </summary>
    /// <returns>
    /// A task representing the list of contacts. If an error occurs,
    /// an empty list is returned.
    /// </returns>
    Task<IEnumerable<Contact>> GetAllContactsAsync();

    /// <summary>
    /// Updates an existing contact asynchronously with the provided contact information.
    /// </summary>
    /// <param name="contact">The updated contact information.</param>
    /// <returns>
    /// A task representing the success (true) or failure (false) of the update operation.
    /// </returns>
    Task<bool> UpdateContactAsync(Contact contact);

    /// <summary>
    /// Deletes a contact asynchronously based on the provided email address.
    /// </summary>
    /// <param name="email">The email address of the contact to be deleted.</param>
    /// <returns>
    /// A task representing the success (true) or failure (false) of the delete operation.
    /// </returns>
    Task<bool> DeleteContactByEmailAsync(string email);

    /// <summary>
    /// Retrieves all contacts from a JSON file and returns them as a list asynchronously.
    /// </summary>
    /// <returns>
    /// A task representing the list of contacts retrieved from the file. 
    /// If an error occurs, returns null.
    /// </returns>
    Task<IEnumerable<Contact>> GetAllContactsFromFileToList();

}
