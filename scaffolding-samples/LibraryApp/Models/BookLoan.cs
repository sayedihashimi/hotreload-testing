using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class BookLoan
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int MemberId { get; set; }

    [Required]
    public DateTime BorrowDate { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }
}
