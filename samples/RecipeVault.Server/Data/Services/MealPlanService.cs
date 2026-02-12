using Microsoft.EntityFrameworkCore;
using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public class MealPlanService : IMealPlanService
{
    private readonly RecipeDbContext _context;

    public MealPlanService(RecipeDbContext context)
    {
        _context = context;
    }

    public async Task<List<MealPlan>> GetMealPlansForWeekAsync(DateTime startDate)
    {
        var endDate = startDate.AddDays(7);
        return await _context.MealPlans
            .Include(mp => mp.Recipe)
            .Where(mp => mp.PlannedDate >= startDate.Date && mp.PlannedDate < endDate.Date)
            .OrderBy(mp => mp.PlannedDate)
            .ThenBy(mp => mp.MealType)
            .ToListAsync();
    }

    public async Task<MealPlan> AddMealPlanAsync(MealPlan mealPlan)
    {
        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync();
        return mealPlan;
    }

    public async Task<bool> RemoveMealPlanAsync(int id)
    {
        var mealPlan = await _context.MealPlans.FindAsync(id);
        if (mealPlan == null)
            return false;

        _context.MealPlans.Remove(mealPlan);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<MealPlan>> GetMealPlansForDateAsync(DateTime date)
    {
        return await _context.MealPlans
            .Include(mp => mp.Recipe)
            .Where(mp => mp.PlannedDate.Date == date.Date)
            .OrderBy(mp => mp.MealType)
            .ToListAsync();
    }
}
