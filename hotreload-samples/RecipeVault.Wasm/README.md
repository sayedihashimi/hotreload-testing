# RecipeVault.Wasm - Blazor WebAssembly Recipe Collection App

A standalone Blazor WebAssembly application for managing your personal recipe collection, all stored locally in your browser using localStorage.

## Features

- **Fully Client-Side**: Runs entirely in the browser with no backend required
- **Local Storage**: All recipes are saved to browser localStorage for offline access
- **Recipe Management**: Create, view, and delete recipes
- **Import/Export**: Import recipes from JSON and export your collection
- **Offline Support**: Visual indicator showing online/offline status
- **Responsive Design**: Works on desktop and mobile devices

## Project Structure

```
RecipeVault.Wasm/
├── Models/
│   ├── Recipe.cs           # Recipe entity with name, description, times, servings
│   ├── Ingredient.cs       # Ingredient with name, quantity, unit
│   └── Instruction.cs      # Instruction with step number and description
├── Services/
│   ├── ILocalStorageService.cs     # Browser localStorage abstraction
│   ├── LocalStorageService.cs      # Implementation using JS interop
│   ├── IRecipeRepository.cs        # Recipe data access interface
│   └── RecipeRepository.cs         # In-memory + localStorage persistence
├── Pages/
│   ├── Home.razor                  # Landing page with recent recipes
│   ├── Recipes/Index.razor         # All recipes list
│   ├── Recipes/Detail.razor        # Recipe detail view
│   ├── Recipes/Create.razor        # Create new recipe form
│   └── Import.razor                # Import recipes from JSON
├── Components/
│   ├── RecipeCard.razor            # Recipe card display
│   ├── IngredientList.razor        # Ingredients display
│   ├── InstructionSteps.razor      # Instructions display
│   ├── ExportButton.razor          # Export to JSON functionality
│   └── OfflineIndicator.razor      # Online/offline status indicator
└── wwwroot/
    └── js/localStorage.js          # JavaScript interop functions
```

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later

### Running the Application

```bash
cd samples/RecipeVault.Wasm
dotnet run
```

The application will start on https://localhost:5001 (or http://localhost:5000).

### Building the Application

```bash
dotnet build
```

### Publishing for Deployment

```bash
dotnet publish -c Release
```

The output will be in `bin/Release/net10.0/publish/wwwroot/` and can be hosted on any static web server.

## Sample Data

The application comes with three sample recipes:
- Classic Spaghetti Carbonara
- Chocolate Chip Cookies
- Greek Salad

These are automatically loaded when you first open the app if no recipes exist in localStorage.

## Usage

### Adding a Recipe

1. Click "Add New Recipe" or navigate to `/recipes/create`
2. Fill in the recipe details:
   - Name and description
   - Prep time, cook time, and servings
   - Ingredients (name, quantity, unit)
   - Step-by-step instructions
3. Click "Save Recipe"

### Viewing Recipes

- View all recipes at `/recipes`
- Click on any recipe card to see full details
- Recipe details include all ingredients and instructions

### Deleting a Recipe

1. Open a recipe detail page
2. Click the "Delete Recipe" button
3. The recipe is removed from localStorage

### Importing Recipes

1. Navigate to `/import`
2. Paste JSON data in the format shown
3. Click "Import Recipes"
4. Recipes are added to your collection

### Exporting Recipes

1. Navigate to `/recipes`
2. Click "Export Recipes to JSON"
3. A JSON file is downloaded with all your recipes

## Technical Details

### Browser Storage

The application uses the browser's `localStorage` API to persist recipes. Data is stored as JSON and survives browser restarts. Note that:
- Data is stored per-origin (domain/protocol/port)
- Clearing browser data will remove all recipes
- Storage limits vary by browser (typically 5-10 MB)

### JavaScript Interop

The `LocalStorageService` uses Blazor's JS interop to call native browser APIs:
- `localStorage.getItem()` - Retrieve data
- `localStorage.setItem()` - Save data
- `localStorage.removeItem()` - Delete data

Additional JS functions:
- `downloadFile()` - Trigger file download for export
- `registerOnlineStatusListener()` - Monitor online/offline status

### Service Registration

Services are registered as scoped in `Program.cs`:
```csharp
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
```

## Hot Reload Testing

This project is designed for testing Blazor WebAssembly Hot Reload capabilities:

- **Component Changes**: Modify Razor components and see updates without full reload
- **CSS Changes**: Update styles in `app.css` and see immediate changes
- **Service Changes**: Changes to service implementations (may require rebuild)
- **Model Changes**: Modifications to data models (may require rebuild)

### Testing Hot Reload

1. Start the app with `dotnet watch`
2. Make changes to Razor components or CSS
3. Save the file and observe updates in the browser

## Browser Compatibility

- Chrome/Edge: Full support
- Firefox: Full support
- Safari: Full support
- Mobile browsers: Supported

## Limitations

- No server-side validation
- No authentication/authorization
- Storage limited to single browser/device
- No synchronization between devices

## Future Enhancements

Potential improvements for testing:
- Add recipe search and filtering
- Support for recipe photos (base64 in localStorage)
- Recipe categories and tags
- Recipe sharing via URL
- Print-friendly recipe view
- Unit conversion helpers

## License

This is a sample application for testing purposes.
