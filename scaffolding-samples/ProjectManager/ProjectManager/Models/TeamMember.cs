using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models;

public class TeamMember
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
    [StringLength(100)]
    public string Role { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }
}
