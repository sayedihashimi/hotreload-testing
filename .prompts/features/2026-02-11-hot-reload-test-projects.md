# Hot Reload Test Projects Specification

**Created:** 2026-02-11  
**Purpose:** Define sample .NET 10 projects for testing Hot Reload and `dotnet watch` functionality

---

## Overview

This specification defines a collection of .NET 10 sample applications designed to thoroughly test Hot Reload scenarios across different project types. Each project is a fictitious but realistic application with enough complexity to exercise various Hot Reload capabilities.

### Target Hot Reload Scenarios

All projects should enable testing of:
- Razor/Blazor component changes
- C# code-behind modifications
- CSS/styling updates
- Adding new methods and classes
- Modifying DI services
- Entity Framework Core model changes (where applicable)

### Database Requirements

- Projects using databases MUST use **Entity Framework Core** with **SQLite**
- Not all projects require database connectivity

---

## Project 1: Aspire - "TaskFlow Orchestrator"

**Type:** .NET Aspire  
**Theme:** A distributed task management system

### Architecture

```
TaskFlow.AppHost/          (Aspire orchestrator)
TaskFlow.ServiceDefaults/  (Shared service configuration)
TaskFlow.Api/              (Web API backend)
TaskFlow.Web/              (Blazor frontend)
```

### TaskFlow.Api (Web API)

**Database:** SQLite with EF Core

**Entities:**
```csharp
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int? AssigneeId { get; set; }
    public Assignee? Assignee { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Assignee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<TaskItem> Tasks { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public ICollection<TaskItem> Tasks { get; set; }
}

public enum TaskPriority { Low, Medium, High, Critical }
public enum TaskStatus { Todo, InProgress, Review, Done }
```

**API Endpoints:**
- `GET/POST/PUT/DELETE /api/tasks` - CRUD for tasks
- `GET/POST /api/assignees` - Manage assignees
- `GET/POST /api/categories` - Manage categories
- `GET /api/tasks/stats` - Dashboard statistics

**Services:**
- `ITaskService` - Business logic for task operations
- `INotificationService` - Mock notification service (for DI testing)

### TaskFlow.Web (Blazor Server)

**Pages:**
- `/` - Dashboard with task statistics and recent tasks
- `/tasks` - Task list with filtering/sorting
- `/tasks/{id}` - Task detail/edit view
- `/assignees` - Assignee management

**Components:**
- `TaskCard.razor` - Displays a single task
- `TaskForm.razor` - Create/edit task form
- `PriorityBadge.razor` - Colored priority indicator
- `StatusDropdown.razor` - Status change dropdown

**Hot Reload Test Points:**
- Modify task card styling
- Add new fields to task form
- Change dashboard statistics calculation
- Update API service methods

---

## Project 2: Blazor Server - "RecipeVault Server"

**Type:** Blazor Server  
**Project Name:** `RecipeVault.Server`  
**Theme:** A recipe collection and meal planning app  
**Database:** SQLite with EF Core

### Entities

```csharp
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int Servings { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    public ICollection<Instruction> Instructions { get; set; }
    public ICollection<RecipeTag> RecipeTags { get; set; }
}

public class Ingredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public Recipe Recipe { get; set; }
}

public class Instruction
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int StepNumber { get; set; }
    public string Text { get; set; }
    public Recipe Recipe { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<RecipeTag> RecipeTags { get; set; }
}

public class RecipeTag
{
    public int RecipeId { get; set; }
    public int TagId { get; set; }
    public Recipe Recipe { get; set; }
    public Tag Tag { get; set; }
}

public enum DifficultyLevel { Easy, Medium, Hard, Expert }
```

### Pages

- `/` - Home with featured recipes
- `/recipes` - Browse all recipes with search/filter
- `/recipes/{id}` - Recipe detail view
- `/recipes/new` - Add new recipe
- `/recipes/{id}/edit` - Edit recipe
- `/meal-planner` - Weekly meal planning grid

### Components

