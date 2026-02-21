using FitLog.Api.Models;

namespace FitLog.Api.Services;

public interface IPersonalRecordService
{
    Task<List<PersonalRecordResponse>> GetAllAsync();
    Task<List<PersonalRecordResponse>> GetByExerciseAsync(int exerciseDefinitionId);
    Task<PersonalRecordResponse> CreateAsync(CreatePersonalRecordRequest request);
}
