using ContactsManager.Console.Data;
using ContactsManager.Console.Models;
using Microsoft.EntityFrameworkCore;

// Initialize database
using (var context = new ContactsContext())
{
    context.Database.EnsureCreated();
}

while (true)
{
    ShowMenu();
    var choice = System.Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                await ListContacts();
                break;
            case "2":
                await SearchContacts();
                break;
            case "3":
                await AddContact();
                break;
            case "4":
                await EditContact();
                break;
            case "5":
                await DeleteContact();
                break;
            case "6":
                await ManageTags();
                break;
            case "7":
                System.Console.WriteLine("Export feature coming soon!");
                PauseForUser();
                break;
            case "8":
                System.Console.WriteLine("Goodbye!");
                return;
            default:
                System.Console.WriteLine("Invalid option. Please try again.");
                PauseForUser();
                break;
        }
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Error: {ex.Message}");
        PauseForUser();
    }
}

void ShowMenu()
{
    System.Console.Clear();
    System.Console.WriteLine("=========================");
    System.Console.WriteLine("=== Contacts Manager ===");
    System.Console.WriteLine("=========================");
    System.Console.WriteLine();
    System.Console.WriteLine("1. List all contacts");
    System.Console.WriteLine("2. Search contacts");
    System.Console.WriteLine("3. Add new contact");
    System.Console.WriteLine("4. Edit contact");
    System.Console.WriteLine("5. Delete contact");
    System.Console.WriteLine("6. Manage tags");
    System.Console.WriteLine("7. Export to CSV");
    System.Console.WriteLine("8. Exit");
    System.Console.WriteLine();
    System.Console.Write("Select option: ");
}

async Task ListContacts()
{
    using var context = new ContactsContext();
    var contacts = await context.Contacts
        .Include(c => c.ContactTags)
        .ThenInclude(ct => ct.Tag)
        .ToListAsync();

    System.Console.Clear();
    System.Console.WriteLine("=== All Contacts ===\n");

    if (!contacts.Any())
    {
        System.Console.WriteLine("No contacts found.");
    }
    else
    {
        foreach (var contact in contacts)
        {
            DisplayContact(contact);
        }
    }

    PauseForUser();
}

async Task SearchContacts()
{
    System.Console.Clear();
    System.Console.Write("Enter search term: ");
    var searchTerm = System.Console.ReadLine() ?? "";

    using var context = new ContactsContext();
    var contacts = await context.Contacts
        .Include(c => c.ContactTags)
        .ThenInclude(ct => ct.Tag)
        .Where(c => c.FirstName.Contains(searchTerm) ||
                    c.LastName.Contains(searchTerm) ||
                    c.Email.Contains(searchTerm) ||
                    c.Company.Contains(searchTerm))
        .ToListAsync();

    System.Console.WriteLine($"\n=== Search Results ({contacts.Count}) ===\n");

    foreach (var contact in contacts)
    {
        DisplayContact(contact);
    }

    PauseForUser();
}

async Task AddContact()
{
    System.Console.Clear();
    System.Console.WriteLine("=== Add New Contact ===\n");

    System.Console.Write("First Name: ");
    var firstName = System.Console.ReadLine() ?? "";

    System.Console.Write("Last Name: ");
    var lastName = System.Console.ReadLine() ?? "";

    System.Console.Write("Email: ");
    var email = System.Console.ReadLine() ?? "";

    System.Console.Write("Phone: ");
    var phone = System.Console.ReadLine() ?? "";

    System.Console.Write("Company: ");
    var company = System.Console.ReadLine() ?? "";

    System.Console.Write("Notes: ");
    var notes = System.Console.ReadLine() ?? "";

    var contact = new Contact
    {
        FirstName = firstName,
        LastName = lastName,
        Email = email,
        Phone = phone,
        Company = company,
        Notes = notes,
        CreatedAt = DateTime.Now
    };

    using var context = new ContactsContext();
    context.Contacts.Add(contact);
    await context.SaveChangesAsync();

    System.Console.WriteLine("\n✓ Contact added successfully!");
    PauseForUser();
}

