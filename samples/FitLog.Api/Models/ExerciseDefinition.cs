namespace FitLog.Api.Models;

public class ExerciseDefinition
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public MuscleGroup? SecondaryMuscleGroup { get; set; }
    public ExerciseCategory Category { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = [];
}

public enum MuscleGroup { Chest, Back, Shoulders, Arms, Core, Legs, FullBody }
public enum ExerciseCategory { Barbell, Dumbbell, Machine, Bodyweight, Cable, Cardio }
