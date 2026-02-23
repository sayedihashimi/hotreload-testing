using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models;

public class Milestone
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsCompleted { get; set; }
}
