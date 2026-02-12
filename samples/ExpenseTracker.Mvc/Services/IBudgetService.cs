using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Services;

public interface IBudgetService
{
    Task<Budget?> GetBudgetAsync(int month, int year);
    Task<IEnumerable<Budget>> GetAllBudgetsAsync();
    Task<Budget?> GetBudgetByIdAsync(int id);
    Task AddBudgetAsync(Budget budget);
    Task UpdateBudgetAsync(Budget budget);
    Task DeleteBudgetAsync(int id);
    Task<decimal> GetCategorySpendingAsync(int categoryId, int month, int year);
    Task<decimal> GetTotalSpendingAsync(int month, int year);
}
