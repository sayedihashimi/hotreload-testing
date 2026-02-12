using BookBuddy.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.RazorPages.Data;

public class BookBuddyContext : DbContext
{
    public BookBuddyContext(DbContextOptions<BookBuddyContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<ReadingSession> ReadingSessions { get; set; } = null!;
    public DbSet<ReadingGoal> ReadingGoals { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.ReadingSessions)
            .WithOne(rs => rs.Book)
            .HasForeignKey(rs => rs.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var currentYear = DateTime.Now.Year;

        // Seed Books (8-10 books in various statuses)
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "The Midnight Library",
                Author = "Matt Haig",
                ISBN = "978-0525559474",
                PageCount = 304,
                Genre = "Fiction",
                CoverImageUrl = "/images/covers/midnight-library.jpg",
                DateAdded = DateTime.Now.AddMonths(-6),
                Status = ReadingStatus.Finished,
                CurrentPage = 304,
                StartedDate = DateTime.Now.AddMonths(-5),
                FinishedDate = DateTime.Now.AddMonths(-4),
                Rating = 5,
                Notes = "Absolutely loved this book! Really makes you think about life choices."
            },
            new Book
            {
                Id = 2,
                Title = "Atomic Habits",
                Author = "James Clear",
                ISBN = "978-0735211292",
                PageCount = 320,
                Genre = "Self-Help",
                CoverImageUrl = "/images/covers/atomic-habits.jpg",
                DateAdded = DateTime.Now.AddMonths(-3),
                Status = ReadingStatus.Reading,
                CurrentPage = 180,
                StartedDate = DateTime.Now.AddDays(-15),
                Notes = "Great practical advice on building habits."
            },
            new Book
            {
                Id = 3,
                Title = "Project Hail Mary",
                Author = "Andy Weir",
                ISBN = "978-0593135204",
                PageCount = 496,
                Genre = "Science Fiction",
                CoverImageUrl = "/images/covers/hail-mary.jpg",
                DateAdded = DateTime.Now.AddMonths(-2),
                Status = ReadingStatus.Finished,
                CurrentPage = 496,
                StartedDate = DateTime.Now.AddMonths(-2),
                FinishedDate = DateTime.Now.AddMonths(-1),
                Rating = 5,
                Notes = "Another masterpiece from Andy Weir!"
            },
            new Book
            {
                Id = 4,
                Title = "The Thursday Murder Club",
                Author = "Richard Osman",
                ISBN = "978-1984880970",
                PageCount = 368,
                Genre = "Mystery",
                CoverImageUrl = "/images/covers/thursday-murder.jpg",
                DateAdded = DateTime.Now.AddDays(-21),
                Status = ReadingStatus.Reading,
                CurrentPage = 120,
                StartedDate = DateTime.Now.AddDays(-14),
                Notes = "Witty and engaging mystery."
            },
            new Book
            {
                Id = 5,
                Title = "Educated",
                Author = "Tara Westover",
                ISBN = "978-0399590504",
                PageCount = 352,
                Genre = "Biography",
                CoverImageUrl = "/images/covers/educated.jpg",
                DateAdded = DateTime.Now.AddMonths(-8),
                Status = ReadingStatus.Finished,
                CurrentPage = 352,
                StartedDate = DateTime.Now.AddMonths(-7),
                FinishedDate = DateTime.Now.AddMonths(-6),
                Rating = 4,
                Notes = "Powerful memoir about education and family."
            },
            new Book
            {
                Id = 6,
                Title = "The Silent Patient",
                Author = "Alex Michaelides",
                ISBN = "978-1250301697",
                PageCount = 336,
                Genre = "Thriller",
                CoverImageUrl = "/images/covers/silent-patient.jpg",
                DateAdded = DateTime.Now.AddDays(-5),
                Status = ReadingStatus.WantToRead,
                Notes = "Heard great things about the twist ending!"
            },
            new Book
            {
                Id = 7,
                Title = "Sapiens",
                Author = "Yuval Noah Harari",
                ISBN = "978-0062316097",
                PageCount = 464,
                Genre = "History",
                CoverImageUrl = "/images/covers/sapiens.jpg",
                DateAdded = DateTime.Now.AddMonths(-4),
                Status = ReadingStatus.Abandoned,
                CurrentPage = 150,
                StartedDate = DateTime.Now.AddMonths(-3),
                Notes = "Interesting but too dense for me right now. Will retry later."
            },
            new Book
            {
                Id = 8,
                Title = "The Seven Husbands of Evelyn Hugo",
                Author = "Taylor Jenkins Reid",
                ISBN = "978-1501161933",
                PageCount = 400,
                Genre = "Fiction",
                CoverImageUrl = "/images/covers/evelyn-hugo.jpg",
                DateAdded = DateTime.Now.AddDays(-7),
                Status = ReadingStatus.WantToRead,
                Notes = "On my must-read list!"
            },
            new Book
            {
                Id = 9,
                Title = "1984",
                Author = "George Orwell",
                ISBN = "978-0451524935",
                PageCount = 328,
                Genre = "Classic",
                CoverImageUrl = "/images/covers/1984.jpg",
                DateAdded = DateTime.Now.AddMonths(-10),
                Status = ReadingStatus.Finished,
                CurrentPage = 328,
                StartedDate = DateTime.Now.AddMonths(-9),
                FinishedDate = DateTime.Now.AddMonths(-8),
                Rating = 5,
                Notes = "Disturbingly relevant even today."
            },
            new Book
            {
                Id = 10,
                Title = "The Song of Achilles",
                Author = "Madeline Miller",
                ISBN = "978-0062060624",
                PageCount = 352,
                Genre = "Historical Fiction",
                CoverImageUrl = "/images/covers/song-achilles.jpg",
                DateAdded = DateTime.Now.AddDays(-10),
                Status = ReadingStatus.WantToRead,
                Notes = "Recommended by book club."
            }
        );

