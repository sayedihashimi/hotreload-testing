using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Services;

public interface IStatsService
{
    Task<StatsResponse> GetStatsAsync();
    Task<List<TopClickedResponse>> GetTopClickedAsync(int count = 10);
}
