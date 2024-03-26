using OnlineBookStoreSystem.Models;
using System.Globalization;

namespace OnlineBookStoreSystem.Services
{
    /// <summary>
    /// Service class which implements the interface and communicates with the API through HTTP requests and
    /// receive the http responses
    /// </summary>
    public class BookServiceManager<T> : IBookService<T>

    {
        /// <summary>
        /// Usage of HttpClient class to send http requests and receive http responses
        /// </summary>
        private readonly BookServiceApiClient _bookServiceApiClient;


        /// <summary>
        /// Constructor with the HttpClient dependency injection
        /// </summary>
        /// <param name="httpClient"></param>
        public BookServiceManager(BookServiceApiClient bookServiceApiClient)
        {
           _bookServiceApiClient = bookServiceApiClient;
        }

        public async Task<List<T>> GetAllPostsAsync(List<string> tags, string sortBy, string direction)
        {
            try
            {
                var posts = await _bookServiceApiClient.GetPostsByTags(tags, sortBy, direction);
                return posts.Cast<T>().ToList();
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while fetching posts:{ex.Message}");
            }
        }

    

    }
}
