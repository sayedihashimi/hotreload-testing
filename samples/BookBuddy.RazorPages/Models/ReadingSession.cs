using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Models;

public class ReadingSession
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    [Required]
    public DateTime SessionDate { get; set; }
    
    [Range(1, 1000)]
    public int PagesRead { get; set; }
    
    [Range(1, 600)]
    public int? MinutesRead { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
}
