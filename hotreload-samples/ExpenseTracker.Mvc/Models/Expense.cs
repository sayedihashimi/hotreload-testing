namespace ExpenseTracker.Mvc.Models;

public class Expense
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public RecurrenceInterval Recurrence { get; set; }
    public string? Notes { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