- `RecipeCard.razor` - Recipe preview card
- `IngredientList.razor` - Editable ingredient list
- `InstructionSteps.razor` - Step-by-step instructions
- `TagSelector.razor` - Multi-select tag picker
- `TimeBadge.razor` - Prep/cook time display
- `DifficultyStars.razor` - Visual difficulty indicator

### Services

- `IRecipeService` - Recipe CRUD operations
- `IMealPlanService` - Meal planning logic
- `ISearchService` - Recipe search/filtering

### Hot Reload Test Points

- Modify recipe card layout
- Add nutrition info field to recipes
- Change difficulty display from stars to text
- Update search algorithm
- Modify meal planner grid styling

---

## Project 3: Blazor WebAssembly - "RecipeVault Wasm"

**Type:** Blazor WebAssembly (standalone)  
**Project Name:** `RecipeVault.Wasm`  
**Theme:** Same recipe app concept, but client-side only  
**Database:** None (uses browser localStorage for persistence)

### Differences from Server Version

- Uses `localStorage` via JS interop for data persistence
- Includes `ILocalStorageService` abstraction
- Simpler data model (no EF)
- Demonstrates WASM-specific Hot Reload behavior

### Pages

- `/` - Home with saved recipes
- `/recipes` - Recipe list from local storage
- `/recipes/{id}` - Recipe detail
- `/recipes/new` - Add recipe (saves to localStorage)
- `/import` - Import recipes from JSON

### Components

Same as Server version plus:
- `ExportButton.razor` - Export recipes to JSON
- `OfflineIndicator.razor` - Shows offline/online status

### Services

- `ILocalStorageService` - Browser storage abstraction
- `IRecipeRepository` - In-memory + localStorage persistence

---

## Project 4: Blazor Auto (SSR + Interactive) - "RecipeVault Auto"

**Type:** Blazor Web App (Auto render mode)  
**Project Name:** `RecipeVault.Auto`  
**Theme:** Recipe app with hybrid rendering  
**Database:** SQLite with EF Core

### Structure

```
RecipeVault.Auto/           (Server project)
RecipeVault.Auto.Client/    (Client/WASM project)
```

### Render Mode Strategy

- `/` - Static SSR (fast initial load)
- `/recipes` - Interactive Server (real-time updates)
- `/recipes/{id}` - Interactive Auto (starts Server, shifts to WASM)
- `/meal-planner` - Interactive WebAssembly (rich client interaction)

### Additional Components

- `RenderModeIndicator.razor` - Shows current render mode (for testing)
- Components in `.Client` project for WASM rendering

### Hot Reload Test Points

- Test Hot Reload across different render modes
- Modify shared components used in multiple modes
- Change render mode attributes
- Update streaming SSR content

---

## Project 5: Blazor with Razor Class Library - "ComponentCraft"

**Type:** Blazor Server + Razor Class Library  
**Theme:** A UI component showcase/documentation site

### Structure

```
ComponentCraft.Web/         (Blazor Server host)
ComponentCraft.Components/  (Razor Class Library)
```

### ComponentCraft.Components (RCL)

**Shared Components:**
```
/Components
    /Buttons
        - PrimaryButton.razor
        - SecondaryButton.razor
        - IconButton.razor
    /Cards
        - InfoCard.razor
        - StatCard.razor
        - ProfileCard.razor
    /Forms
        - TextInput.razor
        - SelectInput.razor
        - DatePicker.razor
        - FormGroup.razor
    /Layout
        - PageHeader.razor
        - Sidebar.razor
        - Footer.razor
    /Feedback
        - Alert.razor
        - Toast.razor
        - LoadingSpinner.razor
        - ProgressBar.razor
```

**Shared Styles:**
- `wwwroot/css/componentcraft.css` - Component styles
- CSS variables for theming

### ComponentCraft.Web

**Pages:**
- `/` - Component gallery overview
- `/components/{category}` - Category listing
- `/components/{category}/{name}` - Individual component docs
- `/playground` - Interactive component playground
- `/themes` - Theme customization

**Features:**
- Live component preview with code samples
- Property/parameter documentation
- Theme switcher (light/dark/custom)

