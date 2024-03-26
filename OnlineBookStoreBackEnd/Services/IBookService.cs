using OnlineBookStoreSystem.Models;

namespace OnlineBookStoreSystem.Services
{
    /// <summary>
    /// Generic interface which contains common methods which can be reused for 
    /// services within the application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBookService<T> 
    {
        Task<List<T>> GetAllPostsAsync(List<string> tags, string sortBy,string direction);
    }
}
