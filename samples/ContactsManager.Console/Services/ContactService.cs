using Microsoft.EntityFrameworkCore;
using ContactsManager.Console.Data;
using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public class ContactService : IContactService
{
    private readonly ContactsDbContext _context;

    public ContactService(ContactsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts
            .Include(c => c.ContactTags)
            .ThenInclude(ct => ct.Tag)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.ContactTags)
            .ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
    {
        var lowerSearch = searchTerm.ToLower();
        return await _context.Contacts
            .Include(c => c.ContactTags)
            .ThenInclude(ct => ct.Tag)
            .Where(c => c.FirstName.ToLower().Contains(lowerSearch) ||
                        c.LastName.ToLower().Contains(lowerSearch) ||
                        c.Email.ToLower().Contains(lowerSearch) ||
                        c.Company.ToLower().Contains(lowerSearch) ||
                        c.Phone.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<Contact> AddContactAsync(Contact contact)
    {
        contact.CreatedAt = DateTime.Now;
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
