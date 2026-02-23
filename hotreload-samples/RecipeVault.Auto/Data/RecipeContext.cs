using Microsoft.EntityFrameworkCore;
using RecipeVault.Auto.Models;

namespace RecipeVault.Auto.Data;

public class RecipeContext : DbContext
{
    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }

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

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var tags = new[]
        {
            new Tag { Id = 1, Name = "Vegetarian", Color = "#10b981" },
            new Tag { Id = 2, Name = "Quick", Color = "#f59e0b" },
            new Tag { Id = 3, Name = "Dessert", Color = "#ec4899" },
            new Tag { Id = 4, Name = "Healthy", Color = "#3b82f6" },
            new Tag { Id = 5, Name = "Italian", Color = "#ef4444" },
            new Tag { Id = 6, Name = "Asian", Color = "#8b5cf6" }
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        var recipes = new[]
        {
            new Recipe
            {
                Id = 1,
                Name = "Classic Margherita Pizza",
                Description = "A traditional Italian pizza with fresh mozzarella, tomatoes, and basil.",
                PrepTimeMinutes = 20,
                CookTimeMinutes = 15,
                Servings = 4,
                Difficulty = DifficultyLevel.Medium,
                ImageUrl = "/images/pizza.jpg"
            },
            new Recipe
            {
                Id = 2,
                Name = "Pad Thai",
                Description = "Authentic Thai stir-fried noodles with shrimp, tofu, and peanuts.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 10,
                Servings = 2,
                Difficulty = DifficultyLevel.Medium,
                ImageUrl = "/images/padthai.jpg"
            },
            new Recipe
            {
                Id = 3,
                Name = "Chocolate Chip Cookies",
                Description = "Soft and chewy cookies loaded with chocolate chips.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 12,
                Servings = 24,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "/images/cookies.jpg"
            },
            new Recipe
            {
                Id = 4,
                Name = "Greek Salad",
                Description = "Fresh Mediterranean salad with feta cheese, olives, and vegetables.",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 0,
                Servings = 4,
                Difficulty = DifficultyLevel.Easy,
                ImageUrl = "/images/salad.jpg"
            },
            new Recipe
            {
                Id = 5,
                Name = "Beef Wellington",
                Description = "Elegant beef tenderloin wrapped in puff pastry with mushroom duxelles.",
                PrepTimeMinutes = 45,
                CookTimeMinutes = 40,
                Servings = 6,
                Difficulty = DifficultyLevel.Expert,
                ImageUrl = "/images/wellington.jpg"
            }
        };
        modelBuilder.Entity<Recipe>().HasData(recipes);

        var ingredients = new[]
        {
            new Ingredient { Id = 1, RecipeId = 1, Name = "Pizza dough", Amount = "1 lb", Order = 1 },
            new Ingredient { Id = 2, RecipeId = 1, Name = "Tomato sauce", Amount = "1 cup", Order = 2 },
            new Ingredient { Id = 3, RecipeId = 1, Name = "Fresh mozzarella", Amount = "8 oz", Order = 3 },
            new Ingredient { Id = 4, RecipeId = 1, Name = "Fresh basil", Amount = "1/4 cup", Order = 4 },
            
            new Ingredient { Id = 5, RecipeId = 2, Name = "Rice noodles", Amount = "8 oz", Order = 1 },
            new Ingredient { Id = 6, RecipeId = 2, Name = "Shrimp", Amount = "1/2 lb", Order = 2 },
            new Ingredient { Id = 7, RecipeId = 2, Name = "Tofu", Amount = "4 oz", Order = 3 },
            new Ingredient { Id = 8, RecipeId = 2, Name = "Peanuts", Amount = "1/4 cup", Order = 4 },
            
            new Ingredient { Id = 9, RecipeId = 3, Name = "All-purpose flour", Amount = "2 1/4 cups", Order = 1 },
            new Ingredient { Id = 10, RecipeId = 3, Name = "Butter", Amount = "1 cup", Order = 2 },
            new Ingredient { Id = 11, RecipeId = 3, Name = "Sugar", Amount = "3/4 cup", Order = 3 },
            new Ingredient { Id = 12, RecipeId = 3, Name = "Chocolate chips", Amount = "2 cups", Order = 4 },
            
            new Ingredient { Id = 13, RecipeId = 4, Name = "Tomatoes", Amount = "4 large", Order = 1 },
            new Ingredient { Id = 14, RecipeId = 4, Name = "Cucumber", Amount = "1 large", Order = 2 },
            new Ingredient { Id = 15, RecipeId = 4, Name = "Feta cheese", Amount = "6 oz", Order = 3 },
            new Ingredient { Id = 16, RecipeId = 4, Name = "Kalamata olives", Amount = "1/2 cup", Order = 4 },
            
            new Ingredient { Id = 17, RecipeId = 5, Name = "Beef tenderloin", Amount = "2 lbs", Order = 1 },
            new Ingredient { Id = 18, RecipeId = 5, Name = "Puff pastry", Amount = "1 lb", Order = 2 },
            new Ingredient { Id = 19, RecipeId = 5, Name = "Mushrooms", Amount = "1 lb", Order = 3 },
            new Ingredient { Id = 20, RecipeId = 5, Name = "Prosciutto", Amount = "8 slices", Order = 4 }
        };
        modelBuilder.Entity<Ingredient>().HasData(ingredients);

        var instructions = new[]
        {
            new Instruction { Id = 1, RecipeId = 1, StepNumber = 1, Description = "Preheat oven to 475°F (245°C)." },
            new Instruction { Id = 2, RecipeId = 1, StepNumber = 2, Description = "Roll out pizza dough on a floured surface." },
            new Instruction { Id = 3, RecipeId = 1, StepNumber = 3, Description = "Spread tomato sauce evenly over dough." },
            new Instruction { Id = 4, RecipeId = 1, StepNumber = 4, Description = "Top with mozzarella and bake for 12-15 minutes." },
            new Instruction { Id = 5, RecipeId = 1, StepNumber = 5, Description = "Garnish with fresh basil before serving." },
            
            new Instruction { Id = 6, RecipeId = 2, StepNumber = 1, Description = "Soak rice noodles in warm water for 20 minutes." },
            new Instruction { Id = 7, RecipeId = 2, StepNumber = 2, Description = "Heat oil in a wok over high heat." },
            new Instruction { Id = 8, RecipeId = 2, StepNumber = 3, Description = "Stir-fry shrimp and tofu until cooked." },
            new Instruction { Id = 9, RecipeId = 2, StepNumber = 4, Description = "Add noodles and sauce, toss to combine." },
            new Instruction { Id = 10, RecipeId = 2, StepNumber = 5, Description = "Top with peanuts and serve hot." },
            
            new Instruction { Id = 11, RecipeId = 3, StepNumber = 1, Description = "Preheat oven to 375°F (190°C)." },
            new Instruction { Id = 12, RecipeId = 3, StepNumber = 2, Description = "Cream together butter and sugar until fluffy." },
            new Instruction { Id = 13, RecipeId = 3, StepNumber = 3, Description = "Mix in flour and chocolate chips." },
            new Instruction { Id = 14, RecipeId = 3, StepNumber = 4, Description = "Drop spoonfuls onto baking sheet." },
            new Instruction { Id = 15, RecipeId = 3, StepNumber = 5, Description = "Bake for 10-12 minutes until golden." },
            
            new Instruction { Id = 16, RecipeId = 4, StepNumber = 1, Description = "Chop tomatoes and cucumber into bite-sized pieces." },
            new Instruction { Id = 17, RecipeId = 4, StepNumber = 2, Description = "Combine vegetables in a large bowl." },
            new Instruction { Id = 18, RecipeId = 4, StepNumber = 3, Description = "Add feta cheese and olives." },
            new Instruction { Id = 19, RecipeId = 4, StepNumber = 4, Description = "Drizzle with olive oil and lemon juice." },
            new Instruction { Id = 20, RecipeId = 4, StepNumber = 5, Description = "Toss gently and serve immediately." },
            
            new Instruction { Id = 21, RecipeId = 5, StepNumber = 1, Description = "Sear beef tenderloin on all sides until browned." },
            new Instruction { Id = 22, RecipeId = 5, StepNumber = 2, Description = "Prepare mushroom duxelles by finely chopping and sautéing mushrooms." },
            new Instruction { Id = 23, RecipeId = 5, StepNumber = 3, Description = "Wrap beef with prosciutto and mushroom mixture." },
            new Instruction { Id = 24, RecipeId = 5, StepNumber = 4, Description = "Encase in puff pastry and seal edges." },
            new Instruction { Id = 25, RecipeId = 5, StepNumber = 5, Description = "Bake at 425°F for 35-40 minutes until golden." }
        };
        modelBuilder.Entity<Instruction>().HasData(instructions);

        var recipeTags = new[]
        {
            new RecipeTag { RecipeId = 1, TagId = 1 },
            new RecipeTag { RecipeId = 1, TagId = 5 },
            new RecipeTag { RecipeId = 2, TagId = 2 },
            new RecipeTag { RecipeId = 2, TagId = 6 },
            new RecipeTag { RecipeId = 3, TagId = 2 },
            new RecipeTag { RecipeId = 3, TagId = 3 },
            new RecipeTag { RecipeId = 4, TagId = 1 },
            new RecipeTag { RecipeId = 4, TagId = 2 },
            new RecipeTag { RecipeId = 4, TagId = 4 }
        };
        modelBuilder.Entity<RecipeTag>().HasData(recipeTags);
    }
}
