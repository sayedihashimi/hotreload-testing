namespace RecipeVault.Server.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#6c757d";
    
    public List<RecipeTag> RecipeTags { get; set; } = new();
}
