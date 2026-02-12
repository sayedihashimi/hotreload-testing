using Microsoft.EntityFrameworkCore;
using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public class SearchService : ISearchService
{
    private readonly RecipeDbContext _context;

    public SearchService(RecipeDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> SearchRecipesAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await _context.Recipes
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .ToListAsync();

        var lowerQuery = query.ToLower();
        return await _context.Recipes
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .Where(r => r.Name.ToLower().Contains(lowerQuery) ||
                       r.Description.ToLower().Contains(lowerQuery))
            .ToListAsync();
    }

    public async Task<List<Recipe>> FilterByTagsAsync(List<int> tagIds)
    {
        if (!tagIds.Any())
            return await _context.Recipes
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .ToListAsync();

        return await _context.Recipes
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .Where(r => r.RecipeTags.Any(rt => tagIds.Contains(rt.TagId)))
            .ToListAsync();
    }

    public async Task<List<Recipe>> FilterByDifficultyAsync(DifficultyLevel difficulty)
    {
        return await _context.Recipes
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .Where(r => r.Difficulty == difficulty)
            .ToListAsync();
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.OrderBy(t => t.Name).ToListAsync();
    }
}
