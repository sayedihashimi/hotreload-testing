using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<Recipe> CreateRecipeAsync(Recipe recipe);
    Task<Recipe?> UpdateRecipeAsync(Recipe recipe);
    Task<bool> DeleteRecipeAsync(int id);
}
