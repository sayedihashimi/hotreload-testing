using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Genre { get; set; }
    
    [Range(1, 10000)]
    public int? Pages { get; set; }
    
    public ReadingStatus Status { get; set; } = ReadingStatus.ToRead;
    
    public DateTime? StartedDate { get; set; }
    
    public DateTime? CompletedDate { get; set; }
    
    [Range(1, 5)]
    public int? Rating { get; set; }
    
    [StringLength(1000)]
    public string? Notes { get; set; }
    
    public List<ReadingSession> ReadingSessions { get; set; } = new();
}
