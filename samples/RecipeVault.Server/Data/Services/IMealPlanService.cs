using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data.Services;

public interface IMealPlanService
{
    Task<List<MealPlan>> GetMealPlansForWeekAsync(DateTime startDate);
    Task<MealPlan> AddMealPlanAsync(MealPlan mealPlan);
    Task<bool> RemoveMealPlanAsync(int id);
    Task<List<MealPlan>> GetMealPlansForDateAsync(DateTime date);
}
