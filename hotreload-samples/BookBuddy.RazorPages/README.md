# BookBuddy.RazorPages

A book tracking application built with ASP.NET Core Razor Pages and Entity Framework Core.

## Features

- **Dashboard**: View reading statistics and progress
- **Book Management**: Add, edit, view, and delete books
- **Reading Status**: Track books as To Read, Reading, Completed, On Hold, or Abandoned
- **Reading Sessions**: Record reading sessions with pages and time
- **Reading Goals**: Set and track yearly reading goals
- **Filtering**: Filter books by status and genre

## Technologies

- ASP.NET Core 10.0 Razor Pages
- Entity Framework Core with SQLite
- Bootstrap 5 for UI

## Getting Started

```bash
cd samples/BookBuddy.RazorPages
dotnet run
```

The application will create a SQLite database (`bookbuddy.db`) with seed data on first run.

Navigate to `https://localhost:5001` (or the URL shown in the console) to use the application.

## Project Structure

- **Models**: Book, ReadingSession, ReadingGoal, ReadingStatus enum
- **Data**: AppDbContext with seed data (10 books, 9 reading sessions, 1 goal)
- **Pages**: Razor Pages for UI
  - Index: Dashboard with reading stats
  - Books/Index: Book list with filters
  - Books/Details: Book detail view with reading sessions
  - Books/Create: Add new book form
  - Books/Edit: Edit book form
- **Partials**: Reusable components (_BookCard, _ReadingProgress)

## Hot Reload Testing

This project is designed for testing ASP.NET Core Hot Reload functionality. Try modifying:
- Page layouts and styles
- Model properties
- Controller logic
- Partial views
