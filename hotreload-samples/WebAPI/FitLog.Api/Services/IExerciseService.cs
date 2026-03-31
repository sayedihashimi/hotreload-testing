using FitLog.Api.Models;

namespace FitLog.Api.Services;

public interface IExerciseService
{
    Task<ExerciseResponse> AddToWorkoutAsync(int workoutId, CreateExerciseRequest request);
    Task<ExerciseResponse?> UpdateAsync(int workoutId, int exerciseId, UpdateExerciseRequest request);
    Task<bool> DeleteAsync(int workoutId, int exerciseId);
}
