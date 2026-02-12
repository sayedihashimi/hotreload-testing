using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<CategoryBudget> CategoryBudgets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Budget>()
            .Property(b => b.TotalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<CategoryBudget>()
            .Property(cb => cb.AllocatedAmount)
            .HasColumnType("decimal(18,2)");

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var categories = new[]
        {
            new Category { Id = 1, Name = "Food & Dining", Icon = "🍔", Color = "#FF6B6B", Description = "Restaurants, groceries, and dining out" },
            new Category { Id = 2, Name = "Transportation", Icon = "🚗", Color = "#4ECDC4", Description = "Gas, public transit, ride-sharing" },
            new Category { Id = 3, Name = "Shopping", Icon = "🛍️", Color = "#95E1D3", Description = "Clothing, electronics, and general purchases" },
            new Category { Id = 4, Name = "Entertainment", Icon = "🎮", Color = "#FFA07A", Description = "Movies, games, hobbies" },
            new Category { Id = 5, Name = "Utilities", Icon = "💡", Color = "#DDA15E", Description = "Electricity, water, internet" },
            new Category { Id = 6, Name = "Healthcare", Icon = "⚕️", Color = "#BC6C25", Description = "Medical expenses and insurance" }
        };

        modelBuilder.Entity<Category>().HasData(categories);

        var now = DateTime.Now;
        var lastMonth = now.AddMonths(-1);

        var expenses = new List<Expense>
        {
            new Expense { Id = 1, Description = "Grocery shopping", Amount = 85.50m, Date = lastMonth.AddDays(-25), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.Weekly, CategoryId = 1 },
            new Expense { Id = 2, Description = "Restaurant dinner", Amount = 65.00m, Date = lastMonth.AddDays(-23), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 1 },
            new Expense { Id = 3, Description = "Gas station", Amount = 45.30m, Date = lastMonth.AddDays(-22), PaymentMethod = PaymentMethod.Debit, Recurrence = RecurrenceInterval.Weekly, CategoryId = 2 },
            new Expense { Id = 4, Description = "Coffee shop", Amount = 12.50m, Date = lastMonth.AddDays(-20), PaymentMethod = PaymentMethod.Cash, Recurrence = RecurrenceInterval.Daily, CategoryId = 1 },
            new Expense { Id = 5, Description = "New shoes", Amount = 120.00m, Date = lastMonth.AddDays(-18), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 3 },
            new Expense { Id = 6, Description = "Movie tickets", Amount = 28.00m, Date = lastMonth.AddDays(-15), PaymentMethod = PaymentMethod.Cash, Recurrence = RecurrenceInterval.None, CategoryId = 4 },
            new Expense { Id = 7, Description = "Electricity bill", Amount = 95.75m, Date = lastMonth.AddDays(-10), PaymentMethod = PaymentMethod.BankTransfer, Recurrence = RecurrenceInterval.Monthly, CategoryId = 5 },
            new Expense { Id = 8, Description = "Doctor visit", Amount = 150.00m, Date = lastMonth.AddDays(-8), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 6 },
            new Expense { Id = 9, Description = "Fast food", Amount = 18.75m, Date = lastMonth.AddDays(-5), PaymentMethod = PaymentMethod.DigitalWallet, Recurrence = RecurrenceInterval.None, CategoryId = 1 },
            new Expense { Id = 10, Description = "Uber ride", Amount = 22.40m, Date = lastMonth.AddDays(-3), PaymentMethod = PaymentMethod.DigitalWallet, Recurrence = RecurrenceInterval.None, CategoryId = 2 },
            new Expense { Id = 11, Description = "Grocery shopping", Amount = 92.30m, Date = now.AddDays(-20), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.Weekly, CategoryId = 1 },
            new Expense { Id = 12, Description = "Gas station", Amount = 48.60m, Date = now.AddDays(-18), PaymentMethod = PaymentMethod.Debit, Recurrence = RecurrenceInterval.Weekly, CategoryId = 2 },
            new Expense { Id = 13, Description = "Online shopping", Amount = 156.90m, Date = now.AddDays(-15), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 3 },
            new Expense { Id = 14, Description = "Streaming service", Amount = 15.99m, Date = now.AddDays(-14), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.Monthly, CategoryId = 4 },
            new Expense { Id = 15, Description = "Internet bill", Amount = 75.00m, Date = now.AddDays(-12), PaymentMethod = PaymentMethod.BankTransfer, Recurrence = RecurrenceInterval.Monthly, CategoryId = 5 },
            new Expense { Id = 16, Description = "Pharmacy", Amount = 35.50m, Date = now.AddDays(-10), PaymentMethod = PaymentMethod.Debit, Recurrence = RecurrenceInterval.None, CategoryId = 6 },
            new Expense { Id = 17, Description = "Restaurant lunch", Amount = 42.00m, Date = now.AddDays(-8), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 1 },
            new Expense { Id = 18, Description = "Video game", Amount = 59.99m, Date = now.AddDays(-5), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.None, CategoryId = 4 },
            new Expense { Id = 19, Description = "Grocery shopping", Amount = 78.25m, Date = now.AddDays(-3), PaymentMethod = PaymentMethod.Credit, Recurrence = RecurrenceInterval.Weekly, CategoryId = 1 },
            new Expense { Id = 20, Description = "Coffee shop", Amount = 8.50m, Date = now.AddDays(-1), PaymentMethod = PaymentMethod.Cash, Recurrence = RecurrenceInterval.Daily, CategoryId = 1 }
        };

        modelBuilder.Entity<Expense>().HasData(expenses);

        var currentMonthBudget = new Budget
        {
            Id = 1,
            Name = $"{now:MMMM yyyy} Budget",
            StartDate = new DateTime(now.Year, now.Month, 1),
            EndDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)),
            TotalAmount = 2000.00m
        };

        modelBuilder.Entity<Budget>().HasData(currentMonthBudget);

        var categoryBudgets = new[]
        {
            new CategoryBudget { Id = 1, BudgetId = 1, CategoryId = 1, AllocatedAmount = 600.00m },
            new CategoryBudget { Id = 2, BudgetId = 1, CategoryId = 2, AllocatedAmount = 300.00m },
            new CategoryBudget { Id = 3, BudgetId = 1, CategoryId = 3, AllocatedAmount = 400.00m },
            new CategoryBudget { Id = 4, BudgetId = 1, CategoryId = 4, AllocatedAmount = 200.00m },
            new CategoryBudget { Id = 5, BudgetId = 1, CategoryId = 5, AllocatedAmount = 300.00m },
            new CategoryBudget { Id = 6, BudgetId = 1, CategoryId = 6, AllocatedAmount = 200.00m }
        };

        modelBuilder.Entity<CategoryBudget>().HasData(categoryBudgets);
    }
}
