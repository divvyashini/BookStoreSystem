using Microsoft.Extensions.Options;
using OnlineBookStoreSystem.Models;
using OnlineBookStoreSystem.Settings;
using System.Text.Json;

namespace OnlineBookStoreSystem.Services
{
    public class BookServiceApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        /// <summary>
        /// Initializes a new instance BookServiceApiClient class
        /// </summary>
        /// <param name="httpClient">HTTP client</param>
        /// <param name="configuration">Configuration which contains base URL for the API</param>
        public BookServiceApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        /// <summary>
        /// Retrieves posts from the API based on the input tags,sortBy field and direction
        /// </summary>
        /// <param name="tags"> The tags required to fetch and filter the posts</param>
        /// <param name="sortBy"> The field in which the posts need to be sorted</param>
        /// <param name="direction"> The order in which the posts need to be sorted (ascending/descending)</param>
        /// <returns>A list of all Posts that is sorted and that have at least one tag specified in “tags” parameter</returns>
        public async Task<List<Post>> GetPostsByTagsAsync(List <string> tags, string sortBy,string direction)
        {
            List<Post> postsData = new List<Post>();

            try
            {
                foreach (var tag in tags)
                {
                    var query = $"?tag={tag}";
                    var apiResponse = await _httpClient.GetAsync(_baseUrl + query);
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var content = await apiResponse.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var result = JsonSerializer.Deserialize<PostApiResponse>(content, options);
                        postsData = postsData.Union(result?.Posts)
                                 .GroupBy(p => p.Id)
                                 .Select(g => g.First())
                                 .ToList();
                    }
                    else
                    {
                       throw new Exception($"Failed to fetch posts for tag '{tag}'.Status Code : {apiResponse.StatusCode}");
                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while fetching posts for tag '{tags}':{ex.Message}");
            }
            
            SortPosts(postsData, sortBy, direction);
            return postsData;
        }

        /// <summary>
        /// Sorts the posts based on sortBy field and direction
        /// </summary>
        /// <param name="posts">The list of posts to be sorted</param>
        /// <param name="sortBy">The field with which the posts need to be sorted</param>
        /// <param name="direction">The order of sorting (ascending/descending)</param>
        private void SortPosts(List<Post>posts,string sortBy, string direction)
        {
            try
            {
                ValidateSortingParameters(ref sortBy, ref direction);

                //Usage of dictionary to make the sorting more efficient 
                var sortingFunctions = new Dictionary<string, Func<Post, Post, int>>
                {
                    ["reads"] = (post1, post2) => direction.ToLower() == "asc" ? post1.Reads.CompareTo(post2.Reads) : post2.Reads.CompareTo(post1.Reads),
                    ["likes"] = (post1, post2) => direction.ToLower() == "asc" ? post1.Likes.CompareTo(post2.Likes) : post2.Likes.CompareTo(post1.Likes),
                    ["popularity"] = (post1, post2) => direction.ToLower() == "asc" ? post1.Popularity.CompareTo(post2.Popularity) : post2.Popularity.CompareTo(post1.Popularity),
                    ["id"] = (post1, post2) => direction.ToLower() == "asc" ? post1.Id.CompareTo(post2.Id) : post2.Id.CompareTo(post1.Id)
                };
               
            }
            catch(Exception ex)
            {
                throw new Exception($"Error in sorting posts:{ex.Message}");
            }          
        }

        /// <summary>
        /// Validates the sorting parameter
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="direction"></param>
        private void ValidateSortingParameters(ref string sortBy, ref string direction)
        {
            if (string.IsNullOrEmpty(sortBy) || !IsSortByFieldValid(sortBy))
            {
                sortBy = "id";
            }
            if (string.IsNullOrEmpty(direction))
            {
                direction = "asc";
            }
        }
        /// <summary>
        /// Verifies if the sorting field provided is valid
        /// </summary>
        /// <param name="sortBy"> The field which will be used to sort the posts</param>
        /// <returns>Return true if sorting field is valid, else false</returns>
        private bool IsSortByFieldValid(string sortBy)
        {
            string[] validSortByField = { "id", "reads", "likes", "popularity" };
            return validSortByField.Contains(sortBy.ToLower());
        } 
      
    }
}  