### Hot Reload Test Points

- Modify RCL component styles
- Add new component parameters
- Change component markup in RCL
- Update theme CSS variables
- Test Hot Reload propagation from RCL to host

---

## Project 6: Razor Pages - "BookBuddy"

**Type:** Razor Pages  
**Project Name:** `BookBuddy.RazorPages`  
**Theme:** A personal book tracking and reading list app  
**Database:** SQLite with EF Core  
**CSS Framework:** Tailwind CSS (via CDN using `@tailwindcss/browser`)

### Entities

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public string Genre { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime? DateAdded { get; set; }
    public ReadingStatus Status { get; set; }
    public int? CurrentPage { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public int? Rating { get; set; }
    public string Notes { get; set; }
    public ICollection<ReadingSession> ReadingSessions { get; set; }
}

public class ReadingSession
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime Date { get; set; }
    public int PagesRead { get; set; }
    public int MinutesSpent { get; set; }
    public Book Book { get; set; }
}

public class ReadingGoal
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int TargetBooks { get; set; }
    public int TargetPages { get; set; }
}

public enum ReadingStatus { WantToRead, Reading, Finished, Abandoned }
```

### Pages

```
/Pages
    /Index.cshtml                 - Dashboard with reading stats
    /Books
        /Index.cshtml             - Book list with filters
        /Details.cshtml           - Book detail view
        /Create.cshtml            - Add new book
        /Edit.cshtml              - Edit book
        /Delete.cshtml            - Delete confirmation
    /ReadingSessions
        /Log.cshtml               - Log a reading session
        /History.cshtml           - Session history
    /Goals
        /Index.cshtml             - Yearly goals
        /Progress.cshtml          - Goal progress tracking
    /Stats
        /Index.cshtml             - Reading statistics
