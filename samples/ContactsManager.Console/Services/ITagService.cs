using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag?> GetTagByIdAsync(int id);
    Task<Tag> AddTagAsync(Tag tag);
    Task DeleteTagAsync(int id);
    Task AddTagToContactAsync(int contactId, int tagId);
    Task RemoveTagFromContactAsync(int contactId, int tagId);
}
