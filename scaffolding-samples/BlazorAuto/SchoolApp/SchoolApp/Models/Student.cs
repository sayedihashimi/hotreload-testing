using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(200)]
    public string? Email { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    public DateTime EnrollmentDate { get; set; }

    [Required]
    [StringLength(20)]
    public string GradeLevel { get; set; } = string.Empty;
}
