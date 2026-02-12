using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Mvc.Models;
using ExpenseTracker.Mvc.Data;

namespace ExpenseTracker.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var currentMonth = DateTime.Now;
        var startOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        var expenses = await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= startOfMonth && e.Date <= endOfMonth)
            .OrderByDescending(e => e.Date)
            .Take(10)
            .ToListAsync();

        var budget = await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .FirstOrDefaultAsync(b => b.StartDate <= currentMonth && b.EndDate >= currentMonth);

        var totalSpent = expenses.Sum(e => e.Amount);
        
        ViewBag.Expenses = expenses;
        ViewBag.Budget = budget;
        ViewBag.TotalSpent = totalSpent;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
