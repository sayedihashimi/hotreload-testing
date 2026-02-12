using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Mvc.Models;
using ExpenseTracker.Mvc.Services;

namespace ExpenseTracker.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IExpenseService _expenseService;
    private readonly IBudgetService _budgetService;
    private readonly IReportService _reportService;

    public HomeController(IExpenseService expenseService, IBudgetService budgetService, IReportService reportService)
    {
        _expenseService = expenseService;
        _budgetService = budgetService;
        _reportService = reportService;
    }

    public async Task<IActionResult> Index()
    {
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;

        var startDate = new DateTime(currentYear, currentMonth, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var expenses = await _expenseService.GetExpensesByDateRangeAsync(startDate, endDate);
        var totalSpent = await _budgetService.GetTotalSpendingAsync(currentMonth, currentYear);
        var budget = await _budgetService.GetBudgetAsync(currentMonth, currentYear);
        var categorySpending = await _reportService.GetSpendingByCategoryAsync(currentMonth, currentYear);

        ViewBag.TotalSpent = totalSpent;
        ViewBag.Budget = budget;
        ViewBag.CategorySpending = categorySpending;
        ViewBag.RecentExpenses = expenses.Take(5);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
