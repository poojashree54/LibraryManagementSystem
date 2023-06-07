using BLL.DTOs;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookServices bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("GetBooks")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetAllBooks()
        {
            try
            {
                var Books = _bookService.GetBooks();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Books Fetched.");
                return Ok(Books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving Books.");
                return StatusCode(500);
            }
        }

        [HttpGet("GetBookById")]
        public IActionResult SearchBook(int id)
        {
            try
            {
                var book = _bookService.SearchBook(id);
                if (book == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Book is fetched by ID");
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a Book by ID.");
                return StatusCode(500);
            }
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(BookDTO bookDto)
        {
            try
            {
                if (bookDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!_bookService.AddBook(bookDto))
                {
                    ModelState.AddModelError("", "Book is not Added [CONTOLLER]");
                    return StatusCode(500, ModelState);
                }
                _logger.LogInformation("Book is Added");
                return Ok("Book Successfully Added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Adding a Book.");
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateBook")]
        public IActionResult Updatebook(int id, BookDTO bookDto)
        {
            try
            {
                if (id != bookDto.BookId)
                {
                    return BadRequest();
                }

                _bookService.UpdateBook(id, bookDto);
                _logger.LogInformation("Book is Created");

                return Ok("Book Successfully Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a Bus.");
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = _bookService.SearchBook(id);

                if (book == null)
                {
                    return NotFound();
                }
                _bookService.DeleteBook(id);
                _logger.LogInformation("Book is Deleted");

                return Ok("Book Successfully Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a Book.");
                return StatusCode(500);
            }
        }
    }
}
