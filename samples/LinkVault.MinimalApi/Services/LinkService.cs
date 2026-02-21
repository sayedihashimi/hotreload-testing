using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Data;
using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public class LinkService(AppDbContext db) : ILinkService
{
    public async Task<List<LinkResponse>> GetAllAsync(string? search, int? collectionId, bool? favoritesOnly)
    {
        var query = db.Links
            .Include(l => l.Collection)
            .Include(l => l.LinkTags).ThenInclude(lt => lt.Tag)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(l => l.Title.Contains(search) || l.Url.Contains(search) || (l.Description != null && l.Description.Contains(search)));
        }

        if (collectionId.HasValue)
        {
            query = query.Where(l => l.CollectionId == collectionId.Value);
        }

        if (favoritesOnly == true)
        {
            query = query.Where(l => l.IsFavorite);
        }

        return await query
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new LinkResponse(
                l.Id, l.Url, l.Title, l.Description, l.IsFavorite, l.ClickCount,
                l.CreatedAt, l.Collection.Name, l.LinkTags.Select(lt => lt.Tag.Name).ToList()))
            .ToListAsync();
    }

    public async Task<LinkResponse?> GetByIdAsync(int id)
    {
        return await db.Links
            .Include(l => l.Collection)
            .Include(l => l.LinkTags).ThenInclude(lt => lt.Tag)
            .Where(l => l.Id == id)
            .Select(l => new LinkResponse(
                l.Id, l.Url, l.Title, l.Description, l.IsFavorite, l.ClickCount,
                l.CreatedAt, l.Collection.Name, l.LinkTags.Select(lt => lt.Tag.Name).ToList()))
            .FirstOrDefaultAsync();
    }

    public async Task<LinkResponse> CreateAsync(CreateLinkRequest request)
    {
        var link = new Link
        {
            Url = request.Url,
            Title = request.Title,
            Description = request.Description,
            CollectionId = request.CollectionId,
            CreatedAt = DateTime.UtcNow
        };

        db.Links.Add(link);
        await db.SaveChangesAsync();

        if (request.Tags is { Count: > 0 })
        {
            foreach (var tagName in request.Tags)
            {
                var tag = await db.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    db.Tags.Add(tag);
                    await db.SaveChangesAsync();
                }
                db.LinkTags.Add(new LinkTag { LinkId = link.Id, TagId = tag.Id });
            }
            await db.SaveChangesAsync();
        }

        return (await GetByIdAsync(link.Id))!;
    }

    public async Task<LinkResponse?> UpdateAsync(int id, UpdateLinkRequest request)
    {
        var link = await db.Links.FindAsync(id);
        if (link == null) return null;

        if (request.Title is not null) link.Title = request.Title;
        if (request.Description is not null) link.Description = request.Description;
        if (request.IsFavorite.HasValue) link.IsFavorite = request.IsFavorite.Value;
        if (request.CollectionId.HasValue) link.CollectionId = request.CollectionId.Value;

        await db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var link = await db.Links.FindAsync(id);
        if (link == null) return false;

        db.Links.Remove(link);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<LinkResponse?> RecordClickAsync(int id)
    {
        var link = await db.Links.FindAsync(id);
        if (link == null) return null;

        link.ClickCount++;
        link.LastClickedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<List<LinkResponse>> GetFavoritesAsync()
    {
        return await GetAllAsync(null, null, true);
    }
}
