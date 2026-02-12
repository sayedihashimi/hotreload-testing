using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly IBookService _bookService;
    private readonly IReadingStatsService _statsService;
    private readonly IGoalService _goalService;
    private readonly IReadingSessionService _sessionService;

    public IndexModel(
        IBookService bookService, 
        IReadingStatsService statsService,
        IGoalService goalService,
        IReadingSessionService sessionService)
    {
        _bookService = bookService;
        _statsService = statsService;
        _goalService = goalService;
        _sessionService = sessionService;
    }

    public ReadingStats? Stats { get; set; }
    public List<Book>? CurrentlyReading { get; set; }
    public GoalProgress? GoalProgress { get; set; }
    public List<ReadingSession>? RecentSessions { get; set; }

    public async Task OnGetAsync()
    {
        Stats = await _statsService.GetReadingStatsAsync();
        CurrentlyReading = await _bookService.GetBooksByStatusAsync(ReadingStatus.Reading);
        GoalProgress = await _goalService.GetGoalProgressAsync(DateTime.Now.Year);
        RecentSessions = await _sessionService.GetRecentSessionsAsync(5);
    }
}
