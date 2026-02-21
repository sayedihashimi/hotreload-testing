using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Data;
using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public class TagService(AppDbContext db) : ITagService
{
    public async Task<List<TagResponse>> GetAllAsync()
    {
        return await db.Tags
            .OrderBy(t => t.Name)
            .Select(t => new TagResponse(t.Id, t.Name, t.LinkTags.Count))
            .ToListAsync();
    }

    public async Task<TagResponse> CreateAsync(CreateTagRequest request)
    {
        var tag = new Tag { Name = request.Name };
        db.Tags.Add(tag);
        await db.SaveChangesAsync();
        return new TagResponse(tag.Id, tag.Name, 0);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await db.Tags.FindAsync(id);
        if (tag == null) return false;

        db.Tags.Remove(tag);
        await db.SaveChangesAsync();
        return true;
    }
}
