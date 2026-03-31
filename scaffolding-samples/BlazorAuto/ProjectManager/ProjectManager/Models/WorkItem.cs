using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models;

public class WorkItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public int? AssignedToId { get; set; }

    [Required]
    [StringLength(20)]
    public string Priority { get; set; } = "Medium";

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Open";

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
