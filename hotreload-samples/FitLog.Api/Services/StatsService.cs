using Microsoft.EntityFrameworkCore;
using FitLog.Api.Data;
using FitLog.Api.Models;

namespace FitLog.Api.Services;

public class StatsService(AppDbContext db) : IStatsService
{
    public async Task<WeeklySummaryResponse> GetWeeklySummaryAsync()
    {
        var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var endOfWeek = startOfWeek.AddDays(7);

        var workouts = await db.Workouts
            .Where(w => w.Date >= startOfWeek && w.Date < endOfWeek)
            .ToListAsync();

        var byType = workouts
            .GroupBy(w => w.Type.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        return new WeeklySummaryResponse(
            workouts.Count,
            workouts.Sum(w => w.DurationMinutes),
            workouts.Sum(w => w.CaloriesBurned),
            byType);
    }

    public async Task<MonthlySummaryResponse> GetMonthlySummaryAsync()
    {
        var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        var workouts = await db.Workouts
            .Include(w => w.Exercises)
            .Where(w => w.Date >= startOfMonth && w.Date < endOfMonth)
            .ToListAsync();

        var byType = workouts
            .GroupBy(w => w.Type.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        var uniqueExercises = workouts
            .SelectMany(w => w.Exercises)
            .Select(e => e.ExerciseDefinitionId)
            .Distinct()
            .Count();

        return new MonthlySummaryResponse(
            workouts.Count,
            workouts.Sum(w => w.DurationMinutes),
            workouts.Sum(w => w.CaloriesBurned),
            uniqueExercises,
            byType);
    }

    public async Task<List<MuscleGroupBreakdownResponse>> GetMuscleGroupBreakdownAsync()
    {
        return await db.Exercises
            .Include(e => e.ExerciseDefinition)
            .GroupBy(e => e.ExerciseDefinition.PrimaryMuscleGroup)
            .Select(g => new MuscleGroupBreakdownResponse(
                g.Key.ToString(),
                g.Sum(e => e.Sets),
                g.Count()))
            .OrderByDescending(r => r.TotalSets)
            .ToListAsync();
    }

    public async Task<ProgressResponse?> GetProgressAsync(int exerciseDefinitionId)
    {
        var def = await db.ExerciseDefinitions.FindAsync(exerciseDefinitionId);
        if (def == null) return null;

        var entries = await db.Exercises
            .Include(e => e.Workout)
            .Where(e => e.ExerciseDefinitionId == exerciseDefinitionId)
            .OrderBy(e => e.Workout.Date)
            .GroupBy(e => e.Workout.Date.Date)
            .Select(g => new ProgressEntryResponse(
                g.Key,
                g.Max(e => e.WeightKg),
                g.Max(e => e.Reps)))
            .ToListAsync();

        return new ProgressResponse(def.Name, entries);
    }
}
