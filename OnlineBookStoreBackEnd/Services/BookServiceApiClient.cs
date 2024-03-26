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
        public BookServiceApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task<List<Post>> GetPostsByTags(List <string> tags, string sortBy,string direction)
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
                        Console.WriteLine($"Failed to fetch posts for tag '{tag}'.Status Code : {apiResponse.StatusCode}");
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

        private void SortPosts(List<Post>posts,string sortBy, string direction)
        {
           if(string.IsNullOrEmpty(sortBy) || !IsSortByFieldValid(sortBy))
            {
                sortBy = "id";
            }

            switch (sortBy.ToLower())
            {
                case "reads":
                    posts.Sort((post1,post2)=> direction.ToLower() == "asc" ? post1.Reads.CompareTo(post2.Reads):post2.Reads.CompareTo(post1.Reads)); 
                    break;
                case "likes":
                    posts.Sort((post1, post2) => direction.ToLower() == "asc" ? post1.Likes.CompareTo(post2.Likes) : post2.Likes.CompareTo(post1.Likes));
                    break;
                case "popularity":
                    posts.Sort((post1, post2) => direction.ToLower() == "asc" ? post1.Popularity.CompareTo(post2.Popularity) : post2.Popularity.CompareTo(post1.Popularity));
                    break;
                default:
                    posts.Sort((post1, post2) => direction.ToLower() == "asc" ? post1.Id.CompareTo(post2.Id) : post2.Id.CompareTo(post1.Id));
                    break;
            }
        }

        private bool IsSortByFieldValid(string sortBy)
        {
            string[] validSortByField = { "id", "reads", "likes", "popularity" };
            return validSortByField.Contains(sortBy.ToLower());
        } 
        private class PostApiResponse
        {
            public List<Post> Posts { get; set; }
        }
    }
}  
