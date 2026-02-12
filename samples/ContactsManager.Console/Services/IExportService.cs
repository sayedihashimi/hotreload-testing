using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public interface IExportService
{
    Task ExportToCsvAsync(IEnumerable<Contact> contacts, string filePath);
}
