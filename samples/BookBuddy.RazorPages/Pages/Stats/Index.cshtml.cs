using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.Stats;

public class IndexModel : PageModel
{
    private readonly IReadingStatsService _statsService;

    public IndexModel(IReadingStatsService statsService)
    {
        _statsService = statsService;
    }

    public ReadingStats? Stats { get; set; }
    public Dictionary<string, int>? GenreDistribution { get; set; }

    public async Task OnGetAsync()
    {
        Stats = await _statsService.GetReadingStatsAsync();
        GenreDistribution = await _statsService.GetGenreDistributionAsync();
    }
}
