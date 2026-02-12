namespace ExpenseTracker.Mvc.Models;

public class Budget
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public ICollection<CategoryBudget> CategoryBudgets { get; set; } = new List<CategoryBudget>();
}
