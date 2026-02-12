using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.Goals;

public class ProgressModel : PageModel
{
    private readonly IGoalService _goalService;

    public ProgressModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    public GoalProgress? GoalProgress { get; set; }

    public async Task OnGetAsync()
    {
        GoalProgress = await _goalService.GetGoalProgressAsync(DateTime.Now.Year);
    }
}
