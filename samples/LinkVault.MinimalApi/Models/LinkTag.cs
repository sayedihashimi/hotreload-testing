namespace LinkVault.MinimalApi.Models;

public class LinkTag
{
    public int LinkId { get; set; }
    public int TagId { get; set; }
    public Link Link { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
