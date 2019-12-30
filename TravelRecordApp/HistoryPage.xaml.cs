using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Model.TravelPost> travelTable = new List<Model.TravelPost>();

            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            {
                // if a table already exists, this call is just ignored
                connection.CreateTable<Model.TravelPost>();
                travelTable = connection.Table<Model.TravelPost>().ToList();
            }

            travelPostListView.ItemsSource = travelTable;
        }

        private void travelPostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = travelPostListView.SelectedItem as Model.TravelPost;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}