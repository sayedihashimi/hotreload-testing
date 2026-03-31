# ExpenseTracker.Mvc

An ASP.NET Core MVC expense tracking application built for Hot Reload testing.

## Features

- **Expense Management**: Create, read, update, and delete expenses
- **Category Management**: Organize expenses by categories with custom icons and colors
- **Budget Tracking**: Set budgets and monitor spending against allocations
- **Dashboard**: View spending summaries and budget progress
- **Payment Methods**: Track different payment methods (Cash, Credit, Debit, etc.)
- **Recurring Expenses**: Support for recurring expense tracking

## Technology Stack

- ASP.NET Core MVC (.NET 10.0)
- Entity Framework Core
- SQLite Database
- Bootstrap 5 for UI

## Getting Started

### Prerequisites

- .NET 10.0 SDK

### Running the Application

1. Navigate to the project directory:
   ```bash
   cd samples/ExpenseTracker.Mvc
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Database

The application uses SQLite with Entity Framework Core. The database is automatically created and seeded with sample data on first run:
- 6 expense categories with icons and colors
- 20 sample expenses across two months
- Current month budget with category allocations

## Project Structure

- **Controllers**: HomeController, ExpensesController, CategoriesController, BudgetsController
- **Models**: Expense, Category, Budget, CategoryBudget with enums for PaymentMethod and RecurrenceInterval
- **Data**: AppDbContext with seed data
- **Views**: Complete CRUD views for all entities
- **Shared Partials**: _ExpenseRow, _CategoryBadge, _BudgetProgress

## Hot Reload Testing

This application is designed to support ASP.NET Core Hot Reload functionality. You can:
- Modify controller logic
- Update views
- Change model properties
- Adjust styles

Without restarting the application during development.
