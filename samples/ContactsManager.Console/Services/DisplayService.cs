using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public class DisplayService : IDisplayService
{
    public void ShowMenu()
    {
        System.Console.WriteLine("\n=== Contacts Manager ===");
        System.Console.WriteLine("1. List all contacts");
        System.Console.WriteLine("2. Search contacts");
        System.Console.WriteLine("3. Add new contact");
        System.Console.WriteLine("4. Edit contact");
        System.Console.WriteLine("5. Delete contact");
        System.Console.WriteLine("6. Manage tags");
        System.Console.WriteLine("7. Export to CSV");
        System.Console.WriteLine("8. Exit");
        System.Console.Write("\nSelect option: ");
    }

    public void ShowContacts(IEnumerable<Contact> contacts)
    {
        System.Console.WriteLine("\n=== Contacts ===");
        foreach (var contact in contacts)
        {
            var tags = string.Join(", ", contact.ContactTags.Select(ct => ct.Tag.Name));
            System.Console.WriteLine($"ID: {contact.Id} | {contact.FirstName} {contact.LastName} | {contact.Email} | {contact.Phone}");
            System.Console.WriteLine($"   Company: {contact.Company} | Tags: {tags}");
        }
        System.Console.WriteLine($"\nTotal: {contacts.Count()} contacts");
    }

    public void ShowContact(Contact contact)
    {
        System.Console.WriteLine("\n=== Contact Details ===");
        System.Console.WriteLine($"ID: {contact.Id}");
        System.Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
        System.Console.WriteLine($"Email: {contact.Email}");
        System.Console.WriteLine($"Phone: {contact.Phone}");
        System.Console.WriteLine($"Company: {contact.Company}");
        System.Console.WriteLine($"Notes: {contact.Notes}");
        System.Console.WriteLine($"Created: {contact.CreatedAt:yyyy-MM-dd}");
        var lastContacted = contact.LastContactedAt.HasValue 
            ? contact.LastContactedAt.Value.ToString("yyyy-MM-dd") 
            : "Never";
        System.Console.WriteLine($"Last Contacted: {lastContacted}");
        var tags = string.Join(", ", contact.ContactTags.Select(ct => ct.Tag.Name));
        System.Console.WriteLine($"Tags: {tags}");
    }

    public void ShowTags(IEnumerable<Tag> tags)
    {
        System.Console.WriteLine("\n=== Tags ===");
        foreach (var tag in tags)
        {
            var contactCount = tag.ContactTags.Count;
            System.Console.WriteLine($"ID: {tag.Id} | {tag.Name} ({contactCount} contacts)");
        }
    }

    public void ShowMessage(string message)
    {
        System.Console.WriteLine($"\n✓ {message}");
    }

    public void ShowError(string error)
    {
        System.Console.WriteLine($"\n✗ Error: {error}");
    }

    public Contact GetContactInput()
    {
        System.Console.WriteLine("\n=== New Contact ===");
        
        System.Console.Write("First Name: ");
        var firstName = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.Write("Last Name: ");
        var lastName = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.Write("Email: ");
        var email = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.Write("Phone: ");
        var phone = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.Write("Company: ");
        var company = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.Write("Notes: ");
        var notes = System.Console.ReadLine() ?? string.Empty;

        return new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Company = company,
            Notes = notes
        };
    }

    public string GetSearchInput()
    {
        System.Console.Write("\nEnter search term: ");
        return System.Console.ReadLine() ?? string.Empty;
    }

    public int GetContactIdInput()
    {
        System.Console.Write("\nEnter contact ID: ");
        var input = System.Console.ReadLine() ?? "0";
        return int.TryParse(input, out var id) ? id : 0;
    }

    public string GetExportPathInput()
    {
        System.Console.Write("\nEnter export file path (e.g., contacts.csv): ");
        return System.Console.ReadLine() ?? "contacts.csv";
    }
}
