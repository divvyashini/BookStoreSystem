using System.ComponentModel.DataAnnotations;

namespace OnlineBookStoreSystem.Models
{
    /// <summary>
    /// Model class which contains the properties for each post
    /// </summary>
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int Likes { get; set; }
        public decimal Popularity { get; set; }
        public int Reads { get; set; }
        public List<string> Tags { get; set; }
    }
}
