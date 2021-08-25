using System;
using KiteLibrary;
using KiteLibrary.Models;
using System.Threading.Tasks;

namespace KiteConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TokenGenerator tokenGenerator = new TokenGenerator();
            TokenResponse response = await tokenGenerator.LoginAndGetAccessTokenAsync(new TokenRequest
            {
                ApiKey = "eguog2vt2umybd0p",
                UserId = "TV7485",
                Password = "Alpha123",
                Pin = "123456",
                ApiSecret = "ym51tkbb2rbos0rqyui3c1abewdps81q"
            });

            Console.WriteLine(response.Data.AccessToken);
        }
    }
}
