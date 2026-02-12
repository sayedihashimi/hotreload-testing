using Microsoft.EntityFrameworkCore;
using ContactsManager.Console.Models;

namespace ContactsManager.Console.Data;

public class ContactsDbContext : DbContext
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ContactTag> ContactTags => Set<ContactTag>();

    public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContactTag>()
            .HasKey(ct => new { ct.ContactId, ct.TagId });

        modelBuilder.Entity<ContactTag>()
            .HasOne(ct => ct.Contact)
            .WithMany(c => c.ContactTags)
            .HasForeignKey(ct => ct.ContactId);

        modelBuilder.Entity<ContactTag>()
            .HasOne(ct => ct.Tag)
            .WithMany(t => t.ContactTags)
            .HasForeignKey(ct => ct.TagId);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var tags = new[]
        {
            new Tag { Id = 1, Name = "Family" },
            new Tag { Id = 2, Name = "Work" },
            new Tag { Id = 3, Name = "Friends" },
            new Tag { Id = 4, Name = "VIP" },
            new Tag { Id = 5, Name = "Newsletter" }
        };

        modelBuilder.Entity<Tag>().HasData(tags);

        var contacts = new[]
        {
            new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                Phone = "(555) 123-4567",
                Company = "Tech Corp",
                Notes = "CEO of Tech Corp",
                CreatedAt = new DateTime(2024, 6, 1),
                LastContactedAt = new DateTime(2025, 1, 26)
            },
            new Contact
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Johnson",
                Email = "sarah.j@email.com",
                Phone = "(555) 234-5678",
                Company = "Design Studio",
                Notes = "Freelance designer",
                CreatedAt = new DateTime(2024, 8, 1),
                LastContactedAt = new DateTime(2025, 1, 29)
            },
            new Contact
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Brown",
                Email = "mbrown@company.com",
                Phone = "(555) 345-6789",
                Company = "Global Industries",
                Notes = "Project manager",
                CreatedAt = new DateTime(2024, 9, 1),
                LastContactedAt = new DateTime(2025, 1, 21)
            },
            new Contact
            {
                Id = 4,
                FirstName = "Emily",
                LastName = "Davis",
                Email = "emily.davis@mail.com",
                Phone = "(555) 456-7890",
                Company = "Marketing Plus",
                Notes = "Marketing specialist",
                CreatedAt = new DateTime(2024, 7, 1),
                LastContactedAt = new DateTime(2025, 1, 30)
            },
            new Contact
            {
                Id = 5,
                FirstName = "David",
                LastName = "Wilson",
                Email = "david.w@example.org",
                Phone = "(555) 567-8901",
                Company = "Wilson & Associates",
                Notes = "Legal consultant",
                CreatedAt = new DateTime(2024, 10, 1),
                LastContactedAt = null
            },
            new Contact
            {
                Id = 6,
                FirstName = "Jennifer",
                LastName = "Martinez",
                Email = "jen.martinez@email.com",
                Phone = "(555) 678-9012",
                Company = "Health First",
                Notes = "Healthcare provider",
                CreatedAt = new DateTime(2024, 11, 1),
                LastContactedAt = new DateTime(2025, 1, 24)
            },
            new Contact
            {
                Id = 7,
                FirstName = "Robert",
                LastName = "Taylor",
                Email = "rtaylor@tech.com",
                Phone = "(555) 789-0123",
                Company = "Tech Solutions",
                Notes = "Software engineer",
                CreatedAt = new DateTime(2024, 4, 1),
                LastContactedAt = new DateTime(2025, 1, 28)
            },
            new Contact
            {
                Id = 8,
                FirstName = "Lisa",
                LastName = "Anderson",
                Email = "lisa.a@example.com",
                Phone = "(555) 890-1234",
                Company = "Creative Agency",
                Notes = "Creative director",
                CreatedAt = new DateTime(2024, 5, 1),
                LastContactedAt = new DateTime(2025, 1, 16)
            },
            new Contact
            {
                Id = 9,
                FirstName = "William",
                LastName = "Thomas",
                Email = "wthomas@business.com",
                Phone = "(555) 901-2345",
                Company = "Business Ventures",
                Notes = "Entrepreneur",
                CreatedAt = new DateTime(2024, 3, 1),
                LastContactedAt = new DateTime(2025, 1, 11)
            },
            new Contact
            {
                Id = 10,
                FirstName = "Amanda",
                LastName = "Garcia",
                Email = "amanda.garcia@mail.com",
                Phone = "(555) 012-3456",
                Company = "Education Group",
                Notes = "Education coordinator",
                CreatedAt = new DateTime(2024, 8, 1),
                LastContactedAt = new DateTime(2025, 1, 27)
            }
        };

        modelBuilder.Entity<Contact>().HasData(contacts);

        var contactTags = new[]
        {
            new ContactTag { ContactId = 1, TagId = 2 }, // John - Work
            new ContactTag { ContactId = 1, TagId = 4 }, // John - VIP
            new ContactTag { ContactId = 2, TagId = 2 }, // Sarah - Work
            new ContactTag { ContactId = 3, TagId = 2 }, // Michael - Work
            new ContactTag { ContactId = 4, TagId = 2 }, // Emily - Work
            new ContactTag { ContactId = 5, TagId = 2 }, // David - Work
            new ContactTag { ContactId = 6, TagId = 1 }, // Jennifer - Family
            new ContactTag { ContactId = 7, TagId = 3 }, // Robert - Friends
            new ContactTag { ContactId = 8, TagId = 2 }, // Lisa - Work
            new ContactTag { ContactId = 8, TagId = 4 }, // Lisa - VIP
            new ContactTag { ContactId = 9, TagId = 2 }, // William - Work
            new ContactTag { ContactId = 10, TagId = 5 }  // Amanda - Newsletter
        };

        modelBuilder.Entity<ContactTag>().HasData(contactTags);
    }
}
