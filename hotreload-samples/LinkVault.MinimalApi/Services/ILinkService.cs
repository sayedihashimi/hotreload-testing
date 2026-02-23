using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public interface ILinkService
{
    Task<List<LinkResponse>> GetAllAsync(string? search, int? collectionId, bool? favoritesOnly);
    Task<LinkResponse?> GetByIdAsync(int id);
    Task<LinkResponse> CreateAsync(CreateLinkRequest request);
    Task<LinkResponse?> UpdateAsync(int id, UpdateLinkRequest request);
    Task<bool> DeleteAsync(int id);
    Task<LinkResponse?> RecordClickAsync(int id);
    Task<List<LinkResponse>> GetFavoritesAsync();
}
