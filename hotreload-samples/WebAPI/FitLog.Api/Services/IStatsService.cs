using FitLog.Api.Models;

namespace FitLog.Api.Services;

public interface IStatsService
{
    Task<WeeklySummaryResponse> GetWeeklySummaryAsync();
    Task<MonthlySummaryResponse> GetMonthlySummaryAsync();
    Task<List<MuscleGroupBreakdownResponse>> GetMuscleGroupBreakdownAsync();
    Task<ProgressResponse?> GetProgressAsync(int exerciseDefinitionId);
}
