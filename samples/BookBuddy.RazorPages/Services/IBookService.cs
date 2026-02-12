using BookBuddy.RazorPages.Models;

namespace BookBuddy.RazorPages.Services;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<List<Book>> GetBooksByStatusAsync(ReadingStatus status);
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
    Task<List<Book>> SearchBooksAsync(string searchTerm);
}
