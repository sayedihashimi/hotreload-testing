using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Pages.Books;

public class EditModel : PageModel
{
    private readonly IBookService _bookService;

    public EditModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty]
    public BookEditModel Book { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        Book = new BookEditModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            PageCount = book.PageCount,
            Genre = book.Genre,
            Status = book.Status,
            CurrentPage = book.CurrentPage,
            Rating = book.Rating,
            StartedDate = book.StartedDate,
            FinishedDate = book.FinishedDate,
            CoverImageUrl = book.CoverImageUrl,
            Notes = book.Notes
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var book = new Book
        {
            Id = Book.Id,
            Title = Book.Title,
            Author = Book.Author,
            ISBN = Book.ISBN,
            PageCount = Book.PageCount,
            Genre = Book.Genre,
            Status = Book.Status,
            CurrentPage = Book.CurrentPage,
            Rating = Book.Rating,
            StartedDate = Book.StartedDate,
            FinishedDate = Book.FinishedDate,
            CoverImageUrl = Book.CoverImageUrl ?? string.Empty,
            Notes = Book.Notes ?? string.Empty
        };

        await _bookService.UpdateBookAsync(book);
        return RedirectToPage("Details", new { id = Book.Id });
    }

    public class BookEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        public int PageCount { get; set; }

        [Required]
        public string Genre { get; set; } = string.Empty;

        public ReadingStatus Status { get; set; }

        [Range(0, 10000)]
        public int? CurrentPage { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string? CoverImageUrl { get; set; }

        public string? Notes { get; set; }
    }
}
