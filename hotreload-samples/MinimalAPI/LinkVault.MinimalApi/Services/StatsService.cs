using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Data;
using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public class StatsService(AppDbContext db) : IStatsService
{
    public async Task<StatsResponse> GetStatsAsync()
    {
        var totalLinks = await db.Links.CountAsync();
        var totalCollections = await db.Collections.CountAsync();
        var totalClicks = await db.Links.SumAsync(l => l.ClickCount);
        var favoriteCount = await db.Links.CountAsync(l => l.IsFavorite);

        return new StatsResponse(totalLinks, totalCollections, totalClicks, favoriteCount);
    }

    public async Task<List<TopClickedResponse>> GetTopClickedAsync(int count = 10)
    {
        return await db.Links
            .Include(l => l.Collection)
            .OrderByDescending(l => l.ClickCount)
            .Take(count)
            .Select(l => new TopClickedResponse(l.Id, l.Url, l.Title, l.ClickCount, l.Collection.Name))
            .ToListAsync();
    }
}
