namespace FitLog.Api.Models;

public class Exercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? WeightKg { get; set; }
    public int? DurationSeconds { get; set; }
    public int OrderIndex { get; set; }
    public Workout Workout { get; set; } = null!;
    public int ExerciseDefinitionId { get; set; }
    public ExerciseDefinition ExerciseDefinition { get; set; } = null!;
}
