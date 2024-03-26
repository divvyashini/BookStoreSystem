using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreSystem.Models;
using OnlineBookStoreSystem.Services;
using System.Globalization;

namespace OnlineBookStoreSystem.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService<Post> _bookService;

        public BooksController(IBookService<Post> bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("api/[controller]/posts")]
        public async Task<IActionResult> GetPostsByTags([FromQuery] List<string> tags, [FromQuery] string sortBy, [FromQuery] string direction)
        {
            try
            {
                var posts = await _bookService.GetAllPostsAsync(tags, sortBy, direction);
                if(posts == null)
                    return NotFound();
                return Ok(posts);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching postd: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching posts.");
            }
        }
    }
}
