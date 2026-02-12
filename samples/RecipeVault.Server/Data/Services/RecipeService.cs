using Microsoft.EntityFrameworkCore;
using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public class RecipeService : IRecipeService
{
    private readonly RecipeDbContext _context;

    public RecipeService(RecipeDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions.OrderBy(i => i.StepNumber))
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        recipe.CreatedAt = DateTime.UtcNow;
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> UpdateRecipeAsync(Recipe recipe)
    {
        var existing = await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.RecipeTags)
            .FirstOrDefaultAsync(r => r.Id == recipe.Id);

        if (existing == null)
            return null;

        existing.Name = recipe.Name;
        existing.Description = recipe.Description;
        existing.PrepTimeMinutes = recipe.PrepTimeMinutes;
        existing.CookTimeMinutes = recipe.CookTimeMinutes;
        existing.Servings = recipe.Servings;
        existing.Difficulty = recipe.Difficulty;
        existing.ImageUrl = recipe.ImageUrl;

        _context.Ingredients.RemoveRange(existing.Ingredients);
        _context.Instructions.RemoveRange(existing.Instructions);
        _context.RecipeTags.RemoveRange(existing.RecipeTags);

        existing.Ingredients = recipe.Ingredients;
        existing.Instructions = recipe.Instructions;
        existing.RecipeTags = recipe.RecipeTags;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
            return false;

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
        return true;
    }
}
