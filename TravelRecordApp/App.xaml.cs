using Microsoft.WindowsAzure.MobileServices;
using System;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient MobileClient = new MobileServiceClient("https://travelrecordapptest12.azurewebsites.net");

        public static Users User;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.FromHex("1F8B00"), BarTextColor = Color.FromHex("F8F8FF") };
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.FromHex("1F8B00"), BarTextColor = Color.FromHex("F8F8FF") };

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
