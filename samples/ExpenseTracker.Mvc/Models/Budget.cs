namespace ExpenseTracker.Mvc.Models;

public class Budget
{
    public int Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalBudget { get; set; }
    public ICollection<CategoryBudget> CategoryBudgets { get; set; } = new List<CategoryBudget>();
}
