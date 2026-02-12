using Microsoft.EntityFrameworkCore;
using RecipeVault.Server.Models;

namespace RecipeVault.Server.Data;

public class RecipeContext : DbContext
{
    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Instruction> Instructions => Set<Instruction>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();

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

        // Seed Tags
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Quick", Color = "#28a745" },
            new Tag { Id = 2, Name = "Vegetarian", Color = "#17a2b8" },
            new Tag { Id = 3, Name = "Dessert", Color = "#e83e8c" },
            new Tag { Id = 4, Name = "Healthy", Color = "#20c997" },
            new Tag { Id = 5, Name = "Comfort Food", Color = "#fd7e14" },
            new Tag { Id = 6, Name = "Breakfast", Color = "#ffc107" }
        );

        // Seed Recipes
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = 1,
                Name = "Classic Pancakes",
                Description = "Fluffy homemade pancakes perfect for weekend breakfast",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 15,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "/images/pancakes.jpg",
                IsFeatured = true,
                CreatedDate = DateTime.UtcNow.AddDays(-10)
            },
            new Recipe
            {
                Id = 2,
                Name = "Vegetable Stir Fry",
                Description = "Colorful and healthy vegetable stir fry with a savory sauce",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 10,
                Servings = 3,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "/images/stirfry.jpg",
                IsFeatured = true,
                CreatedDate = DateTime.UtcNow.AddDays(-8)
            },
            new Recipe
            {
                Id = 3,
                Name = "Chocolate Lava Cake",
                Description = "Decadent molten chocolate cake with a gooey center",
                PrepTimeMinutes = 20,
                CookTimeMinutes = 12,
                Servings = 2,
                Difficulty = DifficultyLevel.Medium,
                ImageUrl = "/images/lavacake.jpg",
                IsFeatured = false,
                CreatedDate = DateTime.UtcNow.AddDays(-5)
            },
            new Recipe
            {
                Id = 4,
                Name = "Beef Wellington",
                Description = "Classic British dish with tender beef wrapped in puff pastry",
                PrepTimeMinutes = 45,
                CookTimeMinutes = 40,
                Servings = 4,
                Difficulty = DifficultyLevel.Hard,
                ImageUrl = "/images/wellington.jpg",
                IsFeatured = true,
                CreatedDate = DateTime.UtcNow.AddDays(-3)
            },
            new Recipe
            {
                Id = 5,
                Name = "Mediterranean Quinoa Bowl",
                Description = "Nutritious quinoa bowl with fresh vegetables and herbs",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 20,
                Servings = 2,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "/images/quinoa.jpg",
                IsFeatured = false,
                CreatedDate = DateTime.UtcNow.AddDays(-1)
            }
        );

        // Seed Ingredients for Recipe 1 (Pancakes)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, RecipeId = 1, Name = "All-purpose flour", Amount = "2 cups", OrderIndex = 1 },
            new Ingredient { Id = 2, RecipeId = 1, Name = "Baking powder", Amount = "2 tsp", OrderIndex = 2 },
            new Ingredient { Id = 3, RecipeId = 1, Name = "Sugar", Amount = "2 tbsp", OrderIndex = 3 },
            new Ingredient { Id = 4, RecipeId = 1, Name = "Salt", Amount = "1/2 tsp", OrderIndex = 4 },
            new Ingredient { Id = 5, RecipeId = 1, Name = "Milk", Amount = "1 3/4 cups", OrderIndex = 5 },
            new Ingredient { Id = 6, RecipeId = 1, Name = "Eggs", Amount = "2 large", OrderIndex = 6 },
            new Ingredient { Id = 7, RecipeId = 1, Name = "Butter, melted", Amount = "2 tbsp", OrderIndex = 7 }
        );

        // Seed Instructions for Recipe 1 (Pancakes)
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 1, RecipeId = 1, StepNumber = 1, Description = "In a large bowl, whisk together flour, baking powder, sugar, and salt." },
            new Instruction { Id = 2, RecipeId = 1, StepNumber = 2, Description = "In another bowl, whisk together milk, eggs, and melted butter." },
            new Instruction { Id = 3, RecipeId = 1, StepNumber = 3, Description = "Pour wet ingredients into dry ingredients and stir until just combined." },
            new Instruction { Id = 4, RecipeId = 1, StepNumber = 4, Description = "Heat a griddle or pan over medium heat and lightly grease." },
            new Instruction { Id = 5, RecipeId = 1, StepNumber = 5, Description = "Pour 1/4 cup batter for each pancake and cook until bubbles form on surface." },
            new Instruction { Id = 6, RecipeId = 1, StepNumber = 6, Description = "Flip and cook until golden brown. Serve warm with syrup." }
        );

        // Seed Ingredients for Recipe 2 (Stir Fry)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 8, RecipeId = 2, Name = "Broccoli florets", Amount = "2 cups", OrderIndex = 1 },
            new Ingredient { Id = 9, RecipeId = 2, Name = "Bell peppers", Amount = "2, sliced", OrderIndex = 2 },
            new Ingredient { Id = 10, RecipeId = 2, Name = "Carrots", Amount = "2, julienned", OrderIndex = 3 },
            new Ingredient { Id = 11, RecipeId = 2, Name = "Soy sauce", Amount = "3 tbsp", OrderIndex = 4 },
            new Ingredient { Id = 12, RecipeId = 2, Name = "Garlic, minced", Amount = "3 cloves", OrderIndex = 5 },
            new Ingredient { Id = 13, RecipeId = 2, Name = "Ginger, grated", Amount = "1 tbsp", OrderIndex = 6 },
            new Ingredient { Id = 14, RecipeId = 2, Name = "Sesame oil", Amount = "2 tsp", OrderIndex = 7 }
        );

        // Seed Instructions for Recipe 2 (Stir Fry)
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 7, RecipeId = 2, StepNumber = 1, Description = "Heat oil in a large wok or skillet over high heat." },
            new Instruction { Id = 8, RecipeId = 2, StepNumber = 2, Description = "Add garlic and ginger, stir-fry for 30 seconds until fragrant." },
            new Instruction { Id = 9, RecipeId = 2, StepNumber = 3, Description = "Add broccoli and carrots, stir-fry for 3-4 minutes." },
            new Instruction { Id = 10, RecipeId = 2, StepNumber = 4, Description = "Add bell peppers and cook for 2 more minutes." },
            new Instruction { Id = 11, RecipeId = 2, StepNumber = 5, Description = "Pour in soy sauce and sesame oil, toss to coat." },
            new Instruction { Id = 12, RecipeId = 2, StepNumber = 6, Description = "Serve immediately over rice." }
        );

        // Seed Ingredients for Recipe 3 (Lava Cake)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 15, RecipeId = 3, Name = "Dark chocolate", Amount = "4 oz", OrderIndex = 1 },
            new Ingredient { Id = 16, RecipeId = 3, Name = "Butter", Amount = "1/2 cup", OrderIndex = 2 },
            new Ingredient { Id = 17, RecipeId = 3, Name = "Eggs", Amount = "2 large", OrderIndex = 3 },
            new Ingredient { Id = 18, RecipeId = 3, Name = "Egg yolks", Amount = "2", OrderIndex = 4 },
            new Ingredient { Id = 19, RecipeId = 3, Name = "Sugar", Amount = "1/4 cup", OrderIndex = 5 },
            new Ingredient { Id = 20, RecipeId = 3, Name = "Flour", Amount = "2 tbsp", OrderIndex = 6 }
        );

        // Seed Instructions for Recipe 3 (Lava Cake)
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 13, RecipeId = 3, StepNumber = 1, Description = "Preheat oven to 425°F. Butter and flour two ramekins." },
            new Instruction { Id = 14, RecipeId = 3, StepNumber = 2, Description = "Melt chocolate and butter together in microwave, stirring until smooth." },
            new Instruction { Id = 15, RecipeId = 3, StepNumber = 3, Description = "Whisk eggs, egg yolks, and sugar until thick and pale." },
            new Instruction { Id = 16, RecipeId = 3, StepNumber = 4, Description = "Fold in chocolate mixture and flour until just combined." },
            new Instruction { Id = 17, RecipeId = 3, StepNumber = 5, Description = "Divide batter between ramekins and bake for 12-14 minutes." },
            new Instruction { Id = 18, RecipeId = 3, StepNumber = 6, Description = "Let cool for 1 minute, then invert onto plates. Serve immediately." }
        );

        // Seed Ingredients for Recipe 4 (Beef Wellington)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 21, RecipeId = 4, Name = "Beef tenderloin", Amount = "2 lbs", OrderIndex = 1 },
            new Ingredient { Id = 22, RecipeId = 4, Name = "Puff pastry", Amount = "1 sheet", OrderIndex = 2 },
            new Ingredient { Id = 23, RecipeId = 4, Name = "Mushrooms, diced", Amount = "8 oz", OrderIndex = 3 },
            new Ingredient { Id = 24, RecipeId = 4, Name = "Pâté", Amount = "4 oz", OrderIndex = 4 },
            new Ingredient { Id = 25, RecipeId = 4, Name = "Prosciutto", Amount = "6 slices", OrderIndex = 5 },
            new Ingredient { Id = 26, RecipeId = 4, Name = "Egg yolk", Amount = "1, beaten", OrderIndex = 6 }
        );

        // Seed Instructions for Recipe 4 (Beef Wellington)
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 19, RecipeId = 4, StepNumber = 1, Description = "Season beef with salt and pepper, sear all sides in hot pan until browned." },
            new Instruction { Id = 20, RecipeId = 4, StepNumber = 2, Description = "Brush beef with pâté and let cool completely." },
            new Instruction { Id = 21, RecipeId = 4, StepNumber = 3, Description = "Sauté mushrooms until moisture evaporates, cool." },
            new Instruction { Id = 22, RecipeId = 4, StepNumber = 4, Description = "Lay prosciutto on plastic wrap, spread mushrooms, place beef on top and wrap tightly." },
            new Instruction { Id = 23, RecipeId = 4, StepNumber = 5, Description = "Wrap in puff pastry, seal edges, brush with egg yolk." },
            new Instruction { Id = 24, RecipeId = 4, StepNumber = 6, Description = "Bake at 425°F for 40-45 minutes until golden. Rest 10 minutes before slicing." }
        );

        // Seed Ingredients for Recipe 5 (Quinoa Bowl)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 27, RecipeId = 5, Name = "Quinoa", Amount = "1 cup", OrderIndex = 1 },
            new Ingredient { Id = 28, RecipeId = 5, Name = "Cherry tomatoes, halved", Amount = "1 cup", OrderIndex = 2 },
            new Ingredient { Id = 29, RecipeId = 5, Name = "Cucumber, diced", Amount = "1", OrderIndex = 3 },
            new Ingredient { Id = 30, RecipeId = 5, Name = "Feta cheese, crumbled", Amount = "1/2 cup", OrderIndex = 4 },
            new Ingredient { Id = 31, RecipeId = 5, Name = "Kalamata olives", Amount = "1/4 cup", OrderIndex = 5 },
            new Ingredient { Id = 32, RecipeId = 5, Name = "Lemon juice", Amount = "3 tbsp", OrderIndex = 6 },
            new Ingredient { Id = 33, RecipeId = 5, Name = "Olive oil", Amount = "2 tbsp", OrderIndex = 7 }
        );

        // Seed Instructions for Recipe 5 (Quinoa Bowl)
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 25, RecipeId = 5, StepNumber = 1, Description = "Rinse quinoa and cook according to package directions. Let cool." },
            new Instruction { Id = 26, RecipeId = 5, StepNumber = 2, Description = "In a large bowl, combine cooled quinoa with tomatoes and cucumber." },
            new Instruction { Id = 27, RecipeId = 5, StepNumber = 3, Description = "Add feta cheese and olives." },
            new Instruction { Id = 28, RecipeId = 5, StepNumber = 4, Description = "Whisk together lemon juice and olive oil." },
            new Instruction { Id = 29, RecipeId = 5, StepNumber = 5, Description = "Pour dressing over quinoa mixture and toss gently." },
            new Instruction { Id = 30, RecipeId = 5, StepNumber = 6, Description = "Season with salt and pepper. Serve chilled or at room temperature." }
        );

        // Seed Recipe Tags
        modelBuilder.Entity<RecipeTag>().HasData(
            new RecipeTag { RecipeId = 1, TagId = 6 }, // Pancakes - Breakfast
            new RecipeTag { RecipeId = 1, TagId = 1 }, // Pancakes - Quick
            new RecipeTag { RecipeId = 2, TagId = 2 }, // Stir Fry - Vegetarian
            new RecipeTag { RecipeId = 2, TagId = 4 }, // Stir Fry - Healthy
            new RecipeTag { RecipeId = 2, TagId = 1 }, // Stir Fry - Quick
            new RecipeTag { RecipeId = 3, TagId = 3 }, // Lava Cake - Dessert
            new RecipeTag { RecipeId = 4, TagId = 5 }, // Beef Wellington - Comfort Food
            new RecipeTag { RecipeId = 5, TagId = 2 }, // Quinoa Bowl - Vegetarian
            new RecipeTag { RecipeId = 5, TagId = 4 }  // Quinoa Bowl - Healthy
        );
    }
}
