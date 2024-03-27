using OnlineBookStoreSystem.Models;

namespace OnlineBookStoreSystem.Services
{
    /// <summary>
    /// Generic interface which can include reusable methods for entities within the book store application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBookService<T> 
    {
        Task<List<T>> GetPostsByTagsAsync(List<string> tags, string sortBy,string direction);
    }
}
