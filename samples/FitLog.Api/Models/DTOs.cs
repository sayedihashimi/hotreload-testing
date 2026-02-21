namespace FitLog.Api.Models;

// Request DTOs
public record CreateWorkoutRequest(string Name, DateTime Date, int DurationMinutes, WorkoutType Type, int CaloriesBurned, string? Notes, List<CreateExerciseRequest>? Exercises);
public record CreateExerciseRequest(int ExerciseDefinitionId, int Sets, int Reps, decimal? WeightKg, int? DurationSeconds);
public record UpdateWorkoutRequest(string? Name, DateTime? Date, int? DurationMinutes, WorkoutType? Type, int? CaloriesBurned, string? Notes);
public record UpdateExerciseRequest(int? Sets, int? Reps, decimal? WeightKg, int? DurationSeconds);

public record CreateExerciseDefinitionRequest(string Name, string? Description, MuscleGroup PrimaryMuscleGroup, MuscleGroup? SecondaryMuscleGroup, ExerciseCategory Category);
public record UpdateExerciseDefinitionRequest(string? Name, string? Description, MuscleGroup? PrimaryMuscleGroup, MuscleGroup? SecondaryMuscleGroup, ExerciseCategory? Category);

public record CreatePersonalRecordRequest(int ExerciseDefinitionId, decimal WeightKg, int Reps, DateTime AchievedDate);

// Response DTOs
public record WorkoutResponse(int Id, string Name, DateTime Date, int DurationMinutes, WorkoutType Type, int CaloriesBurned, string? Notes, List<ExerciseResponse> Exercises);
public record WorkoutSummaryResponse(int Id, string Name, DateTime Date, int DurationMinutes, WorkoutType Type, int CaloriesBurned, int ExerciseCount);
public record ExerciseResponse(int Id, string ExerciseName, int Sets, int Reps, decimal? WeightKg, int? DurationSeconds, string PrimaryMuscleGroup);

public record ExerciseDefinitionResponse(int Id, string Name, string? Description, MuscleGroup PrimaryMuscleGroup, MuscleGroup? SecondaryMuscleGroup, ExerciseCategory Category);

public record PersonalRecordResponse(int Id, string ExerciseName, decimal WeightKg, int Reps, DateTime AchievedDate);

public record WeeklySummaryResponse(int TotalWorkouts, int TotalMinutes, int TotalCalories, Dictionary<string, int> WorkoutsByType);
public record MonthlySummaryResponse(int TotalWorkouts, int TotalMinutes, int TotalCalories, int UniqueExercises, Dictionary<string, int> WorkoutsByType);
public record MuscleGroupBreakdownResponse(string MuscleGroup, int TotalSets, int TotalExercises);
public record ProgressResponse(string ExerciseName, List<ProgressEntryResponse> Entries);
public record ProgressEntryResponse(DateTime Date, decimal? MaxWeight, int MaxReps);
