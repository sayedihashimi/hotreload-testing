using RecipeVault.Wasm.Models;

namespace RecipeVault.Wasm.Services;

public class RecipeRepository : IRecipeRepository
{
    private const string StorageKey = "recipes";
    private readonly ILocalStorageService _localStorage;
    private List<Recipe> _recipes = new();

    public RecipeRepository(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private async Task LoadFromStorageAsync()
    {
        var stored = await _localStorage.GetItemAsync<List<Recipe>>(StorageKey);
        if (stored != null && stored.Any())
        {
            _recipes = stored;
        }
        else
        {
            _recipes = GetSampleRecipes();
            await SaveToStorageAsync();
        }
    }

    private async Task SaveToStorageAsync()
    {
        await _localStorage.SetItemAsync(StorageKey, _recipes);
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        return _recipes.OrderByDescending(r => r.CreatedAt).ToList();
    }

    public async Task<Recipe?> GetByIdAsync(string id)
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        return _recipes.FirstOrDefault(r => r.Id == id);
    }

    public async Task<Recipe> AddAsync(Recipe recipe)
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        recipe.Id = Guid.NewGuid().ToString();
        recipe.CreatedAt = DateTime.UtcNow;
        _recipes.Add(recipe);
        await SaveToStorageAsync();
        return recipe;
    }

    public async Task<Recipe> UpdateAsync(Recipe recipe)
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        var existing = _recipes.FirstOrDefault(r => r.Id == recipe.Id);
        if (existing != null)
        {
            _recipes.Remove(existing);
            _recipes.Add(recipe);
            await SaveToStorageAsync();
        }
        return recipe;
    }

    public async Task DeleteAsync(string id)
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        if (recipe != null)
        {
            _recipes.Remove(recipe);
            await SaveToStorageAsync();
        }
    }

    public async Task<int> ImportAsync(List<Recipe> recipes)
    {
        if (!_recipes.Any())
        {
            await LoadFromStorageAsync();
        }
        foreach (var recipe in recipes)
        {
            recipe.Id = Guid.NewGuid().ToString();
            recipe.CreatedAt = DateTime.UtcNow;
            _recipes.Add(recipe);
        }
        await SaveToStorageAsync();
        return recipes.Count;
    }

    private List<Recipe> GetSampleRecipes()
    {
        return new List<Recipe>
        {
            new Recipe
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Classic Spaghetti Carbonara",
                Description = "Traditional Italian pasta dish with eggs, cheese, and pancetta",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 20,
                Servings = 4,
                Ingredients = new List<Ingredient>
                {
                    new() { Name = "Spaghetti", Quantity = "400", Unit = "g" },
                    new() { Name = "Pancetta", Quantity = "200", Unit = "g" },
                    new() { Name = "Eggs", Quantity = "4", Unit = "large" },
                    new() { Name = "Parmesan cheese", Quantity = "100", Unit = "g" },
                    new() { Name = "Black pepper", Quantity = "1", Unit = "tsp" }
                },
                Instructions = new List<Instruction>
                {
                    new() { StepNumber = 1, Description = "Bring a large pot of salted water to boil and cook spaghetti according to package directions." },
                    new() { StepNumber = 2, Description = "While pasta cooks, fry pancetta in a large skillet until crispy." },
                    new() { StepNumber = 3, Description = "Beat eggs with grated Parmesan cheese and black pepper." },
                    new() { StepNumber = 4, Description = "Drain pasta, reserving 1 cup of pasta water." },
                    new() { StepNumber = 5, Description = "Add hot pasta to pancetta, remove from heat, and quickly stir in egg mixture." },
                    new() { StepNumber = 6, Description = "Add pasta water as needed to create a creamy sauce. Serve immediately." }
                },
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new Recipe
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Chocolate Chip Cookies",
                Description = "Soft and chewy homemade chocolate chip cookies",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 12,
                Servings = 24,
                Ingredients = new List<Ingredient>
                {
                    new() { Name = "All-purpose flour", Quantity = "2.25", Unit = "cups" },
                    new() { Name = "Butter", Quantity = "1", Unit = "cup" },
                    new() { Name = "Brown sugar", Quantity = "0.75", Unit = "cup" },
                    new() { Name = "Granulated sugar", Quantity = "0.75", Unit = "cup" },
                    new() { Name = "Eggs", Quantity = "2", Unit = "large" },
                    new() { Name = "Vanilla extract", Quantity = "2", Unit = "tsp" },
                    new() { Name = "Baking soda", Quantity = "1", Unit = "tsp" },
                    new() { Name = "Salt", Quantity = "1", Unit = "tsp" },
                    new() { Name = "Chocolate chips", Quantity = "2", Unit = "cups" }
                },
                Instructions = new List<Instruction>
                {
                    new() { StepNumber = 1, Description = "Preheat oven to 375°F (190°C)." },
                    new() { StepNumber = 2, Description = "Cream together butter and both sugars until fluffy." },
                    new() { StepNumber = 3, Description = "Beat in eggs and vanilla extract." },
                    new() { StepNumber = 4, Description = "In a separate bowl, whisk together flour, baking soda, and salt." },
                    new() { StepNumber = 5, Description = "Gradually mix dry ingredients into wet ingredients." },
                    new() { StepNumber = 6, Description = "Fold in chocolate chips." },
                    new() { StepNumber = 7, Description = "Drop rounded tablespoons of dough onto ungreased baking sheets." },
                    new() { StepNumber = 8, Description = "Bake for 10-12 minutes until golden brown. Cool on baking sheet for 2 minutes before transferring to wire rack." }
                },
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Recipe
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Greek Salad",
                Description = "Fresh Mediterranean salad with feta cheese and olives",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 0,
                Servings = 6,
                Ingredients = new List<Ingredient>
                {
                    new() { Name = "Tomatoes", Quantity = "4", Unit = "large" },
                    new() { Name = "Cucumber", Quantity = "1", Unit = "large" },
                    new() { Name = "Red onion", Quantity = "1", Unit = "medium" },
                    new() { Name = "Bell pepper", Quantity = "1", Unit = "large" },
                    new() { Name = "Kalamata olives", Quantity = "1", Unit = "cup" },
                    new() { Name = "Feta cheese", Quantity = "200", Unit = "g" },
                    new() { Name = "Olive oil", Quantity = "0.33", Unit = "cup" },
                    new() { Name = "Red wine vinegar", Quantity = "2", Unit = "tbsp" },
                    new() { Name = "Dried oregano", Quantity = "1", Unit = "tsp" }
                },
                Instructions = new List<Instruction>
                {
                    new() { StepNumber = 1, Description = "Cut tomatoes into wedges and cucumber into half-moons." },
                    new() { StepNumber = 2, Description = "Thinly slice red onion and bell pepper." },
                    new() { StepNumber = 3, Description = "Combine vegetables and olives in a large bowl." },
                    new() { StepNumber = 4, Description = "Whisk together olive oil, vinegar, and oregano." },
                    new() { StepNumber = 5, Description = "Pour dressing over salad and toss gently." },
                    new() { StepNumber = 6, Description = "Top with crumbled feta cheese and serve immediately." }
                },
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };
    }
}
