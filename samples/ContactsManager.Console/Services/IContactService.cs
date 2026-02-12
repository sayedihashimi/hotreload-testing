using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
    Task<Contact> AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
}
