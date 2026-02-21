using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public interface ICollectionService
{
    Task<List<CollectionResponse>> GetAllAsync();
    Task<CollectionDetailResponse?> GetByIdAsync(int id);
    Task<CollectionResponse> CreateAsync(CreateCollectionRequest request);
    Task<CollectionResponse?> UpdateAsync(int id, UpdateCollectionRequest request);
    Task<bool> DeleteAsync(int id);
    Task<List<LinkResponse>> GetLinksInCollectionAsync(int collectionId);
}
