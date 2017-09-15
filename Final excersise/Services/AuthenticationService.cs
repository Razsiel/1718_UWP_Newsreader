using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var result = await GetJsonResultAsync<LoginResult>(uri);
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            // Check to see if we got an authentication code back
            var succes = !string.IsNullOrWhiteSpace(result.AuthToken);
            if (succes)
            {
                roamingSettings.Values["authToken"] = result.AuthToken;
            }
            return succes;
        }

        public void Register()
        {
            Debug.WriteLine("register called");
        }
    }

    public interface IAuthenticationService
    {
        Task<bool> Login(string userName, string password);
        void Register();
    }
}
