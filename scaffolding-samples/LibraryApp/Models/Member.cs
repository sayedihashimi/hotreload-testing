using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class Member
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

    public DateTime MembershipDate { get; set; }

    [Required]
    [StringLength(50)]
    public string MembershipType { get; set; } = string.Empty;
}
