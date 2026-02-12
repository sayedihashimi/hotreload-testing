namespace RecipeVault.Server.Models;

public class Ingredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    
    public Recipe Recipe { get; set; } = null!;
}
