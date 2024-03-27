using OnlineBookStoreSystem.Models;
using System.Globalization;

namespace OnlineBookStoreSystem.Services
{
    /// <summary>
    /// Service class which implements the interface and communicates with the API through HTTP requests and
    /// receives the http responses
    /// </summary>
    public class BookServiceManager<T> : IBookService<T>

    {
        /// <summary>
        /// Usage of HttpClient class to send http requests and receive http responses
        /// </summary>
        private readonly BookServiceApiClient _bookServiceApiClient;


        /// <summary>
        /// Initializes a new instance of the BookServiceManager class
        /// </summary>
        /// <param name="bookServiceApiClient"> The HTTP client to communicate with the API</param>
        public BookServiceManager(BookServiceApiClient bookServiceApiClient)
        {
           _bookServiceApiClient = bookServiceApiClient;
        }

        /// <summary>
        /// Retrieves all posts asynchronously based on input tags, sortBy field and direction of sorting
        /// </summary>
        /// <param name="tags">List of tags to filter the posts</param>
        /// <param name="sortBy">Field to sort the posts</param>
        /// <param name="direction">Direction of sorting (ascending/descending)</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<T>> GetPostsByTagsAsync(List<string> tags, string sortBy, string direction)
        {
            try
            {
                var posts = await _bookServiceApiClient.GetPostsByTagsAsync(tags, sortBy, direction);
                return posts.Cast<T>().ToList();
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while fetching posts:{ex.Message}");
            }
        }

    

    }
}
