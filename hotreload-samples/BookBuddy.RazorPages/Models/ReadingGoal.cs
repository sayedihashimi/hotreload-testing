using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Models;

public class ReadingGoal
{
    public int Id { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    [Range(1, 1000)]
    public int TargetBooks { get; set; }
    
    [Range(1, 100000)]
    public int? TargetPages { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
}
