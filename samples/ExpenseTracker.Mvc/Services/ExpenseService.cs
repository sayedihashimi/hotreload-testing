using ExpenseTracker.Mvc.Data;
using ExpenseTracker.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Mvc.Services;

public class ExpenseService : IExpenseService
{
    private readonly ExpenseTrackerContext _context;

    public ExpenseService(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<Expense?> GetExpenseByIdAsync(int id)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(int categoryId)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.CategoryId == categoryId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateExpenseAsync(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Expenses
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .SumAsync(e => e.Amount);
    }
}
