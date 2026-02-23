using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Planning";

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 100_000_000)]
    public decimal? Budget { get; set; }
}
