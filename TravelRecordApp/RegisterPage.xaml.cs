using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            var assembly = typeof(RegisterPage);

            LoginPageLogo.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if(PasswordEntry.Text == PasswordConfirmEntry.Text)
            {              
                var existingUser = Users.FindUser(EmailEntry.Text);

                if(existingUser != null)
                {
                    await DisplayAlert("Error", ApplicationErrors.GetError(ErrorCode.UserExists).Message, "OK");
                    return;
                }

                await Users.InsertUser(EmailEntry.Text, PasswordEntry.Text);
                await DisplayAlert("Success", "You have successfully registered!", "OK");
            }
            else
            {
                await DisplayAlert("Error", ApplicationErrors.GetError(ErrorCode.PasswordMismatch).Message, "OK");
            }
        }
    }
}