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
        Post post;
        public NewTravelPage()
        {
            InitializeComponent();

            post = new Post();
            ContainerStackLayout.BindingContext = post;
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
                post = Post.CreatePost(post.TravelExperience, VenueListView.SelectedItem as Venue);

                await Post.Insert(post);
                await DisplayAlert("Success", "Experience successfully added!", "OK");
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience could not be added due to an error :(", "OK");
            }
        }
    }
}