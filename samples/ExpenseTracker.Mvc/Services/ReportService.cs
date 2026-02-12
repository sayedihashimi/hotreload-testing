using ExpenseTracker.Mvc.Data;
using ExpenseTracker.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Mvc.Services;

public class ReportService : IReportService
{
    private readonly ExpenseTrackerContext _context;

    public ReportService(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<Category, decimal>> GetSpendingByCategoryAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var expenses = await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync();

        return expenses
            .GroupBy(e => e.Category!)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
    }

    public async Task<Dictionary<int, decimal>> GetMonthlyTrendsAsync(int year)
    {
        var expenses = await _context.Expenses
            .Where(e => e.Date.Year == year)
            .ToListAsync();

        var trends = new Dictionary<int, decimal>();
        for (int month = 1; month <= 12; month++)
        {
            trends[month] = expenses
                .Where(e => e.Date.Month == month)
                .Sum(e => e.Amount);
        }

        return trends;
    }

    public async Task<Dictionary<PaymentMethod, decimal>> GetSpendingByPaymentMethodAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var expenses = await _context.Expenses
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync();

        return expenses
            .GroupBy(e => e.PaymentMethod)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
    }
}
