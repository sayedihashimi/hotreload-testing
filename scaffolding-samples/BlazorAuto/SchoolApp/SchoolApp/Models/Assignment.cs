using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Assignment
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public int CourseId { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(1, 1000)]
    public int MaxPoints { get; set; }

    [Required]
    [StringLength(50)]
    public string AssignmentType { get; set; } = string.Empty;
}
