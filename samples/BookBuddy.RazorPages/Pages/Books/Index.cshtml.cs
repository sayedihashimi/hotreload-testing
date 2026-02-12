using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Pages.Books;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Book> Books { get; set; } = new();
    public string? StatusFilter { get; set; }
    public string? GenreFilter { get; set; }
    public List<string> Genres { get; set; } = new();

    public async Task OnGetAsync(string? status, string? genre)
    {
        StatusFilter = status;
        GenreFilter = genre;

        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<ReadingStatus>(status, out var statusEnum))
        {
            query = query.Where(b => b.Status == statusEnum);
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genre == genre);
        }

        Books = await query.OrderBy(b => b.Title).ToListAsync();

        Genres = await _context.Books
            .Where(b => b.Genre != null)
            .Select(b => b.Genre!)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }
}
