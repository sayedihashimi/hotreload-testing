using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Pages.ReadingSessions;

public class LogModel : PageModel
{
    private readonly IReadingSessionService _sessionService;
    private readonly IBookService _bookService;

    public LogModel(IReadingSessionService sessionService, IBookService bookService)
    {
        _sessionService = sessionService;
        _bookService = bookService;
    }

    [BindProperty]
    public SessionInputModel Session { get; set; } = new();

    public SelectList BookSelectList { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadBooksAsync();
        Session.Date = DateTime.Today;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadBooksAsync();
            return Page();
        }

        var session = new ReadingSession
        {
            BookId = Session.BookId,
            Date = Session.Date,
            PagesRead = Session.PagesRead,
            MinutesSpent = Session.MinutesSpent
        };

        await _sessionService.LogSessionAsync(session);
        return RedirectToPage("History");
    }

    private async Task LoadBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        var activeBooks = books.Where(b => b.Status == ReadingStatus.Reading || b.Status == ReadingStatus.WantToRead);
        BookSelectList = new SelectList(activeBooks, nameof(Book.Id), nameof(Book.Title));
    }

    public class SessionInputModel
    {
        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Pages Read")]
        public int PagesRead { get; set; }

        [Required]
        [Range(1, 1440)]
        [Display(Name = "Minutes Spent")]
        public int MinutesSpent { get; set; }
    }
}
