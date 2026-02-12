using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.RazorPages.Pages.Goals;

public class IndexModel : PageModel
{
    private readonly IGoalService _goalService;

    public IndexModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    public GoalProgress? CurrentYearGoal { get; set; }
    public List<ReadingGoal>? PreviousGoals { get; set; }

    [BindProperty]
    public GoalInputModel Goal { get; set; } = new();

    public async Task OnGetAsync()
    {
        var currentYear = DateTime.Now.Year;
        CurrentYearGoal = await _goalService.GetGoalProgressAsync(currentYear);
        
        var allGoals = await _goalService.GetAllGoalsAsync();
        PreviousGoals = allGoals.Where(g => g.Year < currentYear).ToList();

        if (CurrentYearGoal != null)
        {
            Goal = new GoalInputModel
            {
                Year = CurrentYearGoal.Goal.Year,
                TargetBooks = CurrentYearGoal.Goal.TargetBooks,
                TargetPages = CurrentYearGoal.Goal.TargetPages
            };
        }
        else
        {
            Goal.Year = currentYear;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        var goal = new ReadingGoal
        {
            Year = Goal.Year,
            TargetBooks = Goal.TargetBooks,
            TargetPages = Goal.TargetPages
        };

        await _goalService.CreateOrUpdateGoalAsync(goal);
        return RedirectToPage("Progress");
    }

    public class GoalInputModel
    {
        public int Year { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Target Books")]
        public int TargetBooks { get; set; }

        [Required]
        [Range(100, 100000)]
        [Display(Name = "Target Pages")]
        public int TargetPages { get; set; }
    }
}
