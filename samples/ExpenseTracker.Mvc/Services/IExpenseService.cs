using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Services;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetAllExpensesAsync();
    Task<Expense?> GetExpenseByIdAsync(int id);
    Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(int categoryId);
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
    Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate);
}
