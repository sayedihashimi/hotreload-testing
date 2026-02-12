using Microsoft.EntityFrameworkCore;
using ContactsManager.Console.Data;
using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public class TagService : ITagService
{
    private readonly ContactsDbContext _context;

    public TagService(ContactsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags
            .Include(t => t.ContactTags)
            .ThenInclude(ct => ct.Contact)
            .ToListAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(int id)
    {
        return await _context.Tags
            .Include(t => t.ContactTags)
            .ThenInclude(ct => ct.Contact)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tag> AddTagAsync(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task DeleteTagAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddTagToContactAsync(int contactId, int tagId)
    {
        var contactTag = new ContactTag
        {
            ContactId = contactId,
            TagId = tagId
        };
        _context.ContactTags.Add(contactTag);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveTagFromContactAsync(int contactId, int tagId)
    {
        var contactTag = await _context.ContactTags
            .FirstOrDefaultAsync(ct => ct.ContactId == contactId && ct.TagId == tagId);
        
        if (contactTag != null)
        {
            _context.ContactTags.Remove(contactTag);
            await _context.SaveChangesAsync();
        }
    }
}
