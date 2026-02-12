using ExpenseTracker.Mvc.Data;
using ExpenseTracker.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Mvc.Services;

public class BudgetService : IBudgetService
{
    private readonly ExpenseTrackerContext _context;

    public BudgetService(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<Budget?> GetBudgetAsync(int month, int year)
    {
        return await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .FirstOrDefaultAsync(b => b.Month == month && b.Year == year);
    }

    public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
    {
        return await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .OrderByDescending(b => b.Year)
            .ThenByDescending(b => b.Month)
            .ToListAsync();
    }

    public async Task<Budget?> GetBudgetByIdAsync(int id)
    {
        return await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddBudgetAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBudgetAsync(Budget budget)
    {
        _context.Budgets.Update(budget);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBudgetAsync(int id)
    {
        var budget = await _context.Budgets.FindAsync(id);
        if (budget != null)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<decimal> GetCategorySpendingAsync(int categoryId, int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await _context.Expenses
            .Where(e => e.CategoryId == categoryId && e.Date >= startDate && e.Date <= endDate)
            .SumAsync(e => e.Amount);
    }

    public async Task<decimal> GetTotalSpendingAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await _context.Expenses
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .SumAsync(e => e.Amount);
    }
}
