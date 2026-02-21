using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Models;

namespace LinkVault.MinimalApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Link> Links { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<LinkTag> LinkTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LinkTag>()
            .HasKey(lt => new { lt.LinkId, lt.TagId });

        modelBuilder.Entity<LinkTag>()
            .HasOne(lt => lt.Link)
            .WithMany(l => l.LinkTags)
            .HasForeignKey(lt => lt.LinkId);

        modelBuilder.Entity<LinkTag>()
            .HasOne(lt => lt.Tag)
            .WithMany(t => t.LinkTags)
            .HasForeignKey(lt => lt.TagId);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var collections = new[]
        {
            new Collection { Id = 1, Name = "Dev Tools", Description = "Essential developer tools and utilities", Color = "#3B82F6", IsPublic = true, CreatedAt = new DateTime(2026, 1, 5) },
            new Collection { Id = 2, Name = "News", Description = "Tech news and industry updates", Color = "#EF4444", IsPublic = true, CreatedAt = new DateTime(2026, 1, 10) },
            new Collection { Id = 3, Name = "Learning", Description = "Tutorials, courses, and educational content", Color = "#10B981", IsPublic = false, CreatedAt = new DateTime(2026, 1, 15) }
        };
        modelBuilder.Entity<Collection>().HasData(collections);

