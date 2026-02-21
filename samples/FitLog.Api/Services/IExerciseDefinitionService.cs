using FitLog.Api.Models;

namespace FitLog.Api.Services;

public interface IExerciseDefinitionService
{
    Task<List<ExerciseDefinitionResponse>> GetAllAsync();
    Task<ExerciseDefinitionResponse?> GetByIdAsync(int id);
    Task<ExerciseDefinitionResponse> CreateAsync(CreateExerciseDefinitionRequest request);
    Task<ExerciseDefinitionResponse?> UpdateAsync(int id, UpdateExerciseDefinitionRequest request);
}
