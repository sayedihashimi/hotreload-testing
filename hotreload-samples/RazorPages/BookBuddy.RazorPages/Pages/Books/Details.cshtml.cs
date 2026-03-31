using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Book Book { get; set; } = null!;
    public List<ReadingSession> ReadingSessions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        Book = book;
        ReadingSessions = await _context.ReadingSessions
            .Where(rs => rs.BookId == id)
            .OrderByDescending(rs => rs.SessionDate)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
