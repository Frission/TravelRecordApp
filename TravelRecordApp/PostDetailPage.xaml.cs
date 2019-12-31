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
    public partial class PostDetailPage : ContentPage
    {
        private Model.Post _selectedPost;
        public PostDetailPage(Model.Post selectedPost)
        {
            InitializeComponent();

            _selectedPost = selectedPost;

            ExperienceEntry.Text = _selectedPost.TravelExperience;
            LocationLabel.Text = _selectedPost.VenueName;
            AddressLabel.Text = _selectedPost.Address;
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            _selectedPost.TravelExperience = ExperienceEntry.Text;

            using(SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Model.Post>();
                int rows = connection.Update(_selectedPost);

                if(rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully updated!", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience could not be updated :(", "OK");
                }
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Model.Post>();
                int rows = connection.Delete(_selectedPost);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully deleted!", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience could not be deleted :(", "OK");
                }
            }
        }
    }
}