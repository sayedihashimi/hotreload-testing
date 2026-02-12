using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Pages.Books;

public class CreateModel : PageModel
{
    private readonly IBookService _bookService;

    public CreateModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty]
    public BookInputModel Book { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var book = new Book
        {
            Title = Book.Title,
            Author = Book.Author,
            ISBN = Book.ISBN,
            PageCount = Book.PageCount,
            Genre = Book.Genre,
            Status = Book.Status,
            CoverImageUrl = Book.CoverImageUrl ?? string.Empty,
            Notes = Book.Notes ?? string.Empty
        };

        await _bookService.CreateBookAsync(book);
        return RedirectToPage("Index");
    }

    public class BookInputModel
    {
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

        public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;

        public string? CoverImageUrl { get; set; }

        public string? Notes { get; set; }
    }
}
