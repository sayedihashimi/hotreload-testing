using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(300)]
    public string Title { get; set; } = string.Empty;

    [StringLength(13)]
    public string? Isbn { get; set; }

    public DateTime? PublishedDate { get; set; }

    [Range(1, 10000)]
    public int? PageCount { get; set; }

    [StringLength(2000)]
    public string? Summary { get; set; }

    public int AuthorId { get; set; }
}
