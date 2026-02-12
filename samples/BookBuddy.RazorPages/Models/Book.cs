namespace BookBuddy.RazorPages.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public DateTime? DateAdded { get; set; }
    public ReadingStatus Status { get; set; }
    public int? CurrentPage { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public int? Rating { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ICollection<ReadingSession> ReadingSessions { get; set; } = new List<ReadingSession>();
}