```

### Partial Views

- `_BookCard.cshtml` - Book display card
- `_ReadingProgress.cshtml` - Progress bar partial
- `_RecentActivity.cshtml` - Recent reading sessions
- `_GoalProgress.cshtml` - Goal completion widget

### Services

- `IBookService` - Book CRUD and queries
- `IReadingStatsService` - Statistics calculations
- `IGoalService` - Goal tracking logic

### Hot Reload Test Points

- Modify Razor page layouts
- Update partial view Tailwind classes
- Add new form fields
- Change PageModel logic
- Modify Tailwind utility classes in Razor views
- Update custom CSS in wwwroot

---

## Project 7: MVC - "ExpenseTracker"

**Type:** ASP.NET Core MVC  
**Project Name:** `ExpenseTracker.Mvc`  
**Theme:** Personal expense tracking and budgeting app  
**Database:** SQLite with EF Core

### Entities

```csharp
public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Vendor { get; set; }
    public string Notes { get; set; }
    public bool IsRecurring { get; set; }
    public RecurrenceInterval? RecurrenceInterval { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public decimal? MonthlyBudget { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}

public class Budget
{
    public int Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalBudget { get; set; }
    public ICollection<CategoryBudget> CategoryBudgets { get; set; }
}

public class CategoryBudget
{
    public int Id { get; set; }
    public int BudgetId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public Budget Budget { get; set; }
    public Category Category { get; set; }
}

public enum PaymentMethod { Cash, CreditCard, DebitCard, BankTransfer, Other }
public enum RecurrenceInterval { Daily, Weekly, BiWeekly, Monthly, Yearly }
```

### Controllers

- `HomeController` - Dashboard and overview
- `ExpensesController` - Expense CRUD
- `CategoriesController` - Category management
- `BudgetsController` - Budget setup and tracking
- `ReportsController` - Spending reports

### Views

```
/Views
    /Home
        /Index.cshtml             - Dashboard
    /Expenses
        /Index.cshtml             - Expense list
        /Details.cshtml           - Expense detail
        /Create.cshtml            - Add expense
        /Edit.cshtml              - Edit expense
        /Delete.cshtml            - Delete confirmation
    /Categories
        /Index.cshtml             - Category list
        /Create.cshtml            - Add category
        /Edit.cshtml              - Edit category
    /Budgets
        /Index.cshtml             - Budget overview
        /Create.cshtml            - Create monthly budget
        /Edit.cshtml              - Edit budget
    /Reports
        /Monthly.cshtml           - Monthly spending report
        /ByCategory.cshtml        - Category breakdown
        /Trends.cshtml            - Spending trends
    /Shared
        /_Layout.cshtml           - Main layout
        /_ExpenseRow.cshtml       - Expense table row partial
        /_CategoryBadge.cshtml    - Category display partial
        /_BudgetProgress.cshtml   - Budget progress bar
        /_ValidationScripts.cshtml
```

### Services

- `IExpenseService` - Expense operations
- `IBudgetService` - Budget calculations
- `IReportService` - Report generation
- `ICategoryService` - Category management

### Hot Reload Test Points

- Modify view layouts and partials
- Update controller action logic
- Change model validation rules
- Modify CSS/JavaScript
- Add new view fields
- Update _Layout.cshtml

---

## Project 8: Web API (Minimal APIs) - "LinkVault"

**Type:** ASP.NET Core Web API (Minimal APIs)  
**Project Name:** `LinkVault.MinimalApi`  
**Theme:** A URL bookmarking and link management API  
**Database:** SQLite with EF Core

### Entities

```csharp
public class Link
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsFavorite { get; set; }
    public int ClickCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastClickedAt { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
    public ICollection<LinkTag> LinkTags { get; set; }
}

public class Collection
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Color { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Link> Links { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<LinkTag> LinkTags { get; set; }
}

public class LinkTag
{
    public int LinkId { get; set; }
    public int TagId { get; set; }
    public Link Link { get; set; }
    public Tag Tag { get; set; }
}
```

### API Endpoints (Minimal APIs)

Endpoints organized into extension method groups:

```csharp
// LinkEndpoints.cs
app.MapGet("/api/links", ...)              // List links (with filtering/search)
app.MapGet("/api/links/{id}", ...)          // Get link by ID
app.MapPost("/api/links", ...)              // Create link
app.MapPut("/api/links/{id}", ...)          // Update link
app.MapDelete("/api/links/{id}", ...)       // Delete link
app.MapPost("/api/links/{id}/click", ...)   // Record a click
app.MapGet("/api/links/favorites", ...)     // Get favorite links

// CollectionEndpoints.cs
app.MapGet("/api/collections", ...)             // List collections
app.MapGet("/api/collections/{id}", ...)        // Get collection with links
app.MapPost("/api/collections", ...)            // Create collection
app.MapPut("/api/collections/{id}", ...)        // Update collection
app.MapDelete("/api/collections/{id}", ...)     // Delete collection
app.MapGet("/api/collections/{id}/links", ...)  // Get links in collection

// TagEndpoints.cs
app.MapGet("/api/tags", ...)                // List tags with usage count
app.MapPost("/api/tags", ...)               // Create tag
app.MapDelete("/api/tags/{id}", ...)        // Delete tag

// StatsEndpoints.cs
app.MapGet("/api/stats", ...)               // Overall statistics
app.MapGet("/api/stats/top-clicked", ...)   // Most clicked links
```

### Services

- `ILinkService` - Link CRUD and search operations
- `ICollectionService` - Collection management
- `ITagService` - Tag management
- `IStatsService` - Click tracking and statistics

### DTOs / Request-Response Models

```csharp
public record CreateLinkRequest(string Url, string Title, string? Description, int CollectionId, List<string>? Tags);
public record UpdateLinkRequest(string? Title, string? Description, bool? IsFavorite, int? CollectionId);
public record LinkResponse(int Id, string Url, string Title, string? Description, bool IsFavorite, int ClickCount, DateTime CreatedAt, string CollectionName, List<string> Tags);
public record CollectionResponse(int Id, string Name, string? Description, string Color, bool IsPublic, int LinkCount);
public record StatsResponse(int TotalLinks, int TotalCollections, int TotalClicks, int FavoriteCount);
```

### Middleware / Features

- Request validation using endpoint filters
- JSON serialization configuration (camelCase, etc.)
- OpenAPI/Swagger documentation via `Microsoft.AspNetCore.OpenApi`
- Route grouping with `MapGroup`
- TypedResults for response type metadata

### HTTP File

Include a `LinkVault.MinimalApi.http` file that exercises all endpoint groups: CRUD operations on links, collections, and tags, plus click tracking and stats queries.

### Hot Reload Test Points

- Modify endpoint route paths
- Add new endpoints to existing groups
- Change response DTO shapes
- Update endpoint filter/validation logic
- Modify service business logic
- Change OpenAPI metadata/descriptions
- Add new route groups
- Update middleware pipeline in Program.cs

---

## Project 9: Web API (Controllers) - "FitLog"

**Type:** ASP.NET Core Web API (Controllers)  
**Project Name:** `FitLog.Api`  
**Theme:** A fitness workout logging and tracking API  
**Database:** SQLite with EF Core

### Entities

```csharp
public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }
    public string? Notes { get; set; }
    public WorkoutType Type { get; set; }
    public int CaloriesBurned { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Exercise> Exercises { get; set; }
}

public class Exercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? WeightKg { get; set; }
    public int? DurationSeconds { get; set; }
    public int OrderIndex { get; set; }
    public Workout Workout { get; set; }
    public int ExerciseDefinitionId { get; set; }
    public ExerciseDefinition ExerciseDefinition { get; set; }
}

