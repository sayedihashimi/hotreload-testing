namespace RecipeVault.Server.Data.Models;

public class MealPlan
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public DateTime PlannedDate { get; set; }
    public string MealType { get; set; } = string.Empty;
    public Recipe Recipe { get; set; } = null!;
}
