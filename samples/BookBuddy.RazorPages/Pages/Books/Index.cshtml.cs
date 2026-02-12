using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IBookService _bookService;

    public IndexModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public List<Book>? Books { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }

    public async Task OnGetAsync()
    {
        if (!string.IsNullOrEmpty(SearchTerm))
        {
            Books = await _bookService.SearchBooksAsync(SearchTerm);
        }
        else if (!string.IsNullOrEmpty(StatusFilter) && Enum.TryParse<ReadingStatus>(StatusFilter, out var status))
        {
            Books = await _bookService.GetBooksByStatusAsync(status);
        }
        else
        {
            Books = await _bookService.GetAllBooksAsync();
        }
    }
}