async Task EditContact()
{
    System.Console.Clear();
    System.Console.Write("Enter contact ID to edit: ");
    if (!int.TryParse(System.Console.ReadLine(), out int id))
    {
        System.Console.WriteLine("Invalid ID.");
        PauseForUser();
        return;
    }

    using var context = new ContactsContext();
    var contact = await context.Contacts.FindAsync(id);

    if (contact == null)
    {
        System.Console.WriteLine("Contact not found.");
        PauseForUser();
        return;
    }

    System.Console.WriteLine($"\nEditing: {contact.FirstName} {contact.LastName}");
    System.Console.WriteLine("(Press Enter to keep current value)\n");

    System.Console.Write($"First Name [{contact.FirstName}]: ");
    var firstName = System.Console.ReadLine();
    if (!string.IsNullOrEmpty(firstName)) contact.FirstName = firstName;

    System.Console.Write($"Last Name [{contact.LastName}]: ");
    var lastName = System.Console.ReadLine();
    if (!string.IsNullOrEmpty(lastName)) contact.LastName = lastName;

    System.Console.Write($"Email [{contact.Email}]: ");
    var email = System.Console.ReadLine();
    if (!string.IsNullOrEmpty(email)) contact.Email = email;

    System.Console.Write($"Phone [{contact.Phone}]: ");
    var phone = System.Console.ReadLine();
    if (!string.IsNullOrEmpty(phone)) contact.Phone = phone;

    await context.SaveChangesAsync();

    System.Console.WriteLine("\n✓ Contact updated successfully!");
    PauseForUser();
}

async Task DeleteContact()
{
    System.Console.Clear();
    System.Console.Write("Enter contact ID to delete: ");
    if (!int.TryParse(System.Console.ReadLine(), out int id))
    {
        System.Console.WriteLine("Invalid ID.");
        PauseForUser();
        return;
    }

    using var context = new ContactsContext();
    var contact = await context.Contacts.FindAsync(id);

    if (contact == null)
    {
        System.Console.WriteLine("Contact not found.");
        PauseForUser();
        return;
    }

    System.Console.Write($"\nDelete {contact.FirstName} {contact.LastName}? (y/n): ");
    var confirm = System.Console.ReadLine();

    if (confirm?.ToLower() == "y")
    {
        context.Contacts.Remove(contact);
        await context.SaveChangesAsync();
        System.Console.WriteLine("\n✓ Contact deleted successfully!");
    }
    else
    {
        System.Console.WriteLine("\nDeletion cancelled.");
    }

    PauseForUser();
}

async Task ManageTags()
{
    using var context = new ContactsContext();
    var tags = await context.Tags.ToListAsync();

    System.Console.Clear();
    System.Console.WriteLine("=== Tags ===\n");

    foreach (var tag in tags)
    {
        System.Console.WriteLine($"[{tag.Id}] {tag.Name}");
    }

    PauseForUser();
}

void DisplayContact(Contact contact)
{
    System.Console.WriteLine($"[{contact.Id}] {contact.FirstName} {contact.LastName}");
    System.Console.WriteLine($"    Email: {contact.Email}");
    System.Console.WriteLine($"    Phone: {contact.Phone}");
    if (!string.IsNullOrEmpty(contact.Company))
        System.Console.WriteLine($"    Company: {contact.Company}");
    if (contact.ContactTags.Any())
    {
        var tagNames = string.Join(", ", contact.ContactTags.Select(ct => ct.Tag.Name));
        System.Console.WriteLine($"    Tags: {tagNames}");
    }
    System.Console.WriteLine();
}

void PauseForUser()
{
    System.Console.WriteLine("\nPress any key to continue...");
    System.Console.ReadKey(true);
}
