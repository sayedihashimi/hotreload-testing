namespace FitLog.Api.Models;

public class PersonalRecord
{
    public int Id { get; set; }
    public int ExerciseDefinitionId { get; set; }
    public decimal WeightKg { get; set; }
    public int Reps { get; set; }
    public DateTime AchievedDate { get; set; }
    public ExerciseDefinition ExerciseDefinition { get; set; } = null!;
}
