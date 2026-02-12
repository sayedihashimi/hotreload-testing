using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Services;

public class GoalProgress
{
    public ReadingGoal Goal { get; set; } = null!;
    public int BooksCompleted { get; set; }
    public int PagesCompleted { get; set; }
    public double BooksPercentage { get; set; }
    public double PagesPercentage { get; set; }
    public bool BooksGoalMet { get; set; }
    public bool PagesGoalMet { get; set; }
}

public interface IGoalService
{
    Task<ReadingGoal?> GetGoalForYearAsync(int year);
    Task<GoalProgress?> GetGoalProgressAsync(int year);
    Task<ReadingGoal> CreateOrUpdateGoalAsync(ReadingGoal goal);
    Task<List<ReadingGoal>> GetAllGoalsAsync();
}
