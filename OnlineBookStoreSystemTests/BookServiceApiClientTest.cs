using Microsoft.Extensions.Configuration;
using Moq;
using OnlineBookStoreSystem.Models;
using OnlineBookStoreSystem.Services;
using System.Net;
using System.Text.Json;

namespace OnlineBookStoreSystemTests
{
    [TestFixture]
    public class BookServiceApiClientTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetPostsByTagsAsync_IsSuccessful()
        {
            //Arrange
            var _configurationMock = new Mock<IConfiguration>();
            var mockHttpClient = new Mock<HttpClient>();
            var tags = new List<string> { "tech", "health" };
            var sortBy = "popularity";
            var direction = "asc";

            var postApiResponse = new PostApiResponse
            {
                Posts = new List<Post>
                {
                    new Post {Id = 1, Author = "Rylee Paul",AuthorId = 9,Likes = 960,Reads = 50361,Popularity = 0.13M,Tags = new List<string> { "health", "tech" }},
                    new Post {Id = 2, Author = "Ahmad Dunn",AuthorId = 7,Likes = 100,Reads = 361,Popularity = 6.13M,Tags = new List<string> { "startup", "science" }}
                }
            };

            var apiResponse = JsonSerializer.Serialize(postApiResponse);
            var responseContent = new StringContent(apiResponse);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = responseContent };

            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);
            var apiClient = new BookServiceApiClient(mockHttpClient.Object, _configurationMock.Object);

            //Act

            var result = await apiClient.GetPostsByTagsAsync(tags, sortBy, direction);

            //Assert

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);
        }



    }
}