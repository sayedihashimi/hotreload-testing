namespace BookBuddy.RazorPages.Models;

public class ReadingSession
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime Date { get; set; }
    public int PagesRead { get; set; }
    public int MinutesSpent { get; set; }
    public Book Book { get; set; } = null!;
}
