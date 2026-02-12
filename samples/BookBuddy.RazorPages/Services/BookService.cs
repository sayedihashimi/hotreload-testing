using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.RazorPages.Services;

public class BookService : IBookService
{
    private readonly BookBuddyContext _context;

    public BookService(BookBuddyContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.ReadingSessions)
            .OrderByDescending(b => b.DateAdded)
            .ToListAsync();
    }

    public async Task<List<Book>> GetBooksByStatusAsync(ReadingStatus status)
    {
        return await _context.Books
            .Include(b => b.ReadingSessions)
            .Where(b => b.Status == status)
            .OrderByDescending(b => b.DateAdded)
            .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.ReadingSessions)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        book.DateAdded = DateTime.Now;
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook == null)
            return null;

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.ISBN = book.ISBN;
        existingBook.PageCount = book.PageCount;
        existingBook.Genre = book.Genre;
        existingBook.CoverImageUrl = book.CoverImageUrl;
        existingBook.Status = book.Status;
        existingBook.CurrentPage = book.CurrentPage;
        existingBook.StartedDate = book.StartedDate;
        existingBook.FinishedDate = book.FinishedDate;
        existingBook.Rating = book.Rating;
        existingBook.Notes = book.Notes;

        await _context.SaveChangesAsync();
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Book>> SearchBooksAsync(string searchTerm)
    {
        return await _context.Books
            .Include(b => b.ReadingSessions)
            .Where(b => b.Title.Contains(searchTerm) || 
                        b.Author.Contains(searchTerm) || 
                        b.Genre.Contains(searchTerm))
            .OrderByDescending(b => b.DateAdded)
            .ToListAsync();
    }
}
