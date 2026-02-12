# RecipeVault.Auto - Blazor Hybrid Rendering Recipe App

A recipe management application built with Blazor .NET 10 demonstrating different render modes for optimal performance and Hot Reload testing.

## Features

- **Hybrid Rendering**: Pages use different render modes (Static SSR, Interactive Server) for optimal performance
- **Entity Framework Core**: SQLite database with migrations and seed data
- **Recipe Management**: Browse, view details, and create recipes
- **Tag System**: Categorize recipes with colorful tags
- **Responsive Design**: Mobile-friendly UI with custom styling

## Architecture

### Models
- `Recipe` - Main recipe entity with name, description, times, servings, difficulty
- `Ingredient` - Recipe ingredients with amounts and ordering
- `Instruction` - Step-by-step cooking instructions
- `Tag` - Category tags with colors
- `RecipeTag` - Many-to-many relationship between recipes and tags
- `DifficultyLevel` - Enum for recipe difficulty (Easy, Medium, Hard, Expert)

### Pages and Render Modes

1. **Home.razor** (`@rendermode InteractiveServer`)
   - Fast initial load with server-side interactivity
   - Displays recipe statistics
   - Navigation to recipe list and creation

2. **Recipes/Index.razor** (`@rendermode InteractiveServer`)
   - Interactive recipe browsing
   - Grid layout with recipe cards
   - Real-time filtering capabilities

3. **Recipes/Detail.razor** (`@rendermode InteractiveServer`)
   - Full recipe details with ingredients and instructions
   - Tag display
   - Server-side rendering for fast page loads

4. **Recipes/Create.razor** (`@rendermode InteractiveServer`)
   - Form for creating new recipes
   - Server-side validation and submission

### Components

- **RenderModeIndicator** - Shows current render mode (useful for testing Hot Reload)
- **RecipeCard** - Reusable recipe display component with hover effects

## Database

- **Provider**: SQLite
- **Connection String**: `Data Source=recipes.db`
- **Seed Data**: 5 recipes with full ingredients and instructions, 6 category tags

## Getting Started

### Prerequisites
- .NET 10.0 SDK

### Running the Application

```bash
# Navigate to project directory
cd samples/RecipeVault.Auto

# Run the application
dotnet run

# For Hot Reload testing
dotnet watch
```

### Building

```bash
dotnet build
```

## Hot Reload Testing

This application is designed to test Hot Reload functionality across different scenarios:

1. **Component Changes**: Modify RecipeCard or RenderModeIndicator
2. **Page Logic**: Update recipe display logic or add new features
3. **Styling**: Change CSS in any component or page
4. **Data Models**: Add properties to Recipe or other models
5. **Render Mode Switching**: Test different render modes

## Project Structure

```
RecipeVault.Auto/
├── Components/
│   ├── Layout/         # Layout components
│   ├── Pages/          # Page components
│   │   ├── Home.razor
│   │   └── Recipes/
│   │       ├── Index.razor
│   │       ├── Detail.razor
│   │       └── Create.razor
│   └── Shared/         # Shared components
│       ├── RecipeCard.razor
│       └── RenderModeIndicator.razor
├── Data/
│   └── RecipeContext.cs  # EF Core context with seed data
├── Models/             # Domain models
│   ├── Recipe.cs
│   ├── Ingredient.cs
│   ├── Instruction.cs
│   ├── Tag.cs
│   ├── RecipeTag.cs
│   └── DifficultyLevel.cs
├── wwwroot/            # Static assets
├── Program.cs          # Application entry point
└── appsettings.json    # Configuration
```

## Technology Stack

- **Framework**: .NET 10.0
- **UI**: Blazor Server with Interactive render modes
- **Database**: SQLite with Entity Framework Core 10.0
- **Styling**: Custom CSS with responsive design

## Sample Data

The application includes 5 sample recipes:
1. Classic Margherita Pizza (Italian, Vegetarian)
2. Pad Thai (Asian, Quick)
3. Chocolate Chip Cookies (Dessert, Quick)
4. Greek Salad (Vegetarian, Quick, Healthy)
5. Beef Wellington (Expert difficulty)

Each recipe includes complete ingredients, step-by-step instructions, and relevant tags.
