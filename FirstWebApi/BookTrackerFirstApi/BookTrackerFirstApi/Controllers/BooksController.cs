using BookTrackerFirstApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookTrackerFirstApi.Data;
using BookTrackerFirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MasterContext _context;
        
        public BooksController(MasterContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound(); // return 404 status (Not Found)
            }

            return book;
        }


        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddBook),  new {id = book.Id }, book); // return 201 status (Created)

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if(book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            var found = await _context.Books.FindAsync(id);
            if(found == null)
            {
                return NotFound();
            }

            found.Name = book.Name;
            found.Author = book.Author;
            found.Description = book.Description;
            found.IsRead = book.IsRead;
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
