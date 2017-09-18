using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Services.Results;

namespace Final_excersise.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public static AuthenticationService SingleInstance { get; } = new AuthenticationService();

        private AuthenticationService()
        {
        }

        protected override string ServiceUri => "api/users";

        public async Task<bool> Login(string userName, string password)
        {
            var uri = $"{ServiceUri}/login";
            var parameters = new Dictionary<string, string>();
            parameters.Add("UserName", userName);
            parameters.Add("Password", password);
            var result = await GetJsonResultAsync<LoginResult>(uri, HttpMethod.Post, parameters);
            // Check to see if we got an authentication code back
            var succes = !string.IsNullOrWhiteSpace(result.AuthToken);
            if (succes)
            {
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                roamingSettings.Values["authToken"] = result.AuthToken;
            }
            return succes;
        }

        public void LogOut()
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["authToken"] = null;
        }

        public void Register()
        {
            Debug.WriteLine("register called");
        }
    }

    public interface IAuthenticationService
    {
        Task<bool> Login(string userName, string password);
        void LogOut();
        void Register();
    }
}
