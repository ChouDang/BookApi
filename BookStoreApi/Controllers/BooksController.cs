using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService) => _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> Get() => await _booksService.GetBooksAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetBooksAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _booksService.CreaterBook(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Book newBook)
        {
            var book = await _booksService.GetBooksAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            newBook.Id = book.Id;
            await _booksService.UpdateBook(id,newBook);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Del(string id)
        {
            var book = await _booksService.GetBooksAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _booksService.DeleteBook(id);
            return NoContent();    
        }
    }
}
