using ContactsManager.Console.Models;

namespace ContactsManager.Console.Services;

public interface IDisplayService
{
    void ShowMenu();
    void ShowContacts(IEnumerable<Contact> contacts);
    void ShowContact(Contact contact);
    void ShowTags(IEnumerable<Tag> tags);
    void ShowMessage(string message);
    void ShowError(string error);
    Contact GetContactInput();
    string GetSearchInput();
    int GetContactIdInput();
    string GetExportPathInput();
}
