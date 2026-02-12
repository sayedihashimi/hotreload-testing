using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public interface ISearchService
{
    Task<List<Recipe>> SearchRecipesAsync(string query);
    Task<List<Recipe>> FilterByTagsAsync(List<int> tagIds);
    Task<List<Recipe>> FilterByDifficultyAsync(DifficultyLevel difficulty);
    Task<List<Tag>> GetAllTagsAsync();
}
