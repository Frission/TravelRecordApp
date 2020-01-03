using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Post> travelTable = new List<Post>();

            //using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            //{
            //    // if a table already exists, this call is just ignored
            //    connection.CreateTable<Model.Post>();
            //    travelTable = connection.Table<Model.Post>().ToList();
            //}

           travelTable = await Post.GetUserPosts();

            travelPostListView.ItemsSource = travelTable;

            if (travelTable.Count > 0)
            {
                travelPostListView.IsVisible = true;
                NoPostsLabel.IsVisible = false;
            }
            else
            {
                travelPostListView.IsVisible = false;
                NoPostsLabel.IsVisible = true;
            }
        }

        private void travelPostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = travelPostListView.SelectedItem as Model.Post;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}