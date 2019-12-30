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
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        private void ToolbarSave_Clicked(object sender, EventArgs e)
        {
            TravelRecordApp.Model.TravelPost newPost = new Model.TravelPost()
            {
                TravelExperience = Entry_Experience.Text
            };

            int addedRows = 0;
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.DatabaseLocation))
            {
                // if a table already exists, this call is just ignored
                connection.CreateTable<TravelRecordApp.Model.TravelPost>();
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
    }
}