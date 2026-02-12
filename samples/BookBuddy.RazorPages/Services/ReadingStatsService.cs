using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.RazorPages.Services;

public class ReadingStatsService : IReadingStatsService
{
    private readonly BookBuddyContext _context;

    public ReadingStatsService(BookBuddyContext context)
    {
        _context = context;
    }

    public async Task<ReadingStats> GetReadingStatsAsync()
    {
        var books = await _context.Books.Include(b => b.ReadingSessions).ToListAsync();
        var sessions = await _context.ReadingSessions.ToListAsync();
        var currentYear = DateTime.Now.Year;

        return new ReadingStats
        {
            TotalBooks = books.Count,
            BooksRead = books.Count(b => b.Status == ReadingStatus.Finished),
            BooksReading = books.Count(b => b.Status == ReadingStatus.Reading),
            BooksWantToRead = books.Count(b => b.Status == ReadingStatus.WantToRead),
            TotalPagesRead = sessions.Sum(s => s.PagesRead),
            TotalMinutesRead = sessions.Sum(s => s.MinutesSpent),
            AverageRating = books.Where(b => b.Rating.HasValue).Average(b => (double?)b.Rating) ?? 0,
            CurrentYearBooksFinished = books.Count(b => b.Status == ReadingStatus.Finished && 
                                                       b.FinishedDate.HasValue && 
                                                       b.FinishedDate.Value.Year == currentYear),
            CurrentYearPagesRead = sessions.Where(s => s.Date.Year == currentYear).Sum(s => s.PagesRead)
        };
    }

    public async Task<ReadingStats> GetYearStatsAsync(int year)
    {
        var books = await _context.Books.Include(b => b.ReadingSessions).ToListAsync();
        var yearSessions = await _context.ReadingSessions
            .Where(s => s.Date.Year == year)
            .ToListAsync();

        return new ReadingStats
        {
            TotalBooks = books.Count,
            BooksRead = books.Count(b => b.Status == ReadingStatus.Finished && 
                                        b.FinishedDate.HasValue && 
                                        b.FinishedDate.Value.Year == year),
            BooksReading = books.Count(b => b.Status == ReadingStatus.Reading),
            BooksWantToRead = books.Count(b => b.Status == ReadingStatus.WantToRead),
            TotalPagesRead = yearSessions.Sum(s => s.PagesRead),
            TotalMinutesRead = yearSessions.Sum(s => s.MinutesSpent),
            AverageRating = books.Where(b => b.Rating.HasValue && 
                                           b.FinishedDate.HasValue && 
                                           b.FinishedDate.Value.Year == year)
                                 .Average(b => (double?)b.Rating) ?? 0,
            CurrentYearBooksFinished = books.Count(b => b.Status == ReadingStatus.Finished && 
                                                       b.FinishedDate.HasValue && 
                                                       b.FinishedDate.Value.Year == year),
            CurrentYearPagesRead = yearSessions.Sum(s => s.PagesRead)
        };
    }

    public async Task<Dictionary<string, int>> GetGenreDistributionAsync()
    {
        return await _context.Books
            .GroupBy(b => b.Genre)
            .Select(g => new { Genre = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Genre, x => x.Count);
    }

    public async Task<List<(DateTime Date, int Pages)>> GetReadingHistoryAsync(int days = 30)
    {
        var startDate = DateTime.Now.AddDays(-days);
        var sessions = await _context.ReadingSessions
            .Where(s => s.Date >= startDate)
            .OrderBy(s => s.Date)
            .ToListAsync();

        return sessions
            .GroupBy(s => s.Date.Date)
            .Select(g => (Date: g.Key, Pages: g.Sum(s => s.PagesRead)))
            .ToList();
    }
}