public class ExerciseDefinition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public MuscleGroup? SecondaryMuscleGroup { get; set; }
    public ExerciseCategory Category { get; set; }
    public ICollection<Exercise> Exercises { get; set; }
}

public class PersonalRecord
{
    public int Id { get; set; }
    public int ExerciseDefinitionId { get; set; }
    public decimal WeightKg { get; set; }
    public int Reps { get; set; }
    public DateTime AchievedDate { get; set; }
    public ExerciseDefinition ExerciseDefinition { get; set; }
}

public enum WorkoutType { Strength, Cardio, Flexibility, HIIT, Mixed }
public enum MuscleGroup { Chest, Back, Shoulders, Arms, Core, Legs, FullBody }
public enum ExerciseCategory { Barbell, Dumbbell, Machine, Bodyweight, Cable, Cardio }
```

### Controllers

- `WorkoutsController` - Workout CRUD and filtering
- `ExercisesController` - Exercise management within workouts
- `ExerciseDefinitionsController` - Exercise library management
- `PersonalRecordsController` - PR tracking and history
- `StatsController` - Workout statistics and summaries

### API Endpoints

```
GET    /api/workouts                     - List workouts (with date range/type filters)
GET    /api/workouts/{id}                - Get workout with exercises
POST   /api/workouts                     - Create workout
PUT    /api/workouts/{id}                - Update workout
DELETE /api/workouts/{id}                - Delete workout

POST   /api/workouts/{workoutId}/exercises       - Add exercise to workout
PUT    /api/workouts/{workoutId}/exercises/{id}   - Update exercise
DELETE /api/workouts/{workoutId}/exercises/{id}   - Remove exercise

GET    /api/exercise-definitions          - List exercise library
GET    /api/exercise-definitions/{id}     - Get exercise details
POST   /api/exercise-definitions          - Add exercise to library
PUT    /api/exercise-definitions/{id}     - Update exercise definition

GET    /api/personal-records              - List all PRs
GET    /api/personal-records/exercise/{exerciseDefinitionId}  - PRs for specific exercise
POST   /api/personal-records              - Record a new PR

