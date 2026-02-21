namespace LinkVault.MinimalApi.Models;

public class Link
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsFavorite { get; set; }
    public int ClickCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastClickedAt { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; } = null!;
    public ICollection<LinkTag> LinkTags { get; set; } = [];
}
