# RecipeVault.Wasm

A Blazor WebAssembly standalone application for managing recipes with browser localStorage persistence.

## Features

- **Client-Side Storage**: All data is stored in browser localStorage using JS interop
- **Recipe Management**: Create, read, update, and delete recipes
- **Search & Filter**: Filter recipes by difficulty, tags, or search text
- **Import/Export**: Import recipes from JSON files and export your collection
- **Offline Support**: Offline indicator shows connection status
- **Responsive Design**: Works on desktop and mobile devices

## Technology Stack

- **Framework**: Blazor WebAssembly (.NET 10)
- **Storage**: Browser localStorage via JavaScript interop
- **UI**: Bootstrap 5 with custom styling

## Project Structure

```
RecipeVault.Wasm/
├── Models/              - Data models (Recipe, Ingredient, Instruction, etc.)
├── Services/            - Business logic and localStorage abstraction
├── Components/          - Reusable Blazor components
├── Pages/               - Application pages
├── Layout/              - Layout components
└── wwwroot/
    ├── css/             - Stylesheets
    └── js/              - JavaScript interop functions
```

## Running the Application

```bash
# Run the application
dotnet run

# Run with hot reload
dotnet watch
```

The application will start at `http://localhost:5098`

## Hot Reload Test Points

This project is designed to test Hot Reload functionality:

- Modify recipe card styling (RecipeCard.razor.css)
- Add new fields to recipe forms (RecipeForm.razor)
- Change component markup (any .razor file)
- Update service logic (RecipeRepository.cs)
- Modify CSS in wwwroot/css/app.css
- Add new components

## Seed Data

The application includes 5 sample recipes:
1. Classic Pancakes (Breakfast, Quick)
2. Vegetable Stir-Fry (Vegetarian, Healthy, Quick)
3. Chocolate Lava Cake (Dessert)
4. Chicken Noodle Soup (Comfort Food, Healthy)
5. Greek Salad (Vegetarian, Healthy, Quick)

## Data Persistence

- Data is stored in browser's localStorage
- Data persists across browser sessions
- Each browser maintains its own data
- Clear browser data to reset the application