GET    /api/stats/weekly-summary          - Current week summary
GET    /api/stats/monthly-summary         - Current month summary
GET    /api/stats/muscle-group-breakdown  - Volume by muscle group
GET    /api/stats/progress/{exerciseDefinitionId}  - Progress over time for an exercise
```

### Services

- `IWorkoutService` - Workout CRUD and business logic
- `IExerciseService` - Exercise management
- `IPersonalRecordService` - PR detection and tracking
- `IStatsService` - Statistics and summary calculations

### DTOs / Request-Response Models

```csharp
public record CreateWorkoutRequest(string Name, DateTime Date, int DurationMinutes, WorkoutType Type, int CaloriesBurned, string? Notes, List<CreateExerciseRequest>? Exercises);
public record CreateExerciseRequest(int ExerciseDefinitionId, int Sets, int Reps, decimal? WeightKg, int? DurationSeconds);
public record WorkoutResponse(int Id, string Name, DateTime Date, int DurationMinutes, WorkoutType Type, int CaloriesBurned, string? Notes, List<ExerciseResponse> Exercises);
public record ExerciseResponse(int Id, string ExerciseName, int Sets, int Reps, decimal? WeightKg, int? DurationSeconds, string PrimaryMuscleGroup);
public record WeeklySummaryResponse(int TotalWorkouts, int TotalMinutes, int TotalCalories, Dictionary<string, int> WorkoutsByType);
public record PersonalRecordResponse(int Id, string ExerciseName, decimal WeightKg, int Reps, DateTime AchievedDate);
```

### Features

- Model validation via data annotations and `[ApiController]`
- Action filters for common cross-cutting concerns
- OpenAPI/Swagger documentation via `Microsoft.AspNetCore.OpenApi`
- Proper HTTP status codes and `ProblemDetails` responses
- Pagination support for list endpoints

### HTTP File

Include a `FitLog.Api.http` file that exercises all controllers: workout CRUD, exercise management, exercise definitions, personal records, and stats endpoints.

### Hot Reload Test Points

- Modify controller action logic
- Add new controller actions/endpoints
- Change DTO/response model shapes
- Update model validation attributes
- Modify action filters
- Change service business logic
- Update route attributes
- Add new controllers
- Modify middleware configuration in Program.cs

---

## Project 10: Console App (Simple) - "TaskTimer"

**Type:** Console Application  
**Project Name:** `TaskTimer.Console`  
**Theme:** A Pomodoro-style task timer  
**Database:** None

### Features

- Start/stop timer with configurable durations
- Multiple timer presets (Pomodoro: 25/5, Long: 50/10)
- Session tracking (in-memory)
- Console UI with progress display
- Sound notification simulation (console beep)

### Classes

```csharp
public class TimerSession
{
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string TaskName { get; set; }
    public bool Completed { get; set; }
}

public class TimerPreset
{
    public string Name { get; set; }
    public TimeSpan WorkDuration { get; set; }
    public TimeSpan BreakDuration { get; set; }
}

public interface ITimerService
{
    Task StartTimer(TimeSpan duration, string taskName);
    void StopTimer();
    IEnumerable<TimerSession> GetSessionHistory();
}

public interface IDisplayService
{
    void ShowProgress(TimeSpan remaining, TimeSpan total);
    void ShowMenu();
    void ShowSessionSummary(IEnumerable<TimerSession> sessions);
}
```

### Menu Structure

```
=== TaskTimer ===
1. Start Pomodoro (25 min)
2. Start Long Session (50 min)
3. Custom Timer
4. View Session History
5. Exit

Select option: _
```

### Hot Reload Test Points

- Modify menu display text
- Change timer duration defaults
- Update progress bar format
- Add new menu options
- Modify service logic

---

## Project 11: Console App (with EF) - "ContactsManager"

**Type:** Console Application  
**Project Name:** `ContactsManager.Console`  
**Theme:** A command-line contact management tool  
**Database:** SQLite with EF Core

### Entities

```csharp
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastContactedAt { get; set; }
    public ICollection<ContactTag> ContactTags { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ContactTag> ContactTags { get; set; }
}

public class ContactTag
{
    public int ContactId { get; set; }
    public int TagId { get; set; }
    public Contact Contact { get; set; }
    public Tag Tag { get; set; }
}
```

### Commands

Interactive menu-driven interface:

```
=== Contacts Manager ===
1. List all contacts
2. Search contacts
3. Add new contact
4. Edit contact
5. Delete contact
6. Manage tags
7. Export to CSV
8. Exit

