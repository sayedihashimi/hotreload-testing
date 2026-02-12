using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.Books;

public class DeleteModel : PageModel
{
    private readonly IBookService _bookService;

    public DeleteModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty]
    public Book? Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Book = await _bookService.GetBookByIdAsync(id);

        if (Book == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Book == null || Book.Id == 0)
        {
            return NotFound();
        }

        await _bookService.DeleteBookAsync(Book.Id);
        return RedirectToPage("Index");
    }
}
