using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Data;
using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public class CollectionService(AppDbContext db) : ICollectionService
{
    public async Task<List<CollectionResponse>> GetAllAsync()
    {
        return await db.Collections
            .OrderBy(c => c.Name)
            .Select(c => new CollectionResponse(
                c.Id, c.Name, c.Description, c.Color, c.IsPublic, c.Links.Count))
            .ToListAsync();
    }

    public async Task<CollectionDetailResponse?> GetByIdAsync(int id)
    {
        return await db.Collections
            .Include(c => c.Links).ThenInclude(l => l.LinkTags).ThenInclude(lt => lt.Tag)
            .Where(c => c.Id == id)
            .Select(c => new CollectionDetailResponse(
                c.Id, c.Name, c.Description, c.Color, c.IsPublic,
                c.Links.OrderByDescending(l => l.CreatedAt).Select(l => new LinkResponse(
                    l.Id, l.Url, l.Title, l.Description, l.IsFavorite, l.ClickCount,
                    l.CreatedAt, c.Name, l.LinkTags.Select(lt => lt.Tag.Name).ToList())).ToList()))
            .FirstOrDefaultAsync();
    }

    public async Task<CollectionResponse> CreateAsync(CreateCollectionRequest request)
    {
        var collection = new Collection
        {
            Name = request.Name,
            Description = request.Description,
            Color = request.Color,
            IsPublic = request.IsPublic,
            CreatedAt = DateTime.UtcNow
        };

        db.Collections.Add(collection);
        await db.SaveChangesAsync();

        return new CollectionResponse(collection.Id, collection.Name, collection.Description, collection.Color, collection.IsPublic, 0);
    }

    public async Task<CollectionResponse?> UpdateAsync(int id, UpdateCollectionRequest request)
    {
        var collection = await db.Collections.FindAsync(id);
        if (collection == null) return null;

        if (request.Name is not null) collection.Name = request.Name;
        if (request.Description is not null) collection.Description = request.Description;
        if (request.Color is not null) collection.Color = request.Color;
        if (request.IsPublic.HasValue) collection.IsPublic = request.IsPublic.Value;

        await db.SaveChangesAsync();

        var linkCount = await db.Links.CountAsync(l => l.CollectionId == id);
        return new CollectionResponse(collection.Id, collection.Name, collection.Description, collection.Color, collection.IsPublic, linkCount);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var collection = await db.Collections.FindAsync(id);
        if (collection == null) return false;

        db.Collections.Remove(collection);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<List<LinkResponse>> GetLinksInCollectionAsync(int collectionId)
    {
        return await db.Links
            .Include(l => l.Collection)
            .Include(l => l.LinkTags).ThenInclude(lt => lt.Tag)
            .Where(l => l.CollectionId == collectionId)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new LinkResponse(
                l.Id, l.Url, l.Title, l.Description, l.IsFavorite, l.ClickCount,
                l.CreatedAt, l.Collection.Name, l.LinkTags.Select(lt => lt.Tag.Name).ToList()))
            .ToListAsync();
    }
}
