using Microsoft.EntityFrameworkCore;
using FitLog.Api.Data;
using FitLog.Api.Models;

namespace FitLog.Api.Services;

public class ExerciseDefinitionService(AppDbContext db) : IExerciseDefinitionService
{
    public async Task<List<ExerciseDefinitionResponse>> GetAllAsync()
    {
        return await db.ExerciseDefinitions
            .OrderBy(ed => ed.Name)
            .Select(ed => new ExerciseDefinitionResponse(
                ed.Id, ed.Name, ed.Description, ed.PrimaryMuscleGroup, ed.SecondaryMuscleGroup, ed.Category))
            .ToListAsync();
    }

    public async Task<ExerciseDefinitionResponse?> GetByIdAsync(int id)
    {
        return await db.ExerciseDefinitions
            .Where(ed => ed.Id == id)
            .Select(ed => new ExerciseDefinitionResponse(
                ed.Id, ed.Name, ed.Description, ed.PrimaryMuscleGroup, ed.SecondaryMuscleGroup, ed.Category))
            .FirstOrDefaultAsync();
    }

    public async Task<ExerciseDefinitionResponse> CreateAsync(CreateExerciseDefinitionRequest request)
    {
        var definition = new ExerciseDefinition
        {
            Name = request.Name,
            Description = request.Description,
            PrimaryMuscleGroup = request.PrimaryMuscleGroup,
            SecondaryMuscleGroup = request.SecondaryMuscleGroup,
            Category = request.Category
        };

        db.ExerciseDefinitions.Add(definition);
        await db.SaveChangesAsync();

        return new ExerciseDefinitionResponse(definition.Id, definition.Name, definition.Description,
            definition.PrimaryMuscleGroup, definition.SecondaryMuscleGroup, definition.Category);
    }

    public async Task<ExerciseDefinitionResponse?> UpdateAsync(int id, UpdateExerciseDefinitionRequest request)
    {
        var definition = await db.ExerciseDefinitions.FindAsync(id);
        if (definition == null) return null;

        if (request.Name is not null) definition.Name = request.Name;
        if (request.Description is not null) definition.Description = request.Description;
        if (request.PrimaryMuscleGroup.HasValue) definition.PrimaryMuscleGroup = request.PrimaryMuscleGroup.Value;
        if (request.SecondaryMuscleGroup.HasValue) definition.SecondaryMuscleGroup = request.SecondaryMuscleGroup.Value;
        if (request.Category.HasValue) definition.Category = request.Category.Value;

        await db.SaveChangesAsync();

        return new ExerciseDefinitionResponse(definition.Id, definition.Name, definition.Description,
            definition.PrimaryMuscleGroup, definition.SecondaryMuscleGroup, definition.Category);
    }
}
