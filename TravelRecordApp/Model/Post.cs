using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        #region Database Table Definition with OnPropertyChanged Events
        //[SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Unique]
        private string _id;
        private string _travelExperience;
        private string _venueName;
        private string _categoryId;
        private string _categoryName;
        private string _address;
        private double _latitude;
        private double _longitude;
        private int _distance;
        private string _userId;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        //[SQLite.MaxLength(500)]
        public string TravelExperience
        {
            get => _travelExperience;
            set
            {
                _travelExperience = value;
                OnPropertyChanged("TravelExperience");
            }
        }

        public string VenueName
        {
            get => _venueName;
            set
            {
                _venueName = VenueName;
                OnPropertyChanged("VenueName");
            }
        }

        public string CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }

        public string UserId 
        { 
            get => _userId; 
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        #endregion

        #region View Model
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Azure Cloud Operations
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
        #endregion

        #region Helper Functions

        public static Post CreatePost(string experience, Venue selectedVenue)
        {
            var firstCategory = selectedVenue.categories.FirstOrDefault();

            Post post = new Post
            {
                TravelExperience = experience,
                VenueName = selectedVenue.name,
                CategoryId = firstCategory.id,
                CategoryName = firstCategory.name,
                Address = selectedVenue.location.address,
                Latitude = selectedVenue.location.lat,
                Longitude = selectedVenue.location.lng,
                Distance = selectedVenue.location.distance,
                UserId = App.User.Id
            };

            return post;
        }

        public static Dictionary<string, int> GetCategories(List<Post> posts)
        {
            var categories = (from post in posts
                              orderby post.CategoryId
                              select post.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            categories.ForEach(category =>
            {
                var count = (from post in posts where post.CategoryName == category select post).Count();

                categoriesCount.Add(category, count);
            });

            return categoriesCount;
        }
        #endregion
    }
}

#region Old SQL Implementation
// insert post
//
//int addedRows = 0;
//using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
//{
//    // if a table already exists, this call is just ignored
//    connection.CreateTable<Model.Post>();
//    addedRows = connection.Insert(newPost);
//}

//if (addedRows > 0)
//{
//    DisplayAlert("Success", "Experience successfully added!", "OK");
//}
//else
//{
//    DisplayAlert("Failure", "Experience could not be added :(", "OK");
//}
#endregion
