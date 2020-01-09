using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{


    // not named User because User is a reserved word in SQL (wtf?)
    public class Users : INotifyPropertyChanged
    {
        #region Database Table Definition with OnPropertyChanged Events
        private string _id;
        private string _email;
        private string _password;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        
        public string Password 
        { 
            get => _password; 
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region View Model
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Azure Cloud Operations
        public static async Task<Users> FindUser(string email)
        {
            return (await App.MobileClient.GetTable<Users>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
        }

        public static async Task<CloudOperationResult> RegisterUser(Users user)
        {
            CloudOperationResult result = new CloudOperationResult();
            result.Success = true;
            result.Error = ErrorCode.NoError;

            var existingUser = await FindUser(user.Email);

            if (existingUser != null)
            {
                result.Success = false;
                result.Error = ErrorCode.UserExists;
            }

            return result;
        }

        private static async Task InsertUser(Users user)
        {
            await App.MobileClient.GetTable<Users>().InsertAsync(user);
        }

        public static async Task<CloudOperationResult> Login(string email, string password)
        {
            CloudOperationResult result = new CloudOperationResult();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result.Success = false;
                result.Error = ErrorCode.EmailPasswordEmpty;
            }
            else
            {
                var user = await Users.FindUser(email);

                if (user != null)
                {
                    if (user.Password == password)
                    {
                        App.User = user;
                        result.Success = true;
                        result.Error = ErrorCode.NoError;
                    }
                    else
                    {
                        result.Success = false;
                        result.Error = ErrorCode.EmailPasswordIncorrect;
                    }
                }
                else
                {
                    result.Success = false;
                    result.Error = ErrorCode.ServiceUnavailable;
                }
            }

            return result;
        }
    }

    #endregion
}
