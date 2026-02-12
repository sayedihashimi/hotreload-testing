using Microsoft.EntityFrameworkCore;
using RecipeVault.Server.Data.Models;

namespace RecipeVault.Server.Data;

public class RecipeDbContext : DbContext
{
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Instruction> Instructions => Set<Instruction>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();
    public DbSet<MealPlan> MealPlans => Set<MealPlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RecipeTag>()
            .HasKey(rt => new { rt.RecipeId, rt.TagId });

        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(rt => rt.RecipeId);

        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagId);

        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Instruction>()
            .HasOne(i => i.Recipe)
            .WithMany(r => r.Instructions)
            .HasForeignKey(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var tags = new[]
        {
            new Tag { Id = 1, Name = "Quick" },
            new Tag { Id = 2, Name = "Vegetarian" },
            new Tag { Id = 3, Name = "Dessert" },
            new Tag { Id = 4, Name = "Healthy" },
            new Tag { Id = 5, Name = "Comfort Food" },
            new Tag { Id = 6, Name = "Breakfast" }
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        var recipes = new[]
        {
            new Recipe
            {
                Id = 1,
                Name = "Classic Pancakes",
                Description = "Fluffy homemade pancakes perfect for a weekend breakfast",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 15,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://picsum.photos/seed/pancakes/400/300",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Recipe
            {
                Id = 2,
                Name = "Mediterranean Quinoa Bowl",
                Description = "A healthy and colorful quinoa bowl with fresh vegetables and feta",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 20,
                Servings = 2,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://picsum.photos/seed/quinoa/400/300",
                CreatedAt = DateTime.UtcNow.AddDays(-25)
            },
            new Recipe
            {
                Id = 3,
                Name = "Creamy Tomato Pasta",
                Description = "Rich and creamy tomato pasta sauce that's ready in 30 minutes",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 20,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "https://picsum.photos/seed/pasta/400/300",
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Recipe
            {
                Id = 4,
                Name = "Chocolate Lava Cake",
                Description = "Decadent chocolate dessert with a molten center",
                PrepTimeMinutes = 20,
                CookTimeMinutes = 12,
                Servings = 4,
                Difficulty = DifficultyLevel.Medium,
                ImageUrl = "https://picsum.photos/seed/chocolate/400/300",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Recipe
            {
                Id = 5,
                Name = "Beef Wellington",
                Description = "Elegant beef wrapped in puff pastry with mushroom duxelles",
                PrepTimeMinutes = 45,
                CookTimeMinutes = 40,
                Servings = 6,
                Difficulty = DifficultyLevel.Expert,
                ImageUrl = "https://picsum.photos/seed/beef/400/300",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            }
        };
        modelBuilder.Entity<Recipe>().HasData(recipes);

        var ingredients = new[]
        {
            new Ingredient { Id = 1, RecipeId = 1, Name = "All-purpose flour", Quantity = 1.5m, Unit = "cups" },
            new Ingredient { Id = 2, RecipeId = 1, Name = "Sugar", Quantity = 3, Unit = "tbsp" },
            new Ingredient { Id = 3, RecipeId = 1, Name = "Baking powder", Quantity = 1, Unit = "tbsp" },
            new Ingredient { Id = 4, RecipeId = 1, Name = "Salt", Quantity = 0.5m, Unit = "tsp" },
            new Ingredient { Id = 5, RecipeId = 1, Name = "Milk", Quantity = 1.25m, Unit = "cups" },
            new Ingredient { Id = 6, RecipeId = 1, Name = "Egg", Quantity = 1, Unit = "large" },
            new Ingredient { Id = 7, RecipeId = 1, Name = "Butter", Quantity = 3, Unit = "tbsp" },
            
            new Ingredient { Id = 8, RecipeId = 2, Name = "Quinoa", Quantity = 1, Unit = "cup" },
            new Ingredient { Id = 9, RecipeId = 2, Name = "Cherry tomatoes", Quantity = 1, Unit = "cup" },
            new Ingredient { Id = 10, RecipeId = 2, Name = "Cucumber", Quantity = 1, Unit = "medium" },
            new Ingredient { Id = 11, RecipeId = 2, Name = "Red onion", Quantity = 0.5m, Unit = "medium" },
            new Ingredient { Id = 12, RecipeId = 2, Name = "Feta cheese", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 13, RecipeId = 2, Name = "Olive oil", Quantity = 3, Unit = "tbsp" },
            new Ingredient { Id = 14, RecipeId = 2, Name = "Lemon juice", Quantity = 2, Unit = "tbsp" },
            
            new Ingredient { Id = 15, RecipeId = 3, Name = "Pasta", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 16, RecipeId = 3, Name = "Canned tomatoes", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 17, RecipeId = 3, Name = "Heavy cream", Quantity = 0.5m, Unit = "cup" },
            new Ingredient { Id = 18, RecipeId = 3, Name = "Garlic", Quantity = 3, Unit = "cloves" },
            new Ingredient { Id = 19, RecipeId = 3, Name = "Onion", Quantity = 1, Unit = "medium" },
            new Ingredient { Id = 20, RecipeId = 3, Name = "Basil", Quantity = 0.25m, Unit = "cup" },
            
            new Ingredient { Id = 21, RecipeId = 4, Name = "Dark chocolate", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 22, RecipeId = 4, Name = "Butter", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 23, RecipeId = 4, Name = "Eggs", Quantity = 3, Unit = "large" },
            new Ingredient { Id = 24, RecipeId = 4, Name = "Sugar", Quantity = 0.5m, Unit = "cup" },
            new Ingredient { Id = 25, RecipeId = 4, Name = "Flour", Quantity = 0.25m, Unit = "cup" },
            
            new Ingredient { Id = 26, RecipeId = 5, Name = "Beef tenderloin", Quantity = 1.5m, Unit = "kg" },
            new Ingredient { Id = 27, RecipeId = 5, Name = "Mushrooms", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 28, RecipeId = 5, Name = "Puff pastry", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 29, RecipeId = 5, Name = "Prosciutto", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 30, RecipeId = 5, Name = "Egg yolk", Quantity = 1, Unit = "large" },
            new Ingredient { Id = 31, RecipeId = 5, Name = "Dijon mustard", Quantity = 2, Unit = "tbsp" }
        };
        modelBuilder.Entity<Ingredient>().HasData(ingredients);

        var instructions = new[]
        {
            new Instruction { Id = 1, RecipeId = 1, StepNumber = 1, Text = "Mix flour, sugar, baking powder, and salt in a large bowl" },
            new Instruction { Id = 2, RecipeId = 1, StepNumber = 2, Text = "Whisk milk, egg, and melted butter in another bowl" },
            new Instruction { Id = 3, RecipeId = 1, StepNumber = 3, Text = "Pour wet ingredients into dry ingredients and stir until just combined" },
            new Instruction { Id = 4, RecipeId = 1, StepNumber = 4, Text = "Heat a griddle over medium heat and lightly grease" },
            new Instruction { Id = 5, RecipeId = 1, StepNumber = 5, Text = "Pour 1/4 cup batter for each pancake and cook until bubbles form" },
            new Instruction { Id = 6, RecipeId = 1, StepNumber = 6, Text = "Flip and cook until golden brown on both sides" },
            
            new Instruction { Id = 7, RecipeId = 2, StepNumber = 1, Text = "Rinse quinoa and cook according to package directions" },
            new Instruction { Id = 8, RecipeId = 2, StepNumber = 2, Text = "Let quinoa cool while you prepare vegetables" },
            new Instruction { Id = 9, RecipeId = 2, StepNumber = 3, Text = "Chop tomatoes, cucumber, and red onion into small pieces" },
            new Instruction { Id = 10, RecipeId = 2, StepNumber = 4, Text = "Combine quinoa and vegetables in a large bowl" },
            new Instruction { Id = 11, RecipeId = 2, StepNumber = 5, Text = "Drizzle with olive oil and lemon juice, then toss" },
            new Instruction { Id = 12, RecipeId = 2, StepNumber = 6, Text = "Crumble feta cheese on top and serve" },
            
            new Instruction { Id = 13, RecipeId = 3, StepNumber = 1, Text = "Cook pasta according to package directions" },
            new Instruction { Id = 14, RecipeId = 3, StepNumber = 2, Text = "Sauté minced garlic and diced onion in olive oil" },
            new Instruction { Id = 15, RecipeId = 3, StepNumber = 3, Text = "Add canned tomatoes and simmer for 15 minutes" },
            new Instruction { Id = 16, RecipeId = 3, StepNumber = 4, Text = "Stir in heavy cream and fresh basil" },
            new Instruction { Id = 17, RecipeId = 3, StepNumber = 5, Text = "Season with salt and pepper to taste" },
            new Instruction { Id = 18, RecipeId = 3, StepNumber = 6, Text = "Toss pasta with sauce and serve immediately" },
            
            new Instruction { Id = 19, RecipeId = 4, StepNumber = 1, Text = "Preheat oven to 425°F (220°C)" },
            new Instruction { Id = 20, RecipeId = 4, StepNumber = 2, Text = "Melt chocolate and butter together in a double boiler" },
            new Instruction { Id = 21, RecipeId = 4, StepNumber = 3, Text = "Whisk eggs and sugar until light and fluffy" },
            new Instruction { Id = 22, RecipeId = 4, StepNumber = 4, Text = "Fold melted chocolate into egg mixture" },
            new Instruction { Id = 23, RecipeId = 4, StepNumber = 5, Text = "Gently fold in flour until just combined" },
            new Instruction { Id = 24, RecipeId = 4, StepNumber = 6, Text = "Pour into greased ramekins and bake for 12 minutes" },
            new Instruction { Id = 25, RecipeId = 4, StepNumber = 7, Text = "Let stand for 1 minute, then invert onto plates" },
            
            new Instruction { Id = 26, RecipeId = 5, StepNumber = 1, Text = "Season beef tenderloin with salt and pepper" },
            new Instruction { Id = 27, RecipeId = 5, StepNumber = 2, Text = "Sear beef in hot pan until browned on all sides" },
            new Instruction { Id = 28, RecipeId = 5, StepNumber = 3, Text = "Brush beef with Dijon mustard and let cool" },
            new Instruction { Id = 29, RecipeId = 5, StepNumber = 4, Text = "Finely chop mushrooms and cook until dry" },
            new Instruction { Id = 30, RecipeId = 5, StepNumber = 5, Text = "Lay out prosciutto and spread mushroom mixture on top" },
            new Instruction { Id = 31, RecipeId = 5, StepNumber = 6, Text = "Place beef on prosciutto and roll tightly" },
            new Instruction { Id = 32, RecipeId = 5, StepNumber = 7, Text = "Wrap in puff pastry and seal edges" },
            new Instruction { Id = 33, RecipeId = 5, StepNumber = 8, Text = "Brush with egg yolk and bake at 400°F for 40 minutes" },
            new Instruction { Id = 34, RecipeId = 5, StepNumber = 9, Text = "Rest for 10 minutes before slicing" }
        };
        modelBuilder.Entity<Instruction>().HasData(instructions);

        var recipeTags = new[]
        {
            new RecipeTag { RecipeId = 1, TagId = 6 },
            new RecipeTag { RecipeId = 1, TagId = 1 },
            
            new RecipeTag { RecipeId = 2, TagId = 4 },
            new RecipeTag { RecipeId = 2, TagId = 2 },
            new RecipeTag { RecipeId = 2, TagId = 1 },
            
            new RecipeTag { RecipeId = 3, TagId = 1 },
            new RecipeTag { RecipeId = 3, TagId = 2 },
            new RecipeTag { RecipeId = 3, TagId = 5 },
            
            new RecipeTag { RecipeId = 4, TagId = 3 },
            
            new RecipeTag { RecipeId = 5, TagId = 5 }
        };
        modelBuilder.Entity<RecipeTag>().HasData(recipeTags);
    }
}
