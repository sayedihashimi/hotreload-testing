using Microsoft.EntityFrameworkCore;
using FitLog.Api.Data;
using FitLog.Api.Models;

namespace FitLog.Api.Services;

public class ExerciseService(AppDbContext db) : IExerciseService
{
    public async Task<ExerciseResponse> AddToWorkoutAsync(int workoutId, CreateExerciseRequest request)
    {
        var maxOrder = await db.Exercises
            .Where(e => e.WorkoutId == workoutId)
            .MaxAsync(e => (int?)e.OrderIndex) ?? 0;

        var def = await db.ExerciseDefinitions.FindAsync(request.ExerciseDefinitionId);

        var exercise = new Exercise
        {
            WorkoutId = workoutId,
            Name = def?.Name ?? "Unknown",
            Sets = request.Sets,
            Reps = request.Reps,
            WeightKg = request.WeightKg,
            DurationSeconds = request.DurationSeconds,
            OrderIndex = maxOrder + 1,
            ExerciseDefinitionId = request.ExerciseDefinitionId
        };

        db.Exercises.Add(exercise);
        await db.SaveChangesAsync();

        return new ExerciseResponse(
            exercise.Id, def?.Name ?? "Unknown", exercise.Sets, exercise.Reps,
            exercise.WeightKg, exercise.DurationSeconds, def?.PrimaryMuscleGroup.ToString() ?? "Unknown");
    }

    public async Task<ExerciseResponse?> UpdateAsync(int workoutId, int exerciseId, UpdateExerciseRequest request)
    {
        var exercise = await db.Exercises
            .Include(e => e.ExerciseDefinition)
            .FirstOrDefaultAsync(e => e.Id == exerciseId && e.WorkoutId == workoutId);

        if (exercise == null) return null;

        if (request.Sets.HasValue) exercise.Sets = request.Sets.Value;
        if (request.Reps.HasValue) exercise.Reps = request.Reps.Value;
        if (request.WeightKg.HasValue) exercise.WeightKg = request.WeightKg.Value;
        if (request.DurationSeconds.HasValue) exercise.DurationSeconds = request.DurationSeconds.Value;

        await db.SaveChangesAsync();

        return new ExerciseResponse(
            exercise.Id, exercise.ExerciseDefinition.Name, exercise.Sets, exercise.Reps,
            exercise.WeightKg, exercise.DurationSeconds, exercise.ExerciseDefinition.PrimaryMuscleGroup.ToString());
    }

    public async Task<bool> DeleteAsync(int workoutId, int exerciseId)
    {
        var exercise = await db.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId && e.WorkoutId == workoutId);
        if (exercise == null) return false;

        db.Exercises.Remove(exercise);
        await db.SaveChangesAsync();
        return true;
    }
}