Select option: _
```

### Services

- `IContactService` - Contact CRUD with search
- `ITagService` - Tag management
- `IExportService` - CSV export functionality
- `IDisplayService` - Console UI formatting

### Hot Reload Test Points

- Modify contact display format
- Add new search criteria
- Change menu structure
- Update validation logic
- Modify export format

---

## Seed Data Requirements

Each project with a database should include seed data for immediate testing:

### TaskFlow (Aspire)
- 3 categories: "Development", "Design", "Marketing"
- 2 assignees: "Alice Smith", "Bob Johnson"
- 5-8 sample tasks across categories and statuses

### LinkVault (Minimal API)
- 3 collections: "Dev Tools", "News", "Learning"
- 10-15 sample links across collections with varying click counts
- 5 tags: "reference", "tutorial", "tool", "article", "video"
- A few links marked as favorites

### FitLog (Controllers API)
- 8-10 exercise definitions across muscle groups and categories
- 5-8 sample workouts over the past 2 weeks with exercises
- 3-5 personal records
- Mix of workout types (Strength, Cardio, HIIT)

### RecipeVault (all Blazor variants with DB)
- 5 recipes with full ingredients and instructions
- 6 tags: "Quick", "Vegetarian", "Dessert", "Healthy", "Comfort Food", "Breakfast"
- Mix of difficulties and prep times

### BookBuddy (Razor Pages)
- 8-10 books in various reading statuses
- Reading sessions for "Reading" and "Finished" books
- Current year reading goal

### ExpenseTracker (MVC)
- 6 expense categories with icons and colors
- 15-20 sample expenses across 2 months
- Current month budget with category allocations

### ContactsManager (Console)
- 10 sample contacts
- 5 tags: "Family", "Work", "Friends", "VIP", "Newsletter"

---

## Project Structure Summary

All sample projects are located under the `samples/` folder at the repository root.

```
samples/
├── TaskFlow/                    (Aspire)
├── RecipeVault.Server/          (Blazor Server)
├── RecipeVault.Wasm/            (Blazor WASM)
├── RecipeVault.Auto/            (Blazor Auto)
├── ComponentCraft/              (Blazor + RCL)
├── BookBuddy.RazorPages/        (Razor Pages)
├── ExpenseTracker.Mvc/          (MVC)
├── LinkVault.MinimalApi/        (Web API - Minimal APIs)
├── FitLog.Api/                  (Web API - Controllers)
├── TaskTimer.Console/           (Console)
└── ContactsManager.Console/     (Console + EF)
```

| Project | Type | Database | Theme |
|---------|------|----------|-------|
| TaskFlow.* | Aspire | SQLite/EF | Task Management |
| RecipeVault.Server | Blazor Server | SQLite/EF | Recipe Collection |
| RecipeVault.Wasm | Blazor WASM | localStorage | Recipe Collection |
| RecipeVault.Auto | Blazor Auto | SQLite/EF | Recipe Collection |
| ComponentCraft.* | Blazor + RCL | None | UI Component Library |
| BookBuddy.RazorPages | Razor Pages | SQLite/EF | Book Tracking |
| ExpenseTracker.Mvc | MVC | SQLite/EF | Expense Tracking |
| LinkVault.MinimalApi | Web API (Minimal APIs) | SQLite/EF | Link Bookmarking |
| FitLog.Api | Web API (Controllers) | SQLite/EF | Fitness Tracking |
| TaskTimer.Console | Console | None | Pomodoro Timer |
| ContactsManager.Console | Console | SQLite/EF | Contact Management |

---

## Acceptance Criteria

- [ ] All projects target .NET 10
- [ ] All projects build and run successfully
- [ ] Database projects use EF Core with SQLite
- [ ] Each project has seed data (where applicable)
- [ ] Hot Reload works for all identified test points
- [ ] `dotnet watch` runs without errors for each project
- [ ] Blazor projects clearly indicate their hosting model in the project name
- [ ] RCL project is referenced by its host project
- [ ] Each project is self-contained in its own solution folder
