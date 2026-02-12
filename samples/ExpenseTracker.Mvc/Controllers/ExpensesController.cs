using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Mvc.Models;
using ExpenseTracker.Mvc.Services;

namespace ExpenseTracker.Mvc.Controllers;

public class ExpensesController : Controller
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;

    public ExpensesController(IExpenseService expenseService, ICategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return View(expenses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesDropdown();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Expense expense)
    {
        if (ModelState.IsValid)
        {
            await _expenseService.AddExpenseAsync(expense);
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesDropdown();
        return View(expense);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        await PopulateCategoriesDropdown();
        return View(expense);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Expense expense)
    {
        if (id != expense.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _expenseService.UpdateExpenseAsync(expense);
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategoriesDropdown();
        return View(expense);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _expenseService.DeleteExpenseAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesDropdown()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
    }
}
