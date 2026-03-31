using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Instructor
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

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = string.Empty;
}