        var tags = new[]
        {
            new Tag { Id = 1, Name = "reference" },
            new Tag { Id = 2, Name = "tutorial" },
            new Tag { Id = 3, Name = "tool" },
            new Tag { Id = 4, Name = "article" },
            new Tag { Id = 5, Name = "video" }
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        var links = new[]
        {
            new Link { Id = 1, Url = "https://github.com", Title = "GitHub", Description = "Code hosting and collaboration platform", IsFavorite = true, ClickCount = 42, CreatedAt = new DateTime(2026, 1, 6), LastClickedAt = new DateTime(2026, 2, 15), CollectionId = 1 },
            new Link { Id = 2, Url = "https://learn.microsoft.com/aspnet/core", Title = "ASP.NET Core Docs", Description = "Official ASP.NET Core documentation", IsFavorite = true, ClickCount = 35, CreatedAt = new DateTime(2026, 1, 7), LastClickedAt = new DateTime(2026, 2, 14), CollectionId = 3 },
            new Link { Id = 3, Url = "https://code.visualstudio.com", Title = "Visual Studio Code", Description = "Free source-code editor by Microsoft", IsFavorite = true, ClickCount = 28, CreatedAt = new DateTime(2026, 1, 8), LastClickedAt = new DateTime(2026, 2, 13), CollectionId = 1 },
            new Link { Id = 4, Url = "https://devblogs.microsoft.com/dotnet", Title = ".NET Blog", Description = "Official .NET team blog", IsFavorite = false, ClickCount = 18, CreatedAt = new DateTime(2026, 1, 10), LastClickedAt = new DateTime(2026, 2, 10), CollectionId = 2 },
            new Link { Id = 5, Url = "https://stackoverflow.com", Title = "Stack Overflow", Description = "Q&A for professional and enthusiast programmers", IsFavorite = false, ClickCount = 55, CreatedAt = new DateTime(2026, 1, 11), LastClickedAt = new DateTime(2026, 2, 16), CollectionId = 1 },
            new Link { Id = 6, Url = "https://www.youtube.com/@dotnet", Title = ".NET YouTube Channel", Description = "Official .NET YouTube channel with tutorials", IsFavorite = false, ClickCount = 12, CreatedAt = new DateTime(2026, 1, 14), LastClickedAt = new DateTime(2026, 2, 8), CollectionId = 3 },
            new Link { Id = 7, Url = "https://nuget.org", Title = "NuGet Gallery", Description = "Package manager for .NET", IsFavorite = false, ClickCount = 22, CreatedAt = new DateTime(2026, 1, 15), LastClickedAt = new DateTime(2026, 2, 12), CollectionId = 1 },
            new Link { Id = 8, Url = "https://techcrunch.com", Title = "TechCrunch", Description = "Technology news and analysis", IsFavorite = false, ClickCount = 8, CreatedAt = new DateTime(2026, 1, 18), LastClickedAt = new DateTime(2026, 2, 5), CollectionId = 2 },
            new Link { Id = 9, Url = "https://learn.microsoft.com/ef/core", Title = "EF Core Docs", Description = "Entity Framework Core documentation", IsFavorite = false, ClickCount = 15, CreatedAt = new DateTime(2026, 1, 20), LastClickedAt = new DateTime(2026, 2, 9), CollectionId = 3 },
            new Link { Id = 10, Url = "https://thehackernews.com", Title = "The Hacker News", Description = "Cybersecurity news and insights", IsFavorite = false, ClickCount = 6, CreatedAt = new DateTime(2026, 1, 22), LastClickedAt = new DateTime(2026, 2, 3), CollectionId = 2 },
            new Link { Id = 11, Url = "https://www.jetbrains.com/rider", Title = "JetBrains Rider", Description = "Cross-platform .NET IDE", IsFavorite = false, ClickCount = 10, CreatedAt = new DateTime(2026, 1, 25), LastClickedAt = new DateTime(2026, 2, 7), CollectionId = 1 },
            new Link { Id = 12, Url = "https://www.pluralsight.com", Title = "Pluralsight", Description = "Online technology courses", IsFavorite = false, ClickCount = 4, CreatedAt = new DateTime(2026, 1, 28), LastClickedAt = new DateTime(2026, 2, 1), CollectionId = 3 },
            new Link { Id = 13, Url = "https://arstechnica.com", Title = "Ars Technica", Description = "Technology news and information", IsFavorite = false, ClickCount = 9, CreatedAt = new DateTime(2026, 2, 1), LastClickedAt = new DateTime(2026, 2, 11), CollectionId = 2 },
            new Link { Id = 14, Url = "https://linqpad.net", Title = "LINQPad", Description = "Instant C# and LINQ scratchpad", IsFavorite = true, ClickCount = 19, CreatedAt = new DateTime(2026, 2, 3), LastClickedAt = new DateTime(2026, 2, 14), CollectionId = 1 },
            new Link { Id = 15, Url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ", Title = "C# Advanced Patterns", Description = "Video tutorial on advanced C# patterns", IsFavorite = false, ClickCount = 3, CreatedAt = new DateTime(2026, 2, 5), LastClickedAt = new DateTime(2026, 2, 6), CollectionId = 3 }
        };
        modelBuilder.Entity<Link>().HasData(links);

        var linkTags = new[]
        {
            new LinkTag { LinkId = 1, TagId = 3 },  // GitHub - tool
            new LinkTag { LinkId = 2, TagId = 1 },  // ASP.NET Docs - reference
            new LinkTag { LinkId = 2, TagId = 2 },  // ASP.NET Docs - tutorial
            new LinkTag { LinkId = 3, TagId = 3 },  // VS Code - tool
            new LinkTag { LinkId = 4, TagId = 4 },  // .NET Blog - article
            new LinkTag { LinkId = 5, TagId = 1 },  // Stack Overflow - reference
            new LinkTag { LinkId = 5, TagId = 3 },  // Stack Overflow - tool
            new LinkTag { LinkId = 6, TagId = 5 },  // .NET YouTube - video
            new LinkTag { LinkId = 6, TagId = 2 },  // .NET YouTube - tutorial
            new LinkTag { LinkId = 7, TagId = 3 },  // NuGet - tool
            new LinkTag { LinkId = 8, TagId = 4 },  // TechCrunch - article
            new LinkTag { LinkId = 9, TagId = 1 },  // EF Core Docs - reference
            new LinkTag { LinkId = 9, TagId = 2 },  // EF Core Docs - tutorial
            new LinkTag { LinkId = 10, TagId = 4 }, // Hacker News - article
            new LinkTag { LinkId = 11, TagId = 3 }, // Rider - tool
            new LinkTag { LinkId = 12, TagId = 2 }, // Pluralsight - tutorial
            new LinkTag { LinkId = 12, TagId = 5 }, // Pluralsight - video
            new LinkTag { LinkId = 13, TagId = 4 }, // Ars Technica - article
            new LinkTag { LinkId = 14, TagId = 3 }, // LINQPad - tool
            new LinkTag { LinkId = 15, TagId = 5 }, // C# Patterns - video
            new LinkTag { LinkId = 15, TagId = 2 }  // C# Patterns - tutorial
        };
        modelBuilder.Entity<LinkTag>().HasData(linkTags);
    }
}
