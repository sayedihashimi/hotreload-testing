using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class Author
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Biography { get; set; }

    public DateTime? BirthDate { get; set; }
}
