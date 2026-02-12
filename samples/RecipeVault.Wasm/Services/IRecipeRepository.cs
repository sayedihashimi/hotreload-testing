using RecipeVault.Wasm.Models;

namespace RecipeVault.Wasm.Services;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(string id);
    Task<Recipe> AddAsync(Recipe recipe);
    Task<Recipe> UpdateAsync(Recipe recipe);
    Task DeleteAsync(string id);
    Task<int> ImportAsync(List<Recipe> recipes);
}
