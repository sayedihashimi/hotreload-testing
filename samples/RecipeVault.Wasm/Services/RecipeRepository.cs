using RecipeVault.Wasm.Models;

namespace RecipeVault.Wasm.Services;

public class RecipeRepository : IRecipeRepository
{
    private const string StorageKey = "recipes";
    private readonly ILocalStorageService _localStorage;
    private List<Recipe> _recipes = new();
    private bool _loaded = false;

    public RecipeRepository(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private async Task EnsureLoadedAsync()
    {
        if (!_loaded)
        {
            var stored = await _localStorage.GetItemAsync<List<Recipe>>(StorageKey);
            _recipes = stored ?? new List<Recipe>();
            
            if (_recipes.Count == 0)
            {
                await SeedDataAsync();
            }
            
            _loaded = true;
        }
    }

    private async Task SaveAsync()
    {
        await _localStorage.SetItemAsync(StorageKey, _recipes);
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        await EnsureLoadedAsync();
        return _recipes.OrderByDescending(r => r.CreatedAt).ToList();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        await EnsureLoadedAsync();
        return _recipes.FirstOrDefault(r => r.Id == id);
    }

    public async Task<Recipe> AddAsync(Recipe recipe)
    {
        await EnsureLoadedAsync();
        recipe.Id = _recipes.Count > 0 ? _recipes.Max(r => r.Id) + 1 : 1;
        recipe.CreatedAt = DateTime.UtcNow;
        _recipes.Add(recipe);
        await SaveAsync();
        return recipe;
    }

    public async Task<Recipe> UpdateAsync(Recipe recipe)
    {
        await EnsureLoadedAsync();
        var index = _recipes.FindIndex(r => r.Id == recipe.Id);
        if (index != -1)
        {
            _recipes[index] = recipe;
            await SaveAsync();
        }
        return recipe;
    }

    public async Task DeleteAsync(int id)
    {
        await EnsureLoadedAsync();
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        if (recipe != null)
        {
            _recipes.Remove(recipe);
            await SaveAsync();
        }
    }

    public async Task<List<string>> GetAllTagsAsync()
    {
        await EnsureLoadedAsync();
        return _recipes.SelectMany(r => r.Tags).Distinct().OrderBy(t => t).ToList();
    }

    public async Task ImportRecipesAsync(List<Recipe> recipes)
    {
        await EnsureLoadedAsync();
        foreach (var recipe in recipes)
        {
            recipe.Id = _recipes.Count > 0 ? _recipes.Max(r => r.Id) + 1 : 1;
            recipe.CreatedAt = DateTime.UtcNow;
            _recipes.Add(recipe);
        }
        await SaveAsync();
    }

    private async Task SeedDataAsync()
    {
        _recipes = new List<Recipe>
        {
            new Recipe
            {
                Id = 1,
                Name = "Classic Pancakes",
                Description = "Fluffy and delicious breakfast pancakes that the whole family will love.",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 15,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://images.unsplash.com/photo-1528207776546-365bb710ee93?w=400",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "All-purpose flour", Quantity = 1.5m, Unit = "cups" },
                    new Ingredient { Name = "Milk", Quantity = 1.25m, Unit = "cups" },
                    new Ingredient { Name = "Egg", Quantity = 1, Unit = "whole" },
                    new Ingredient { Name = "Baking powder", Quantity = 3.5m, Unit = "tsp" },
                    new Ingredient { Name = "Salt", Quantity = 1, Unit = "tsp" },
                    new Ingredient { Name = "Sugar", Quantity = 1, Unit = "tbsp" },
                    new Ingredient { Name = "Melted butter", Quantity = 3, Unit = "tbsp" }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction { StepNumber = 1, Text = "In a large bowl, sift together flour, baking powder, salt, and sugar." },
                    new Instruction { StepNumber = 2, Text = "Make a well in the center and pour in milk, egg, and melted butter." },
                    new Instruction { StepNumber = 3, Text = "Mix until smooth but don't overmix." },
                    new Instruction { StepNumber = 4, Text = "Heat a lightly oiled griddle over medium-high heat." },
                    new Instruction { StepNumber = 5, Text = "Pour batter onto the griddle, using approximately 1/4 cup for each pancake." },
                    new Instruction { StepNumber = 6, Text = "Cook until bubbles form and edges are dry, then flip and cook until golden brown." }
                },
                Tags = new List<string> { "Breakfast", "Quick" }
            },
            new Recipe
            {
                Id = 2,
                Name = "Vegetable Stir-Fry",
                Description = "A colorful and healthy vegetable stir-fry with Asian-inspired flavors.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 10,
                Servings = 3,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://images.unsplash.com/photo-1512058564366-18510be2db19?w=400",
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Broccoli florets", Quantity = 2, Unit = "cups" },
                    new Ingredient { Name = "Bell peppers", Quantity = 2, Unit = "whole" },
                    new Ingredient { Name = "Carrots", Quantity = 2, Unit = "medium" },
                    new Ingredient { Name = "Soy sauce", Quantity = 3, Unit = "tbsp" },
                    new Ingredient { Name = "Garlic", Quantity = 3, Unit = "cloves" },
                    new Ingredient { Name = "Ginger", Quantity = 1, Unit = "tbsp" },
                    new Ingredient { Name = "Sesame oil", Quantity = 2, Unit = "tbsp" }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction { StepNumber = 1, Text = "Prep all vegetables by cutting them into bite-sized pieces." },
                    new Instruction { StepNumber = 2, Text = "Heat sesame oil in a large wok or skillet over high heat." },
                    new Instruction { StepNumber = 3, Text = "Add garlic and ginger, stir-fry for 30 seconds." },
                    new Instruction { StepNumber = 4, Text = "Add harder vegetables like carrots first, stir-fry for 2 minutes." },
                    new Instruction { StepNumber = 5, Text = "Add remaining vegetables and stir-fry for 3-4 minutes." },
                    new Instruction { StepNumber = 6, Text = "Add soy sauce, toss to combine, and serve immediately." }
                },
                Tags = new List<string> { "Vegetarian", "Healthy", "Quick" }
            },
            new Recipe
            {
                Id = 3,
                Name = "Chocolate Lava Cake",
                Description = "Decadent individual chocolate cakes with a molten chocolate center.",
                PrepTimeMinutes = 20,
                CookTimeMinutes = 12,
                Servings = 4,
                Difficulty = DifficultyLevel.Medium,
                ImageUrl = "https://images.unsplash.com/photo-1606313564200-e75d5e30476c?w=400",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Dark chocolate", Quantity = 4, Unit = "oz" },
                    new Ingredient { Name = "Butter", Quantity = 4, Unit = "tbsp" },
                    new Ingredient { Name = "Eggs", Quantity = 2, Unit = "whole" },
                    new Ingredient { Name = "Egg yolks", Quantity = 2, Unit = "whole" },
                    new Ingredient { Name = "Sugar", Quantity = 0.25m, Unit = "cup" },
                    new Ingredient { Name = "Flour", Quantity = 2, Unit = "tbsp" },
                    new Ingredient { Name = "Vanilla extract", Quantity = 1, Unit = "tsp" }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction { StepNumber = 1, Text = "Preheat oven to 425°F. Butter and flour four 6-oz ramekins." },
                    new Instruction { StepNumber = 2, Text = "Melt chocolate and butter together in a double boiler." },
                    new Instruction { StepNumber = 3, Text = "In a bowl, whisk together eggs, egg yolks, and sugar until thick." },
                    new Instruction { StepNumber = 4, Text = "Whisk in melted chocolate mixture and vanilla." },
                    new Instruction { StepNumber = 5, Text = "Fold in flour until just combined." },
                    new Instruction { StepNumber = 6, Text = "Divide batter among ramekins and bake for 12 minutes." },
                    new Instruction { StepNumber = 7, Text = "Let stand 1 minute, then invert onto plates and serve immediately." }
                },
                Tags = new List<string> { "Dessert" }
            },
            new Recipe
            {
                Id = 4,
                Name = "Chicken Noodle Soup",
                Description = "Classic comfort food - warming chicken soup with vegetables and noodles.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 45,
                Servings = 6,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://images.unsplash.com/photo-1587248720327-d4d13fd8a24f?w=400",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Chicken breast", Quantity = 1, Unit = "lb" },
                    new Ingredient { Name = "Chicken broth", Quantity = 8, Unit = "cups" },
                    new Ingredient { Name = "Carrots", Quantity = 3, Unit = "medium" },
                    new Ingredient { Name = "Celery stalks", Quantity = 3, Unit = "whole" },
                    new Ingredient { Name = "Onion", Quantity = 1, Unit = "medium" },
                    new Ingredient { Name = "Egg noodles", Quantity = 2, Unit = "cups" },
                    new Ingredient { Name = "Bay leaves", Quantity = 2, Unit = "whole" },
                    new Ingredient { Name = "Fresh parsley", Quantity = 0.25m, Unit = "cup" }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction { StepNumber = 1, Text = "In a large pot, bring chicken broth to a boil." },
                    new Instruction { StepNumber = 2, Text = "Add chicken breast, bay leaves, and simmer for 20 minutes." },
                    new Instruction { StepNumber = 3, Text = "Remove chicken, let cool, then shred." },
                    new Instruction { StepNumber = 4, Text = "Add chopped vegetables to the broth and simmer 15 minutes." },
                    new Instruction { StepNumber = 5, Text = "Add noodles and cook according to package directions." },
                    new Instruction { StepNumber = 6, Text = "Return shredded chicken to pot, add parsley, and season to taste." }
                },
                Tags = new List<string> { "Comfort Food", "Healthy" }
            },
            new Recipe
            {
                Id = 5,
                Name = "Greek Salad",
                Description = "Fresh and vibrant Mediterranean salad with feta cheese and olives.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 0,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://images.unsplash.com/photo-1540189549336-e6e99c3679fe?w=400",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Cucumbers", Quantity = 2, Unit = "medium" },
                    new Ingredient { Name = "Tomatoes", Quantity = 4, Unit = "large" },
                    new Ingredient { Name = "Red onion", Quantity = 1, Unit = "small" },
                    new Ingredient { Name = "Feta cheese", Quantity = 1, Unit = "cup" },
                    new Ingredient { Name = "Kalamata olives", Quantity = 0.5m, Unit = "cup" },
                    new Ingredient { Name = "Olive oil", Quantity = 0.25m, Unit = "cup" },
                    new Ingredient { Name = "Lemon juice", Quantity = 2, Unit = "tbsp" },
                    new Ingredient { Name = "Oregano", Quantity = 1, Unit = "tsp" }
                },
                Instructions = new List<Instruction>
                {
                    new Instruction { StepNumber = 1, Text = "Chop cucumbers, tomatoes, and red onion into bite-sized pieces." },
                    new Instruction { StepNumber = 2, Text = "Combine vegetables in a large bowl." },
                    new Instruction { StepNumber = 3, Text = "Add olives and crumbled feta cheese." },
                    new Instruction { StepNumber = 4, Text = "In a small bowl, whisk together olive oil, lemon juice, and oregano." },
                    new Instruction { StepNumber = 5, Text = "Drizzle dressing over salad and toss gently to combine." },
                    new Instruction { StepNumber = 6, Text = "Serve immediately or chill for 30 minutes to let flavors meld." }
                },
                Tags = new List<string> { "Vegetarian", "Healthy", "Quick" }
            }
        };
        
        await SaveAsync();
    }
}
