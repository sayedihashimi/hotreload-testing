namespace ContactsManager.Console.Models;

public class ContactTag
{
    public int ContactId { get; set; }
    public int TagId { get; set; }
    public Contact Contact { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
