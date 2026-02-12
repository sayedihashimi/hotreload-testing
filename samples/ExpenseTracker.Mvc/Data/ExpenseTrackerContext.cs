using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Mvc.Data;

public class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Expense> Expenses { get; set; } = null!;
    public DbSet<Models.Category> Categories { get; set; } = null!;
    public DbSet<Models.Budget> Budgets { get; set; } = null!;
    public DbSet<Models.CategoryBudget> CategoryBudgets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Expense>()
            .Property(e => e.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Models.Category>()
            .Property(c => c.MonthlyBudget)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Models.Budget>()
            .Property(b => b.TotalBudget)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Models.CategoryBudget>()
            .Property(cb => cb.Amount)
            .HasColumnType("decimal(18,2)");
    }
}
