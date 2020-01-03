using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    

    // not named User because User is a reserved word in SQL (wtf?)
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<Users> FindUser(string email)
        {
            return (await App.MobileClient.GetTable<Users>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
        }

        public static async Task InsertUser(string email, string password)
        {
            Users user = new Users()
            {
                Email = email,
                Password = password
            };

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
}
