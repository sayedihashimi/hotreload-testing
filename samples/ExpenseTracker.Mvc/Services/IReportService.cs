using ExpenseTracker.Mvc.Models;

namespace ExpenseTracker.Mvc.Services;

public interface IReportService
{
    Task<Dictionary<Category, decimal>> GetSpendingByCategoryAsync(int month, int year);
    Task<Dictionary<int, decimal>> GetMonthlyTrendsAsync(int year);
    Task<Dictionary<PaymentMethod, decimal>> GetSpendingByPaymentMethodAsync(int month, int year);
}
