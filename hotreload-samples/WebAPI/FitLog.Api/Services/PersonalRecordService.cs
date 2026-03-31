using Microsoft.EntityFrameworkCore;
using FitLog.Api.Data;
using FitLog.Api.Models;

namespace FitLog.Api.Services;

public class PersonalRecordService(AppDbContext db) : IPersonalRecordService
{
    public async Task<List<PersonalRecordResponse>> GetAllAsync()
    {
        return await db.PersonalRecords
            .Include(pr => pr.ExerciseDefinition)
            .OrderByDescending(pr => pr.AchievedDate)
            .Select(pr => new PersonalRecordResponse(
                pr.Id, pr.ExerciseDefinition.Name, pr.WeightKg, pr.Reps, pr.AchievedDate))
            .ToListAsync();
    }

    public async Task<List<PersonalRecordResponse>> GetByExerciseAsync(int exerciseDefinitionId)
    {
        return await db.PersonalRecords
            .Include(pr => pr.ExerciseDefinition)
            .Where(pr => pr.ExerciseDefinitionId == exerciseDefinitionId)
            .OrderByDescending(pr => pr.AchievedDate)
            .Select(pr => new PersonalRecordResponse(
                pr.Id, pr.ExerciseDefinition.Name, pr.WeightKg, pr.Reps, pr.AchievedDate))
            .ToListAsync();
    }

    public async Task<PersonalRecordResponse> CreateAsync(CreatePersonalRecordRequest request)
    {
        var record = new PersonalRecord
        {
            ExerciseDefinitionId = request.ExerciseDefinitionId,
            WeightKg = request.WeightKg,
            Reps = request.Reps,
            AchievedDate = request.AchievedDate
        };

        db.PersonalRecords.Add(record);
        await db.SaveChangesAsync();

        var def = await db.ExerciseDefinitions.FindAsync(request.ExerciseDefinitionId);
        return new PersonalRecordResponse(record.Id, def?.Name ?? "Unknown", record.WeightKg, record.Reps, record.AchievedDate);
    }
}
