using MongodbApi.Models;
using MongodbApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MongodbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Book> books = await _bookService.GetAll();
         
            return Ok(books);
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public async Task<IActionResult> Get(string id)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            await _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book bookIn)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.Remove(book.Id);

            return NoContent();
        }

        [HttpGet("{offset}/{limit}")]
        public async Task<IActionResult> GetAllPaginated(int offset, int limit) {
            try {
                BookPaginatedList result = await _bookService.GetAllPaginated(offset, limit);
                return Ok(result);
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}