using System.Text;
using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public class ExportService : IExportService
{
    public async Task ExportToCsvAsync(IEnumerable<Contact> contacts, string filePath)
    {
        var csv = new StringBuilder();
        csv.AppendLine("FirstName,LastName,Email,Phone,Company,Notes,CreatedAt,LastContactedAt,Tags");

        foreach (var contact in contacts)
        {
            var tags = string.Join("; ", contact.ContactTags.Select(ct => ct.Tag.Name));
            csv.AppendLine($"\"{contact.FirstName}\",\"{contact.LastName}\",\"{contact.Email}\",\"{contact.Phone}\",\"{contact.Company}\",\"{contact.Notes}\",\"{contact.CreatedAt:yyyy-MM-dd}\",\"{contact.LastContactedAt:yyyy-MM-dd}\",\"{tags}\"");
        }

        await File.WriteAllTextAsync(filePath, csv.ToString());
    }
}
