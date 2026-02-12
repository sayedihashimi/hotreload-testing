using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.RazorPages.Services;

public class ReadingSessionService : IReadingSessionService
{
    private readonly BookBuddyContext _context;

    public ReadingSessionService(BookBuddyContext context)
    {
        _context = context;
    }

    public async Task<List<ReadingSession>> GetSessionsForBookAsync(int bookId)
    {
        return await _context.ReadingSessions
            .Include(s => s.Book)
            .Where(s => s.BookId == bookId)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<List<ReadingSession>> GetRecentSessionsAsync(int count = 10)
    {
        return await _context.ReadingSessions
            .Include(s => s.Book)
            .OrderByDescending(s => s.Date)
            .Take(count)
            .ToListAsync();
    }

    public async Task<ReadingSession> LogSessionAsync(ReadingSession session)
    {
        _context.ReadingSessions.Add(session);
        
        var book = await _context.Books.FindAsync(session.BookId);
        if (book != null)
        {
            book.CurrentPage = (book.CurrentPage ?? 0) + session.PagesRead;
            
            if (book.Status == ReadingStatus.WantToRead)
            {
                book.Status = ReadingStatus.Reading;
                book.StartedDate = session.Date;
            }
            
            if (book.CurrentPage >= book.PageCount && book.Status == ReadingStatus.Reading)
            {
                book.Status = ReadingStatus.Finished;
                book.FinishedDate = session.Date;
                book.CurrentPage = book.PageCount;
            }
        }
        
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<bool> DeleteSessionAsync(int id)
    {
        var session = await _context.ReadingSessions.FindAsync(id);
        if (session == null)
            return false;

        _context.ReadingSessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }
}
