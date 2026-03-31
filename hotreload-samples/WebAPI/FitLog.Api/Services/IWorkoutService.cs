using FitLog.Api.Models;

namespace FitLog.Api.Services;

public interface IWorkoutService
{
    Task<List<WorkoutSummaryResponse>> GetAllAsync(DateTime? fromDate, DateTime? toDate, WorkoutType? type);
    Task<WorkoutResponse?> GetByIdAsync(int id);
    Task<WorkoutResponse> CreateAsync(CreateWorkoutRequest request);
    Task<WorkoutResponse?> UpdateAsync(int id, UpdateWorkoutRequest request);
    Task<bool> DeleteAsync(int id);
}
