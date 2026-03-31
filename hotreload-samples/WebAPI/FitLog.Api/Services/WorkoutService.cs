using Microsoft.EntityFrameworkCore;
using FitLog.Api.Data;
using FitLog.Api.Models;

namespace FitLog.Api.Services;

public class WorkoutService(AppDbContext db) : IWorkoutService
{
    public async Task<List<WorkoutSummaryResponse>> GetAllAsync(DateTime? fromDate, DateTime? toDate, WorkoutType? type)
    {
        var query = db.Workouts
            .Include(w => w.Exercises)
            .AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(w => w.Date >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(w => w.Date <= toDate.Value);
        if (type.HasValue)
            query = query.Where(w => w.Type == type.Value);

        return await query
            .OrderByDescending(w => w.Date)
            .Select(w => new WorkoutSummaryResponse(
                w.Id, w.Name, w.Date, w.DurationMinutes, w.Type, w.CaloriesBurned, w.Exercises.Count))
            .ToListAsync();
    }

    public async Task<WorkoutResponse?> GetByIdAsync(int id)
    {
        return await db.Workouts
            .Include(w => w.Exercises).ThenInclude(e => e.ExerciseDefinition)
            .Where(w => w.Id == id)
            .Select(w => new WorkoutResponse(
                w.Id, w.Name, w.Date, w.DurationMinutes, w.Type, w.CaloriesBurned, w.Notes,
                w.Exercises.OrderBy(e => e.OrderIndex).Select(e => new ExerciseResponse(
                    e.Id, e.ExerciseDefinition.Name, e.Sets, e.Reps, e.WeightKg, e.DurationSeconds,
                    e.ExerciseDefinition.PrimaryMuscleGroup.ToString())).ToList()))
            .FirstOrDefaultAsync();
    }

    public async Task<WorkoutResponse> CreateAsync(CreateWorkoutRequest request)
    {
        var workout = new Workout
        {
            Name = request.Name,
            Date = request.Date,
            DurationMinutes = request.DurationMinutes,
            Type = request.Type,
            CaloriesBurned = request.CaloriesBurned,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        db.Workouts.Add(workout);
        await db.SaveChangesAsync();

        if (request.Exercises is { Count: > 0 })
        {
            for (var i = 0; i < request.Exercises.Count; i++)
            {
                var exReq = request.Exercises[i];
                var def = await db.ExerciseDefinitions.FindAsync(exReq.ExerciseDefinitionId);
                db.Exercises.Add(new Exercise
                {
                    WorkoutId = workout.Id,
                    Name = def?.Name ?? "Unknown",
                    Sets = exReq.Sets,
                    Reps = exReq.Reps,
                    WeightKg = exReq.WeightKg,
                    DurationSeconds = exReq.DurationSeconds,
                    OrderIndex = i + 1,
                    ExerciseDefinitionId = exReq.ExerciseDefinitionId
                });
            }
            await db.SaveChangesAsync();
        }

        return (await GetByIdAsync(workout.Id))!;
    }

    public async Task<WorkoutResponse?> UpdateAsync(int id, UpdateWorkoutRequest request)
    {
        var workout = await db.Workouts.FindAsync(id);
        if (workout == null) return null;

        if (request.Name is not null) workout.Name = request.Name;
        if (request.Date.HasValue) workout.Date = request.Date.Value;
        if (request.DurationMinutes.HasValue) workout.DurationMinutes = request.DurationMinutes.Value;
        if (request.Type.HasValue) workout.Type = request.Type.Value;
        if (request.CaloriesBurned.HasValue) workout.CaloriesBurned = request.CaloriesBurned.Value;
        if (request.Notes is not null) workout.Notes = request.Notes;

        await db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var workout = await db.Workouts.FindAsync(id);
        if (workout == null) return false;

        db.Workouts.Remove(workout);
        await db.SaveChangesAsync();
        return true;
    }
}
