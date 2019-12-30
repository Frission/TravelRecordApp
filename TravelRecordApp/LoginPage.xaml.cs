using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void Button_Login_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Input_Email.Text) || string.IsNullOrEmpty(Input_Password.Text))
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());              
            }
        }
    }
}
