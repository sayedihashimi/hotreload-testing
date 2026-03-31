namespace FitLog.Api.Models;

public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public WorkoutType Type { get; set; }
    public int CaloriesBurned { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = [];
}

public enum WorkoutType { Strength, Cardio, Flexibility, HIIT, Mixed }
