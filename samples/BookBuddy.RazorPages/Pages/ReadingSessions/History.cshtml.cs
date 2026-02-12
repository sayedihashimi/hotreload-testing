using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.ReadingSessions;

public class HistoryModel : PageModel
{
    private readonly IReadingSessionService _sessionService;

    public HistoryModel(IReadingSessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public List<ReadingSession>? Sessions { get; set; }

    public async Task OnGetAsync()
    {
        Sessions = await _sessionService.GetRecentSessionsAsync(50);
    }
}
