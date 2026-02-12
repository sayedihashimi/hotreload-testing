using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Mvc.Models;
using ExpenseTracker.Mvc.Services;

namespace ExpenseTracker.Mvc.Controllers;

public class BudgetsController : Controller
{
    private readonly IBudgetService _budgetService;
    private readonly ICategoryService _categoryService;

    public BudgetsController(IBudgetService budgetService, ICategoryService categoryService)
    {
        _budgetService = budgetService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var budgets = await _budgetService.GetAllBudgetsAsync();
        return View(budgets);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesDropdown();
        var budget = new Budget
        {
            Month = DateTime.Now.Month,
            Year = DateTime.Now.Year
        };
        return View(budget);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Budget budget, Dictionary<int, decimal> categoryAmounts)
    {
        if (ModelState.IsValid)
        {
            foreach (var kvp in categoryAmounts.Where(x => x.Value > 0))
            {
                budget.CategoryBudgets.Add(new CategoryBudget
                {
                    CategoryId = kvp.Key,
                    Amount = kvp.Value
                });
            }
            await _budgetService.AddBudgetAsync(budget);
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesDropdown();
        return View(budget);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var budget = await _budgetService.GetBudgetByIdAsync(id);
        if (budget == null)
        {
            return NotFound();
        }
        await PopulateCategoriesDropdown();
        return View(budget);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Budget budget)
    {
        if (id != budget.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _budgetService.UpdateBudgetAsync(budget);
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesDropdown();
        return View(budget);
    }

    private async Task PopulateCategoriesDropdown()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
    }
}
