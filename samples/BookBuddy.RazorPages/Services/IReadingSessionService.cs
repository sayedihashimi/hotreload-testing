using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Services;

public interface IReadingSessionService
{
    Task<List<ReadingSession>> GetSessionsForBookAsync(int bookId);
    Task<List<ReadingSession>> GetRecentSessionsAsync(int count = 10);
    Task<ReadingSession> LogSessionAsync(ReadingSession session);
    Task<bool> DeleteSessionAsync(int id);
}
