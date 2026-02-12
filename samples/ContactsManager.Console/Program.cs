using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactsManager.Console.Data;
using ContactsManager.Console.Services;

var services = new ServiceCollection();

services.AddDbContext<ContactsDbContext>(options =>
    options.UseSqlite("Data Source=contacts.db"));

services.AddScoped<IContactService, ContactService>();
services.AddScoped<ITagService, TagService>();
services.AddScoped<IExportService, ExportService>();
services.AddScoped<IDisplayService, DisplayService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
    await dbContext.Database.MigrateAsync();
}

var contactService = serviceProvider.GetRequiredService<IContactService>();
var tagService = serviceProvider.GetRequiredService<ITagService>();
var exportService = serviceProvider.GetRequiredService<IExportService>();
var displayService = serviceProvider.GetRequiredService<IDisplayService>();

var running = true;

while (running)
{
    displayService.ShowMenu();
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                await ListAllContacts();
                break;
            case "2":
                await SearchContacts();
                break;
            case "3":
                await AddNewContact();
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
                await ExportContacts();
                break;
            case "8":
                running = false;
                displayService.ShowMessage("Goodbye!");
                break;
            default:
                displayService.ShowError("Invalid option. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        displayService.ShowError(ex.Message);
    }
}

async Task ListAllContacts()
{
    var contacts = await contactService.GetAllContactsAsync();
    displayService.ShowContacts(contacts);
}

async Task SearchContacts()
{
    var searchTerm = displayService.GetSearchInput();
    var contacts = await contactService.SearchContactsAsync(searchTerm);
    displayService.ShowContacts(contacts);
}

async Task AddNewContact()
{
    var contact = displayService.GetContactInput();
    await contactService.AddContactAsync(contact);
    displayService.ShowMessage("Contact added successfully!");
}

async Task EditContact()
{
    var id = displayService.GetContactIdInput();
    var contact = await contactService.GetContactByIdAsync(id);
    
    if (contact == null)
    {
        displayService.ShowError("Contact not found.");
        return;
    }

    displayService.ShowContact(contact);
    Console.WriteLine("\nEnter new values (leave blank to keep current):");
    
    Console.Write($"First Name [{contact.FirstName}]: ");
    var firstName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(firstName)) contact.FirstName = firstName;
    
    Console.Write($"Last Name [{contact.LastName}]: ");
    var lastName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(lastName)) contact.LastName = lastName;
    
    Console.Write($"Email [{contact.Email}]: ");
    var email = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(email)) contact.Email = email;
    
    Console.Write($"Phone [{contact.Phone}]: ");
    var phone = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(phone)) contact.Phone = phone;
    
    Console.Write($"Company [{contact.Company}]: ");
    var company = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(company)) contact.Company = company;
    
    Console.Write($"Notes [{contact.Notes}]: ");
    var notes = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(notes)) contact.Notes = notes;

    await contactService.UpdateContactAsync(contact);
    displayService.ShowMessage("Contact updated successfully!");
}

async Task DeleteContact()
{
    var id = displayService.GetContactIdInput();
    var contact = await contactService.GetContactByIdAsync(id);
    
    if (contact == null)
    {
        displayService.ShowError("Contact not found.");
        return;
    }

    displayService.ShowContact(contact);
    Console.Write("\nAre you sure you want to delete this contact? (y/n): ");
    var confirm = Console.ReadLine();
    
    if (confirm?.ToLower() == "y")
    {
        await contactService.DeleteContactAsync(id);
        displayService.ShowMessage("Contact deleted successfully!");
    }
}

async Task ManageTags()
{
    Console.WriteLine("\n=== Manage Tags ===");
    Console.WriteLine("1. List all tags");
    Console.WriteLine("2. Add tag to contact");
    Console.WriteLine("3. Remove tag from contact");
    Console.WriteLine("4. Create new tag");
    Console.WriteLine("5. Delete tag");
    Console.Write("\nSelect option: ");
    
    var choice = Console.ReadLine();
    
    switch (choice)
    {
        case "1":
            var tags = await tagService.GetAllTagsAsync();
            displayService.ShowTags(tags);
            break;
        case "2":
            await AddTagToContact();
            break;
        case "3":
            await RemoveTagFromContact();
            break;
        case "4":
            await CreateNewTag();
            break;
        case "5":
            await DeleteTag();
            break;
    }
}

async Task AddTagToContact()
{
    var contactId = displayService.GetContactIdInput();
    var contact = await contactService.GetContactByIdAsync(contactId);
    
    if (contact == null)
    {
        displayService.ShowError("Contact not found.");
        return;
    }

    var tags = await tagService.GetAllTagsAsync();
    displayService.ShowTags(tags);
    
    Console.Write("\nEnter tag ID: ");
    var tagId = int.Parse(Console.ReadLine() ?? "0");
    
    await tagService.AddTagToContactAsync(contactId, tagId);
    displayService.ShowMessage("Tag added to contact!");
}

async Task RemoveTagFromContact()
{
    var contactId = displayService.GetContactIdInput();
    var contact = await contactService.GetContactByIdAsync(contactId);
    
    if (contact == null)
    {
        displayService.ShowError("Contact not found.");
        return;
    }

    displayService.ShowContact(contact);
    
    Console.Write("\nEnter tag ID to remove: ");
    var tagId = int.Parse(Console.ReadLine() ?? "0");
    
    await tagService.RemoveTagFromContactAsync(contactId, tagId);
    displayService.ShowMessage("Tag removed from contact!");
}

async Task CreateNewTag()
{
    Console.Write("\nEnter tag name: ");
    var name = Console.ReadLine() ?? string.Empty;
    
    var tag = new ContactsManager.Console.Models.Tag { Name = name };
    await tagService.AddTagAsync(tag);
    displayService.ShowMessage("Tag created successfully!");
}

async Task DeleteTag()
{
    var tags = await tagService.GetAllTagsAsync();
    displayService.ShowTags(tags);
    
    Console.Write("\nEnter tag ID to delete: ");
    var tagId = int.Parse(Console.ReadLine() ?? "0");
    
    await tagService.DeleteTagAsync(tagId);
    displayService.ShowMessage("Tag deleted successfully!");
}

async Task ExportContacts()
{
    var contacts = await contactService.GetAllContactsAsync();
    var filePath = displayService.GetExportPathInput();
    await exportService.ExportToCsvAsync(contacts, filePath);
    displayService.ShowMessage($"Contacts exported to {filePath}");
}
