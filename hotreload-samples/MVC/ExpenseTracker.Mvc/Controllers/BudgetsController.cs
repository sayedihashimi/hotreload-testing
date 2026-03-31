using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Mvc.Data;
using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Controllers;

public class BudgetsController : Controller
{
    private readonly AppDbContext _context;

    public BudgetsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var budgets = await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .ToListAsync();
        
        foreach (var budget in budgets)
        {
            var totalSpent = await _context.Expenses
                .Where(e => e.Date >= budget.StartDate && e.Date <= budget.EndDate)
                .SumAsync(e => e.Amount);
            ViewData[$"TotalSpent_{budget.Id}"] = totalSpent;
        }

        return View(budgets);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var budget = await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (budget == null)
        {
            return NotFound();
        }

        var categorySpending = new Dictionary<int, decimal>();
        foreach (var categoryBudget in budget.CategoryBudgets)
        {
            var spent = await _context.Expenses
                .Where(e => e.CategoryId == categoryBudget.CategoryId && 
                           e.Date >= budget.StartDate && 
                           e.Date <= budget.EndDate)
                .SumAsync(e => e.Amount);
            categorySpending[categoryBudget.CategoryId] = spent;
        }

        ViewBag.CategorySpending = categorySpending;

        return View(budget);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,TotalAmount")] Budget budget)
    {
        if (ModelState.IsValid)
        {
            _context.Add(budget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(budget);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var budget = await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (budget == null)
        {
            return NotFound();
        }
        
        ViewBag.Categories = _context.Categories.ToList();
        return View(budget);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,TotalAmount")] Budget budget)
    {
        if (id != budget.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(budget);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(budget.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Categories = _context.Categories.ToList();
        return View(budget);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var budget = await _context.Budgets
            .Include(b => b.CategoryBudgets)
            .ThenInclude(cb => cb.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (budget == null)
        {
            return NotFound();
        }

        return View(budget);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var budget = await _context.Budgets.FindAsync(id);
        if (budget != null)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool BudgetExists(int id)
    {
        return _context.Budgets.Any(e => e.Id == id);
    }
}
