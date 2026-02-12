using RecipeVault.Wasm.Models;

namespace RecipeVault.Wasm.Services;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<Recipe> AddAsync(Recipe recipe);
    Task<Recipe> UpdateAsync(Recipe recipe);
    Task DeleteAsync(int id);
    Task<List<string>> GetAllTagsAsync();
    Task ImportRecipesAsync(List<Recipe> recipes);
}
