using Microsoft.EntityFrameworkCore;
using FitLog.Api.Models;

namespace FitLog.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseDefinition> ExerciseDefinitions { get; set; }
    public DbSet<PersonalRecord> PersonalRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Exercise>()
            .Property(e => e.WeightKg)
            .HasColumnType("decimal(8,2)");

        modelBuilder.Entity<PersonalRecord>()
            .Property(pr => pr.WeightKg)
            .HasColumnType("decimal(8,2)");

        modelBuilder.Entity<Exercise>()
            .HasOne(e => e.Workout)
            .WithMany(w => w.Exercises)
            .HasForeignKey(e => e.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exercise>()
            .HasOne(e => e.ExerciseDefinition)
            .WithMany(ed => ed.Exercises)
            .HasForeignKey(e => e.ExerciseDefinitionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PersonalRecord>()
            .HasOne(pr => pr.ExerciseDefinition)
            .WithMany()
            .HasForeignKey(pr => pr.ExerciseDefinitionId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var exerciseDefinitions = new[]
        {
            new ExerciseDefinition { Id = 1, Name = "Barbell Bench Press", Description = "Flat bench press with barbell", PrimaryMuscleGroup = MuscleGroup.Chest, SecondaryMuscleGroup = MuscleGroup.Arms, Category = ExerciseCategory.Barbell },
            new ExerciseDefinition { Id = 2, Name = "Barbell Squat", Description = "Back squat with barbell", PrimaryMuscleGroup = MuscleGroup.Legs, SecondaryMuscleGroup = MuscleGroup.Core, Category = ExerciseCategory.Barbell },
            new ExerciseDefinition { Id = 3, Name = "Deadlift", Description = "Conventional deadlift", PrimaryMuscleGroup = MuscleGroup.Back, SecondaryMuscleGroup = MuscleGroup.Legs, Category = ExerciseCategory.Barbell },
            new ExerciseDefinition { Id = 4, Name = "Pull-Up", Description = "Bodyweight pull-up", PrimaryMuscleGroup = MuscleGroup.Back, SecondaryMuscleGroup = MuscleGroup.Arms, Category = ExerciseCategory.Bodyweight },
            new ExerciseDefinition { Id = 5, Name = "Dumbbell Shoulder Press", Description = "Overhead press with dumbbells", PrimaryMuscleGroup = MuscleGroup.Shoulders, SecondaryMuscleGroup = MuscleGroup.Arms, Category = ExerciseCategory.Dumbbell },
            new ExerciseDefinition { Id = 6, Name = "Plank", Description = "Core stabilization hold", PrimaryMuscleGroup = MuscleGroup.Core, SecondaryMuscleGroup = null, Category = ExerciseCategory.Bodyweight },
            new ExerciseDefinition { Id = 7, Name = "Cable Fly", Description = "Chest fly on cable machine", PrimaryMuscleGroup = MuscleGroup.Chest, SecondaryMuscleGroup = null, Category = ExerciseCategory.Cable },
            new ExerciseDefinition { Id = 8, Name = "Leg Press", Description = "Machine leg press", PrimaryMuscleGroup = MuscleGroup.Legs, SecondaryMuscleGroup = null, Category = ExerciseCategory.Machine },
            new ExerciseDefinition { Id = 9, Name = "Treadmill Run", Description = "Running on treadmill", PrimaryMuscleGroup = MuscleGroup.Legs, SecondaryMuscleGroup = MuscleGroup.FullBody, Category = ExerciseCategory.Cardio },
            new ExerciseDefinition { Id = 10, Name = "Dumbbell Bicep Curl", Description = "Bicep curl with dumbbells", PrimaryMuscleGroup = MuscleGroup.Arms, SecondaryMuscleGroup = null, Category = ExerciseCategory.Dumbbell }
        };
        modelBuilder.Entity<ExerciseDefinition>().HasData(exerciseDefinitions);

        var now = new DateTime(2026, 2, 18);

        var workouts = new[]
        {
            new Workout { Id = 1, Name = "Upper Body Strength", Date = now.AddDays(-13), DurationMinutes = 55, Type = WorkoutType.Strength, CaloriesBurned = 350, Notes = "Felt strong today", CreatedAt = now.AddDays(-13) },
            new Workout { Id = 2, Name = "Leg Day", Date = now.AddDays(-12), DurationMinutes = 60, Type = WorkoutType.Strength, CaloriesBurned = 420, Notes = "Heavy squats", CreatedAt = now.AddDays(-12) },
            new Workout { Id = 3, Name = "Cardio Session", Date = now.AddDays(-10), DurationMinutes = 30, Type = WorkoutType.Cardio, CaloriesBurned = 300, Notes = null, CreatedAt = now.AddDays(-10) },
            new Workout { Id = 4, Name = "HIIT Circuit", Date = now.AddDays(-8), DurationMinutes = 25, Type = WorkoutType.HIIT, CaloriesBurned = 280, Notes = "Intense session", CreatedAt = now.AddDays(-8) },
            new Workout { Id = 5, Name = "Push Day", Date = now.AddDays(-6), DurationMinutes = 50, Type = WorkoutType.Strength, CaloriesBurned = 320, Notes = null, CreatedAt = now.AddDays(-6) },
            new Workout { Id = 6, Name = "Pull Day", Date = now.AddDays(-4), DurationMinutes = 45, Type = WorkoutType.Strength, CaloriesBurned = 310, Notes = "New PR on deadlift!", CreatedAt = now.AddDays(-4) },
            new Workout { Id = 7, Name = "Full Body Mix", Date = now.AddDays(-2), DurationMinutes = 65, Type = WorkoutType.Mixed, CaloriesBurned = 450, Notes = null, CreatedAt = now.AddDays(-2) },
            new Workout { Id = 8, Name = "Morning Cardio", Date = now.AddDays(-1), DurationMinutes = 35, Type = WorkoutType.Cardio, CaloriesBurned = 320, Notes = "Easy pace", CreatedAt = now.AddDays(-1) }
        };
        modelBuilder.Entity<Workout>().HasData(workouts);

        var exercises = new[]
        {
            // Workout 1: Upper Body Strength
            new Exercise { Id = 1, WorkoutId = 1, Name = "Barbell Bench Press", Sets = 4, Reps = 8, WeightKg = 80m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 1 },
            new Exercise { Id = 2, WorkoutId = 1, Name = "Dumbbell Shoulder Press", Sets = 3, Reps = 10, WeightKg = 22m, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 5 },
            new Exercise { Id = 3, WorkoutId = 1, Name = "Cable Fly", Sets = 3, Reps = 12, WeightKg = 15m, DurationSeconds = null, OrderIndex = 3, ExerciseDefinitionId = 7 },
            new Exercise { Id = 4, WorkoutId = 1, Name = "Dumbbell Bicep Curl", Sets = 3, Reps = 12, WeightKg = 14m, DurationSeconds = null, OrderIndex = 4, ExerciseDefinitionId = 10 },
            // Workout 2: Leg Day
            new Exercise { Id = 5, WorkoutId = 2, Name = "Barbell Squat", Sets = 5, Reps = 5, WeightKg = 100m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 2 },
            new Exercise { Id = 6, WorkoutId = 2, Name = "Leg Press", Sets = 4, Reps = 10, WeightKg = 150m, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 8 },
            new Exercise { Id = 7, WorkoutId = 2, Name = "Plank", Sets = 3, Reps = 1, WeightKg = null, DurationSeconds = 60, OrderIndex = 3, ExerciseDefinitionId = 6 },
            // Workout 3: Cardio Session
            new Exercise { Id = 8, WorkoutId = 3, Name = "Treadmill Run", Sets = 1, Reps = 1, WeightKg = null, DurationSeconds = 1800, OrderIndex = 1, ExerciseDefinitionId = 9 },
            // Workout 4: HIIT Circuit
            new Exercise { Id = 9, WorkoutId = 4, Name = "Barbell Squat", Sets = 4, Reps = 15, WeightKg = 50m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 2 },
            new Exercise { Id = 10, WorkoutId = 4, Name = "Pull-Up", Sets = 4, Reps = 8, WeightKg = null, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 4 },
            new Exercise { Id = 11, WorkoutId = 4, Name = "Plank", Sets = 3, Reps = 1, WeightKg = null, DurationSeconds = 45, OrderIndex = 3, ExerciseDefinitionId = 6 },
            // Workout 5: Push Day
            new Exercise { Id = 12, WorkoutId = 5, Name = "Barbell Bench Press", Sets = 4, Reps = 6, WeightKg = 85m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 1 },
            new Exercise { Id = 13, WorkoutId = 5, Name = "Dumbbell Shoulder Press", Sets = 3, Reps = 10, WeightKg = 24m, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 5 },
            new Exercise { Id = 14, WorkoutId = 5, Name = "Cable Fly", Sets = 3, Reps = 12, WeightKg = 17.5m, DurationSeconds = null, OrderIndex = 3, ExerciseDefinitionId = 7 },
            // Workout 6: Pull Day
            new Exercise { Id = 15, WorkoutId = 6, Name = "Deadlift", Sets = 5, Reps = 3, WeightKg = 140m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 3 },
            new Exercise { Id = 16, WorkoutId = 6, Name = "Pull-Up", Sets = 4, Reps = 10, WeightKg = null, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 4 },
            new Exercise { Id = 17, WorkoutId = 6, Name = "Dumbbell Bicep Curl", Sets = 3, Reps = 12, WeightKg = 16m, DurationSeconds = null, OrderIndex = 3, ExerciseDefinitionId = 10 },
            // Workout 7: Full Body Mix
            new Exercise { Id = 18, WorkoutId = 7, Name = "Barbell Squat", Sets = 3, Reps = 8, WeightKg = 90m, DurationSeconds = null, OrderIndex = 1, ExerciseDefinitionId = 2 },
            new Exercise { Id = 19, WorkoutId = 7, Name = "Barbell Bench Press", Sets = 3, Reps = 8, WeightKg = 75m, DurationSeconds = null, OrderIndex = 2, ExerciseDefinitionId = 1 },
            new Exercise { Id = 20, WorkoutId = 7, Name = "Deadlift", Sets = 3, Reps = 5, WeightKg = 120m, DurationSeconds = null, OrderIndex = 3, ExerciseDefinitionId = 3 },
            new Exercise { Id = 21, WorkoutId = 7, Name = "Plank", Sets = 3, Reps = 1, WeightKg = null, DurationSeconds = 60, OrderIndex = 4, ExerciseDefinitionId = 6 },
            // Workout 8: Morning Cardio
            new Exercise { Id = 22, WorkoutId = 8, Name = "Treadmill Run", Sets = 1, Reps = 1, WeightKg = null, DurationSeconds = 2100, OrderIndex = 1, ExerciseDefinitionId = 9 }
        };
        modelBuilder.Entity<Exercise>().HasData(exercises);

        var personalRecords = new[]
        {
            new PersonalRecord { Id = 1, ExerciseDefinitionId = 1, WeightKg = 85m, Reps = 6, AchievedDate = new DateTime(2026, 2, 12) },   // Bench Press
            new PersonalRecord { Id = 2, ExerciseDefinitionId = 2, WeightKg = 100m, Reps = 5, AchievedDate = new DateTime(2026, 2, 6) },   // Squat
            new PersonalRecord { Id = 3, ExerciseDefinitionId = 3, WeightKg = 140m, Reps = 3, AchievedDate = new DateTime(2026, 2, 14) },  // Deadlift
            new PersonalRecord { Id = 4, ExerciseDefinitionId = 5, WeightKg = 24m, Reps = 10, AchievedDate = new DateTime(2026, 2, 12) },  // Shoulder Press
            new PersonalRecord { Id = 5, ExerciseDefinitionId = 4, WeightKg = 0m, Reps = 10, AchievedDate = new DateTime(2026, 2, 14) }    // Pull-Up (bodyweight)
        };
        modelBuilder.Entity<PersonalRecord>().HasData(personalRecords);
    }
}
