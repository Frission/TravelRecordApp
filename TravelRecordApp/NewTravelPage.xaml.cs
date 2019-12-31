using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Managers;
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

            var venues = await VenueManager.GetVenues(position.Latitude, position.Longitude);

            venues.Sort(delegate (Model.Venue venue1, Model.Venue venue2)
            {
                if (venue1.location.distance > venue2.location.distance)
                    return 1;
                else if (venue1.location.distance == venue2.location.distance)
                    return 0;
                else
                    return -1;
            });
            VenueListView.ItemsSource = venues;
        }

        private void ToolbarSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = VenueListView.SelectedItem as Model.Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Model.Post newPost = new Model.Post()
                {
                    TravelExperience = Entry_Experience.Text,
                    VenueName = selectedVenue.name,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    Distance = selectedVenue.location.distance
                };

                int addedRows = 0;
                using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
                {
                    // if a table already exists, this call is just ignored
                    connection.CreateTable<Model.Post>();
                    addedRows = connection.Insert(newPost);
                }

                if (addedRows > 0)
                {
                    DisplayAlert("Success", "Experience successfully added!", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience could not be added :(", "OK");
                }
            }
            catch(NullReferenceException nullEx)
            {
                DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
            catch(Exception ex)
            {
                DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
        }
    }
}