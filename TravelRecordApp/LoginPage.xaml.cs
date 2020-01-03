using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var assembly = typeof(LoginPage);

            LoginPageLogo.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            LoginButton.Text = "LOGGING IN...";
            LoginButton.IsEnabled = false;

            CloudOperationResult opResult = await Users.Login(EmailEntry.Text, PasswordEntry.Text);

            if(opResult.Success == false)
            {
                await DisplayAlert("Error", ApplicationErrors.GetError(opResult.Error).Message, "OK");
                LoginButton.Text = "LOG IN";
                LoginButton.IsEnabled = true;
                return;
            }

            LoginButton.Text = "LOG IN";
            LoginButton.IsEnabled = true;
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
