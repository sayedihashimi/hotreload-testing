namespace LinkVault.MinimalApi.Models;

public class Collection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Color { get; set; } = "#3B82F6";
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Link> Links { get; set; } = [];
}
