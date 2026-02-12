namespace ExpenseTracker.Mvc.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Vendor { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
    public RecurrenceInterval? RecurrenceInterval { get; set; }
}
