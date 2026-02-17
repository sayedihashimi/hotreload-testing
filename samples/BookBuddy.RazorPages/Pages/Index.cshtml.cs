using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public int TotalBooks { get; set; }
    public int BooksRead { get; set; }
    public int BooksReading { get; set; }
    public int TotalPages { get; set; }
    public ReadingGoal? CurrentGoal { get; set; }
    public List<Book> RecentBooks { get; set; } = new();
    public List<Book> CurrentlyReading { get; set; } = new();

    public void PrintInfo()
    {
        Console.WriteLine("############ \n*******IndexModel created");
    }

    public async Task OnGetAsync()
    {
        PrintInfo();
        TotalBooks = await _context.Books.CountAsync();
        BooksRead = await _context.Books.CountAsync(b => b.Status == ReadingStatus.Completed);
        BooksReading = await _context.Books.CountAsync(b => b.Status == ReadingStatus.Reading);
        TotalPages = await _context.Books
            .Where(b => b.Status == ReadingStatus.Completed && b.Pages.HasValue)
            .SumAsync(b => b.Pages!.Value);

        CurrentGoal = await _context.ReadingGoals
            .FirstOrDefaultAsync(g => g.Year == DateTime.Now.Year);

        RecentBooks = await _context.Books
            .Where(b => b.Status == ReadingStatus.Completed)
            .OrderByDescending(b => b.CompletedDate)
            .Take(5)
            .ToListAsync();

        CurrentlyReading = await _context.Books
            .Where(b => b.Status == ReadingStatus.Reading)
            .OrderBy(b => b.StartedDate)
            .ToListAsync();
    }
}
