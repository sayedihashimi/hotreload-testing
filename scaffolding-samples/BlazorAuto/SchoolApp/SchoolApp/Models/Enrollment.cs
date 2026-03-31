using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Enrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    [Required]
    public DateTime EnrollmentDate { get; set; }

    [StringLength(5)]
    public string? Grade { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Active";
}
