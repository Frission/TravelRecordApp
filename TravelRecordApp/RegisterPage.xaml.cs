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
        Users user;

        public RegisterPage()
        {
            InitializeComponent();

            var assembly = typeof(RegisterPage);

            LoginPageLogo.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);

            user = new Users();
            ContainerStackLayout.BindingContext = user;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if(PasswordEntry.Text == PasswordConfirmEntry.Text)
            {                           
                var result = await Users.RegisterUser(user);

                if(result.Success == false)
                {
                    switch (result.Error)
                    {
                        case ErrorCode.UserExists:
                            await DisplayAlert("Error", ApplicationErrors.GetError(result.Error).Message, "OK");
                            return;
                    }
                }

                await DisplayAlert("Success", "You have successfully registered!", "OK");
            }
            else
            {
                await DisplayAlert("Error", ApplicationErrors.GetError(ErrorCode.PasswordMismatch).Message, "OK");
            }
        }
    }
}