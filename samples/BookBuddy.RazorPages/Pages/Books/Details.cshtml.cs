using BookBuddy.RazorPages.Models;
using BookBuddy.RazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBuddy.RazorPages.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly IBookService _bookService;

    public DetailsModel(IBookService bookService)
    {
        _bookService = bookService;
    }

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
}
