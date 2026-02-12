using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Mvc.Services;

namespace ExpenseTracker.Mvc.Controllers;

public class ReportsController : Controller
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Monthly(int? month, int? year)
    {
        var selectedMonth = month ?? DateTime.Now.Month;
        var selectedYear = year ?? DateTime.Now.Year;

        var categorySpending = await _reportService.GetSpendingByCategoryAsync(selectedMonth, selectedYear);
        var paymentMethodSpending = await _reportService.GetSpendingByPaymentMethodAsync(selectedMonth, selectedYear);

        ViewBag.SelectedMonth = selectedMonth;
        ViewBag.SelectedYear = selectedYear;
        ViewBag.CategorySpending = categorySpending;
        ViewBag.PaymentMethodSpending = paymentMethodSpending;

        return View();
    }

    public async Task<IActionResult> ByCategory(int? month, int? year)
    {
        var selectedMonth = month ?? DateTime.Now.Month;
        var selectedYear = year ?? DateTime.Now.Year;

        var categorySpending = await _reportService.GetSpendingByCategoryAsync(selectedMonth, selectedYear);

        ViewBag.SelectedMonth = selectedMonth;
        ViewBag.SelectedYear = selectedYear;

        return View(categorySpending);
    }

    public async Task<IActionResult> Trends(int? year)
    {
        var selectedYear = year ?? DateTime.Now.Year;

        var monthlyTrends = await _reportService.GetMonthlyTrendsAsync(selectedYear);

        ViewBag.SelectedYear = selectedYear;

        return View(monthlyTrends);
    }
}
