using Microsoft.EntityFrameworkCore;
using ContactsManager.Console.Models;

namespace ContactsManager.Console.Data;

public class ContactsContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=contacts.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        // Seed data
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Family" },
            new Tag { Id = 2, Name = "Work" },
            new Tag { Id = 3, Name = "Friends" },
            new Tag { Id = 4, Name = "VIP" },
            new Tag { Id = 5, Name = "Newsletter" }
        );

        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "555-0101", Company = "Acme Corp", CreatedAt = DateTime.Now.AddMonths(-6) },
            new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "555-0102", Company = "Tech Solutions", CreatedAt = DateTime.Now.AddMonths(-5) },
            new Contact { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.j@example.com", Phone = "555-0103", Company = "Design Studio", CreatedAt = DateTime.Now.AddMonths(-4) },
            new Contact { Id = 4, FirstName = "Alice", LastName = "Williams", Email = "alice.w@example.com", Phone = "555-0104", Company = "Marketing Plus", CreatedAt = DateTime.Now.AddMonths(-3) },
            new Contact { Id = 5, FirstName = "Charlie", LastName = "Brown", Email = "charlie.b@example.com", Phone = "555-0105", Company = "Web Agency", CreatedAt = DateTime.Now.AddMonths(-2) },
            new Contact { Id = 6, FirstName = "Diana", LastName = "Davis", Email = "diana.d@example.com", Phone = "555-0106", Company = "", Notes = "Met at conference", CreatedAt = DateTime.Now.AddMonths(-1) },
            new Contact { Id = 7, FirstName = "Edward", LastName = "Miller", Email = "ed.miller@example.com", Phone = "555-0107", Company = "Consulting Group", CreatedAt = DateTime.Now.AddDays(-20) },
            new Contact { Id = 8, FirstName = "Fiona", LastName = "Wilson", Email = "fiona.w@example.com", Phone = "555-0108", Company = "Creative Labs", CreatedAt = DateTime.Now.AddDays(-15) },
            new Contact { Id = 9, FirstName = "George", LastName = "Taylor", Email = "george.t@example.com", Phone = "555-0109", Company = "Finance Corp", CreatedAt = DateTime.Now.AddDays(-10) },
            new Contact { Id = 10, FirstName = "Hannah", LastName = "Anderson", Email = "hannah.a@example.com", Phone = "555-0110", Company = "Legal Services", CreatedAt = DateTime.Now.AddDays(-5) }
        );

        modelBuilder.Entity<ContactTag>().HasData(
            new ContactTag { ContactId = 1, TagId = 2 },
            new ContactTag { ContactId = 2, TagId = 2 },
            new ContactTag { ContactId = 2, TagId = 4 },
            new ContactTag { ContactId = 3, TagId = 3 },
            new ContactTag { ContactId = 4, TagId = 2 },
            new ContactTag { ContactId = 5, TagId = 2 },
            new ContactTag { ContactId = 6, TagId = 1 },
            new ContactTag { ContactId = 7, TagId = 4 },
            new ContactTag { ContactId = 8, TagId = 3 },
            new ContactTag { ContactId = 9, TagId = 2 },
            new ContactTag { ContactId = 10, TagId = 2 }
        );
    }
}
