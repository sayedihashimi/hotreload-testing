using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models;

public class TimeEntry
{
    public int Id { get; set; }

    public int WorkItemId { get; set; }

    public int TeamMemberId { get; set; }

    [Required]
    [Column(TypeName = "decimal(8,2)")]
    [Range(0.25, 24)]
    public decimal HoursWorked { get; set; }

    [Required]
    public DateTime EntryDate { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}
