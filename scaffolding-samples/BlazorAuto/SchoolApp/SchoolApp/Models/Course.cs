using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Range(1, 12)]
    public int Credits { get; set; }

    public int InstructorId { get; set; }

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Range(1, 500)]
    public int MaxEnrollment { get; set; }
}
