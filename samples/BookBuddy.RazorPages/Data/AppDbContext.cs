using Microsoft.EntityFrameworkCore;
using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<ReadingSession> ReadingSessions { get; set; }
    public DbSet<ReadingGoal> ReadingGoals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.ReadingSessions)
            .WithOne(rs => rs.Book)
            .HasForeignKey(rs => rs.BookId);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var books = new[]
        {
            new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Genre = "Classic Fiction",
                Pages = 180,
                Status = ReadingStatus.Completed,
                StartedDate = new DateTime(2025, 1, 5),
                CompletedDate = new DateTime(2025, 1, 15),
                Rating = 5
            },
            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                Genre = "Dystopian",
                Pages = 328,
                Status = ReadingStatus.Completed,
                StartedDate = new DateTime(2025, 1, 16),
                CompletedDate = new DateTime(2025, 1, 28),
                Rating = 5
            },
            new Book
            {
                Id = 3,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Genre = "Classic Fiction",
                Pages = 324,
                Status = ReadingStatus.Reading,
                StartedDate = new DateTime(2025, 2, 1)
            },
            new Book
            {
                Id = 4,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                Pages = 310,
                Status = ReadingStatus.Reading,
                StartedDate = new DateTime(2025, 2, 5)
            },
            new Book
            {
                Id = 5,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Genre = "Romance",
                Pages = 432,
                Status = ReadingStatus.ToRead
            },
            new Book
            {
                Id = 6,
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                Genre = "Coming-of-age",
                Pages = 277,
                Status = ReadingStatus.OnHold,
                StartedDate = new DateTime(2024, 12, 20)
            },
            new Book
            {
                Id = 7,
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Genre = "Dystopian",
                Pages = 268,
                Status = ReadingStatus.ToRead
            },
            new Book
            {
                Id = 8,
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                Pages = 1178,
                Status = ReadingStatus.ToRead
            },
            new Book
            {
                Id = 9,
                Title = "Animal Farm",
                Author = "George Orwell",
                Genre = "Political Satire",
                Pages = 112,
                Status = ReadingStatus.Completed,
                StartedDate = new DateTime(2024, 12, 28),
                CompletedDate = new DateTime(2025, 1, 2),
                Rating = 4
            },
            new Book
            {
                Id = 10,
                Title = "Fahrenheit 451",
                Author = "Ray Bradbury",
                Genre = "Science Fiction",
                Pages = 249,
                Status = ReadingStatus.Abandoned,
                StartedDate = new DateTime(2024, 11, 10)
            }
        };

        modelBuilder.Entity<Book>().HasData(books);

        var sessions = new[]
        {
            new ReadingSession
            {
                Id = 1,
                BookId = 1,
                SessionDate = new DateTime(2025, 1, 5),
                PagesRead = 90,
                MinutesRead = 120
            },
            new ReadingSession
            {
                Id = 2,
                BookId = 1,
                SessionDate = new DateTime(2025, 1, 10),
                PagesRead = 90,
                MinutesRead = 110
            },
            new ReadingSession
            {
                Id = 3,
                BookId = 2,
                SessionDate = new DateTime(2025, 1, 16),
                PagesRead = 100,
                MinutesRead = 150
            },
            new ReadingSession
            {
                Id = 4,
                BookId = 2,
                SessionDate = new DateTime(2025, 1, 20),
                PagesRead = 128,
                MinutesRead = 180
            },
            new ReadingSession
            {
                Id = 5,
                BookId = 2,
                SessionDate = new DateTime(2025, 1, 25),
                PagesRead = 100,
                MinutesRead = 140
            },
            new ReadingSession
            {
                Id = 6,
                BookId = 3,
                SessionDate = new DateTime(2025, 2, 1),
                PagesRead = 80,
                MinutesRead = 90
            },
            new ReadingSession
            {
                Id = 7,
                BookId = 3,
                SessionDate = new DateTime(2025, 2, 6),
                PagesRead = 95,
                MinutesRead = 100
            },
            new ReadingSession
            {
                Id = 8,
                BookId = 4,
                SessionDate = new DateTime(2025, 2, 5),
                PagesRead = 60,
                MinutesRead = 75
            },
            new ReadingSession
            {
                Id = 9,
                BookId = 9,
                SessionDate = new DateTime(2024, 12, 28),
                PagesRead = 112,
                MinutesRead = 90
            }
        };

        modelBuilder.Entity<ReadingSession>().HasData(sessions);

        var goals = new[]
        {
            new ReadingGoal
            {
                Id = 1,
                Year = 2025,
                TargetBooks = 52,
                TargetPages = 15000,
                Description = "Read one book per week challenge"
            }
        };

        modelBuilder.Entity<ReadingGoal>().HasData(goals);
    }
}