        // Seed Reading Sessions
        modelBuilder.Entity<ReadingSession>().HasData(
            // Sessions for "The Midnight Library" (Finished)
            new ReadingSession { Id = 1, BookId = 1, Date = DateTime.Now.AddMonths(-5), PagesRead = 80, MinutesSpent = 120 },
            new ReadingSession { Id = 2, BookId = 1, Date = DateTime.Now.AddMonths(-5).AddDays(2), PagesRead = 70, MinutesSpent = 90 },
            new ReadingSession { Id = 3, BookId = 1, Date = DateTime.Now.AddMonths(-4).AddDays(-5), PagesRead = 154, MinutesSpent = 180 },
            
            // Sessions for "Atomic Habits" (Currently Reading)
            new ReadingSession { Id = 4, BookId = 2, Date = DateTime.Now.AddDays(-15), PagesRead = 60, MinutesSpent = 75 },
            new ReadingSession { Id = 5, BookId = 2, Date = DateTime.Now.AddDays(-10), PagesRead = 50, MinutesSpent = 60 },
            new ReadingSession { Id = 6, BookId = 2, Date = DateTime.Now.AddDays(-5), PagesRead = 40, MinutesSpent = 50 },
            new ReadingSession { Id = 7, BookId = 2, Date = DateTime.Now.AddDays(-2), PagesRead = 30, MinutesSpent = 45 },
            
            // Sessions for "Project Hail Mary" (Finished)
            new ReadingSession { Id = 8, BookId = 3, Date = DateTime.Now.AddMonths(-2), PagesRead = 100, MinutesSpent = 150 },
            new ReadingSession { Id = 9, BookId = 3, Date = DateTime.Now.AddMonths(-2).AddDays(3), PagesRead = 120, MinutesSpent = 160 },
            new ReadingSession { Id = 10, BookId = 3, Date = DateTime.Now.AddMonths(-2).AddDays(7), PagesRead = 150, MinutesSpent = 180 },
            new ReadingSession { Id = 11, BookId = 3, Date = DateTime.Now.AddMonths(-1).AddDays(-3), PagesRead = 126, MinutesSpent = 150 },
            
            // Sessions for "The Thursday Murder Club" (Currently Reading)
            new ReadingSession { Id = 12, BookId = 4, Date = DateTime.Now.AddDays(-14), PagesRead = 70, MinutesSpent = 90 },
            new ReadingSession { Id = 13, BookId = 4, Date = DateTime.Now.AddDays(-7), PagesRead = 50, MinutesSpent = 65 },
            
            // Sessions for "Educated" (Finished)
            new ReadingSession { Id = 14, BookId = 5, Date = DateTime.Now.AddMonths(-7), PagesRead = 100, MinutesSpent = 130 },
            new ReadingSession { Id = 15, BookId = 5, Date = DateTime.Now.AddMonths(-7).AddDays(5), PagesRead = 120, MinutesSpent = 150 },
            new ReadingSession { Id = 16, BookId = 5, Date = DateTime.Now.AddMonths(-6).AddDays(-3), PagesRead = 132, MinutesSpent = 160 },
            
            // Sessions for "1984" (Finished)
            new ReadingSession { Id = 17, BookId = 9, Date = DateTime.Now.AddMonths(-9), PagesRead = 110, MinutesSpent = 140 },
            new ReadingSession { Id = 18, BookId = 9, Date = DateTime.Now.AddMonths(-9).AddDays(4), PagesRead = 108, MinutesSpent = 135 },
            new ReadingSession { Id = 19, BookId = 9, Date = DateTime.Now.AddMonths(-8).AddDays(-2), PagesRead = 110, MinutesSpent = 145 }
        );

        // Seed Reading Goal for current year
        modelBuilder.Entity<ReadingGoal>().HasData(
            new ReadingGoal
            {
                Id = 1,
                Year = currentYear,
                TargetBooks = 24,
                TargetPages = 8000
            }
        );
    }
}
