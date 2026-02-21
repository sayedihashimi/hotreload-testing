using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public interface ITagService
{
    Task<List<TagResponse>> GetAllAsync();
    Task<TagResponse> CreateAsync(CreateTagRequest request);
    Task<bool> DeleteAsync(int id);
}
