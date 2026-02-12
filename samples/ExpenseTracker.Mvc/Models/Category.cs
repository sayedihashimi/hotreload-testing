namespace ExpenseTracker.Mvc.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public required string Color { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<CategoryBudget> CategoryBudgets { get; set; } = new List<CategoryBudget>();
}
