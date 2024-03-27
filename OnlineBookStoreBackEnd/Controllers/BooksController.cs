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

        /// <summary>
        /// Initializes a new instance of BooksController
        /// </summary>
        /// <param name="bookService">IBookService interface</param>
        public BooksController(IBookService<Post> bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Retrieves posts having atleast one tag specified in the tags list
        /// Sorts the retrieved posts by the sorting field and the direction specified
        /// </summary>
        /// <param name="tags">List of tags to filter posts</param>
        /// <param name="sortBy">Sorting field</param>
        /// <param name="direction">Sorting direction</param>
        /// <returns> The action result which contains the filtered and sorted posts</returns>
        /// <exception> Error message if fetching posts fails</exception>
        [HttpGet]
        [Route("api/[controller]/posts")]
        public async Task<IActionResult> GetPostsByTags([FromQuery] List<string> tags, 
            [FromQuery] string? sortBy, [FromQuery] string? direction)
        {
            try
            {
                var posts = await _bookService.GetPostsByTagsAsync(tags, sortBy, direction);
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
