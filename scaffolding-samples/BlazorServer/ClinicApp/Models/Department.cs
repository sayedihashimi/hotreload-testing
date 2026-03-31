using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models;

public class Department
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(1, 50)]
    public int? Floor { get; set; }

    [StringLength(10)]
    public string? PhoneExtension { get; set; }
}
