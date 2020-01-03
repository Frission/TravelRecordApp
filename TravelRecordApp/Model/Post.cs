using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Unique]
        public string Id { get; set; }

        [SQLite.MaxLength(500)]
        public string TravelExperience { get; set; }

        public string VenueName { get; set; }
        
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }

        public static async Task Insert(Post post)
        {
            await App.MobileClient.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> GetUserPosts()
        {
            var posts = await App.MobileClient.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();
            return posts;
        }

        public static async Task UpdatePost(Post post)
        {
            await App.MobileClient.GetTable<Post>().UpdateAsync(post);
        }

        public static async Task DeletePost(Post post)
        {
            await App.MobileClient.GetTable<Post>().DeleteAsync(post);
        }

        public static Dictionary<string, int> GetCategories(List<Post> posts)
        {
            var categories = (from post in posts
                              orderby post.CategoryId
                              select post.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            categories.ForEach(category =>
            {
                var count = (from post in postTable where post.CategoryName == category select post).Count();

                categoriesCount.Add(category, count);
            });

            return categoriesCount;
        }
    }
}
