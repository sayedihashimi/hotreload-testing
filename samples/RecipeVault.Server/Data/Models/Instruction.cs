namespace RecipeVault.Server.Data.Models;

public class Instruction
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int StepNumber { get; set; }
    public string Text { get; set; } = string.Empty;
    public Recipe Recipe { get; set; } = null!;
}
