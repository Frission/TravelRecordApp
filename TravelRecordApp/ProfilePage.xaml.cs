using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //using(SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            //{
            //var postTable = connection.Table<Post>().ToList();
            var posts = await Post.GetUserPosts();

            Dictionary<string, int> categoriesCount = Post.GetCategories(posts);

            CategoriesListView.ItemsSource = categoriesCount;

            PostCountLabel.Text = posts.Count.ToString();
            //}
        }
    }
}