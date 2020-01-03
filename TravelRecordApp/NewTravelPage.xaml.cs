using Plugin.Geolocator;
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
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenuesSorted(position.Latitude, position.Longitude);
            
            VenueListView.ItemsSource = venues;
        }

        private async void ToolbarSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = VenueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Post newPost = new Post()
                {
                    TravelExperience = Entry_Experience.Text,
                    VenueName = selectedVenue.name,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    Distance = selectedVenue.location.distance,
                    UserId = App.User.Id
                };

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

                await Post.Insert(newPost);
                await DisplayAlert("Success", "Experience successfully added!", "OK");
            }
            catch(NullReferenceException)
            {
                await DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
        }
    }
}