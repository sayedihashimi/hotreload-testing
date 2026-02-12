using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Data;

public static class DbInitializer
{
    public static void Initialize(ExpenseTrackerContext context)
    {
        context.Database.EnsureCreated();

        if (context.Categories.Any())
        {
            return;
        }

        var categories = new[]
        {
            new Category { Name = "Groceries", Icon = "🛒", Color = "#4CAF50" },
            new Category { Name = "Transportation", Icon = "🚗", Color = "#2196F3" },
            new Category { Name = "Utilities", Icon = "💡", Color = "#FF9800" },
            new Category { Name = "Entertainment", Icon = "🎬", Color = "#E91E63" },
            new Category { Name = "Healthcare", Icon = "🏥", Color = "#F44336" },
            new Category { Name = "Shopping", Icon = "🛍️", Color = "#9C27B0" }
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();

        var expenses = new[]
        {
            new Expense { Description = "Weekly grocery shopping", Amount = 127.50m, Date = DateTime.Now.AddDays(-45), CategoryId = 1, PaymentMethod = PaymentMethod.DebitCard, Vendor = "SuperMart", IsRecurring = false },
            new Expense { Description = "Gas station fill-up", Amount = 55.00m, Date = DateTime.Now.AddDays(-44), CategoryId = 2, PaymentMethod = PaymentMethod.CreditCard, Vendor = "Shell", IsRecurring = false },
            new Expense { Description = "Electricity bill", Amount = 89.99m, Date = DateTime.Now.AddDays(-40), CategoryId = 3, PaymentMethod = PaymentMethod.BankTransfer, Vendor = "Power Co", IsRecurring = true, RecurrenceInterval = RecurrenceInterval.Monthly },
            new Expense { Description = "Movie tickets", Amount = 32.00m, Date = DateTime.Now.AddDays(-38), CategoryId = 4, PaymentMethod = PaymentMethod.CreditCard, Vendor = "Cinema Plus", IsRecurring = false },
            new Expense { Description = "Doctor visit", Amount = 150.00m, Date = DateTime.Now.AddDays(-35), CategoryId = 5, PaymentMethod = PaymentMethod.DebitCard, Vendor = "Medical Center", IsRecurring = false },
            new Expense { Description = "Grocery store", Amount = 98.75m, Date = DateTime.Now.AddDays(-32), CategoryId = 1, PaymentMethod = PaymentMethod.DebitCard, Vendor = "SuperMart", IsRecurring = false },
            new Expense { Description = "Public transport pass", Amount = 75.00m, Date = DateTime.Now.AddDays(-30), CategoryId = 2, PaymentMethod = PaymentMethod.DebitCard, Vendor = "Metro", IsRecurring = true, RecurrenceInterval = RecurrenceInterval.Monthly },
            new Expense { Description = "New shoes", Amount = 89.99m, Date = DateTime.Now.AddDays(-28), CategoryId = 6, PaymentMethod = PaymentMethod.CreditCard, Vendor = "Shoe Store", IsRecurring = false },
            new Expense { Description = "Internet bill", Amount = 59.99m, Date = DateTime.Now.AddDays(-25), CategoryId = 3, PaymentMethod = PaymentMethod.BankTransfer, Vendor = "ISP Provider", IsRecurring = true, RecurrenceInterval = RecurrenceInterval.Monthly },
            new Expense { Description = "Streaming service", Amount = 14.99m, Date = DateTime.Now.AddDays(-20), CategoryId = 4, PaymentMethod = PaymentMethod.CreditCard, Vendor = "StreamFlix", IsRecurring = true, RecurrenceInterval = RecurrenceInterval.Monthly },
            new Expense { Description = "Weekly groceries", Amount = 115.30m, Date = DateTime.Now.AddDays(-18), CategoryId = 1, PaymentMethod = PaymentMethod.DebitCard, Vendor = "SuperMart", IsRecurring = false },
            new Expense { Description = "Pharmacy", Amount = 42.50m, Date = DateTime.Now.AddDays(-15), CategoryId = 5, PaymentMethod = PaymentMethod.Cash, Vendor = "HealthPlus Pharmacy", IsRecurring = false },
            new Expense { Description = "Gas", Amount = 60.00m, Date = DateTime.Now.AddDays(-12), CategoryId = 2, PaymentMethod = PaymentMethod.CreditCard, Vendor = "BP", IsRecurring = false },
            new Expense { Description = "Groceries", Amount = 132.45m, Date = DateTime.Now.AddDays(-8), CategoryId = 1, PaymentMethod = PaymentMethod.DebitCard, Vendor = "SuperMart", IsRecurring = false },
            new Expense { Description = "Concert tickets", Amount = 85.00m, Date = DateTime.Now.AddDays(-5), CategoryId = 4, PaymentMethod = PaymentMethod.CreditCard, Vendor = "TicketHub", IsRecurring = false },
            new Expense { Description = "Clothing purchase", Amount = 125.00m, Date = DateTime.Now.AddDays(-3), CategoryId = 6, PaymentMethod = PaymentMethod.CreditCard, Vendor = "Fashion Store", IsRecurring = false },
            new Expense { Description = "Water bill", Amount = 35.00m, Date = DateTime.Now.AddDays(-2), CategoryId = 3, PaymentMethod = PaymentMethod.BankTransfer, Vendor = "Water Utility", IsRecurring = true, RecurrenceInterval = RecurrenceInterval.Monthly },
            new Expense { Description = "Grocery shopping", Amount = 105.80m, Date = DateTime.Now.AddDays(-1), CategoryId = 1, PaymentMethod = PaymentMethod.DebitCard, Vendor = "SuperMart", IsRecurring = false }
        };
        context.Expenses.AddRange(expenses);
        context.SaveChanges();

        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;
        var budget = new Budget
        {
            Month = currentMonth,
            Year = currentYear,
            TotalBudget = 2000.00m
        };
        context.Budgets.Add(budget);
        context.SaveChanges();

        var categoryBudgets = new[]
        {
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 1, Amount = 500.00m },
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 2, Amount = 300.00m },
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 3, Amount = 250.00m },
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 4, Amount = 200.00m },
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 5, Amount = 400.00m },
            new CategoryBudget { BudgetId = budget.Id, CategoryId = 6, Amount = 350.00m }
        };
        context.CategoryBudgets.AddRange(categoryBudgets);
        context.SaveChanges();
    }
}
