using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using TravelRecordApp.Model;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool _hasLocationPermission = false;
        /// <summary>
        /// Is it the first time the user is opening this section in this session?
        /// </summary>
        private bool _firstTime = true;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();

            List<Post> travelTable = new List<Post>();

            //using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            //{
            //    // if a table already exists, this call is just ignored
            //    connection.CreateTable<Model.Post>();
            //    travelTable = connection.Table<Model.Post>().ToList();
            //}

            travelTable = await Post.GetUserPosts();

            DisplayInMap(travelTable);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (_hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                if (_firstTime)
                {
                    MoveMap(position);
                    _firstTime = false;
                }
            }
        }
        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 0.2, 0.2);
            LocationsMap.MoveToRegion(span);
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address,                        
                    };

                    LocationsMap.Pins.Add(pin);
                }
                catch(NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Location Access", "We would like to access your location to show your location in the app.", "OK");
                    }

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (result.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = result[Permission.LocationWhenInUse];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    LocationsMap.IsShowingUser = true;
                    _hasLocationPermission = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location Access", "We will not be able to show your location in app without permissions.", "OK");
                    LocationsMap.IsShowingUser = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}