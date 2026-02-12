using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.RazorPages.Services;

public class GoalService : IGoalService
{
    private readonly BookBuddyContext _context;

    public GoalService(BookBuddyContext context)
    {
        _context = context;
    }

    public async Task<ReadingGoal?> GetGoalForYearAsync(int year)
    {
        return await _context.ReadingGoals
            .FirstOrDefaultAsync(g => g.Year == year);
    }

    public async Task<GoalProgress?> GetGoalProgressAsync(int year)
    {
        var goal = await GetGoalForYearAsync(year);
        if (goal == null)
            return null;

        var booksCompleted = await _context.Books
            .CountAsync(b => b.Status == ReadingStatus.Finished && 
                           b.FinishedDate.HasValue && 
                           b.FinishedDate.Value.Year == year);

        var pagesCompleted = await _context.ReadingSessions
            .Where(s => s.Date.Year == year)
            .SumAsync(s => s.PagesRead);

        var booksPercentage = goal.TargetBooks > 0 
            ? (double)booksCompleted / goal.TargetBooks * 100 
            : 0;
        var pagesPercentage = goal.TargetPages > 0 
            ? (double)pagesCompleted / goal.TargetPages * 100 
            : 0;

        return new GoalProgress
        {
            Goal = goal,
            BooksCompleted = booksCompleted,
            PagesCompleted = pagesCompleted,
            BooksPercentage = Math.Min(booksPercentage, 100),
            PagesPercentage = Math.Min(pagesPercentage, 100),
            BooksGoalMet = booksCompleted >= goal.TargetBooks,
            PagesGoalMet = pagesCompleted >= goal.TargetPages
        };
    }

    public async Task<ReadingGoal> CreateOrUpdateGoalAsync(ReadingGoal goal)
    {
        var existingGoal = await GetGoalForYearAsync(goal.Year);
        
        if (existingGoal != null)
        {
            existingGoal.TargetBooks = goal.TargetBooks;
            existingGoal.TargetPages = goal.TargetPages;
            await _context.SaveChangesAsync();
            return existingGoal;
        }
        
        _context.ReadingGoals.Add(goal);
        await _context.SaveChangesAsync();
        return goal;
    }

    public async Task<List<ReadingGoal>> GetAllGoalsAsync()
    {
        return await _context.ReadingGoals
            .OrderByDescending(g => g.Year)
            .ToListAsync();
    }
}
