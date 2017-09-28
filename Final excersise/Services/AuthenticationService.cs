using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Final_excersise.Models;
using Final_excersise.Services.Results;

namespace Final_excersise.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public static AuthenticationService SingleInstance { get; } = new AuthenticationService();

        private static Settings Settings => Settings.SingleInstance;

        private AuthenticationService()
        {
        }

        protected override string ServiceUri => "api/users";

        public async Task<bool> Login(string username, string password)
        {
            var uri = $"{ServiceUri}/login";
            var parameters = new Dictionary<string, string>
            {
                {"UserName", username},
                {"Password", password}
            };
            var result = await GetJsonResultAsync<LoginResult>(uri, HttpMethod.Post, parameters);
            // Check to see if we got an authentication code back
            var succes = !string.IsNullOrWhiteSpace(result?.AuthToken);
            if (succes)
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                if (!vault.RetrieveAll().Any())
                {
                    var credentails = new Windows.Security.Credentials.PasswordCredential(Package.Current.DisplayName, username, password);
                    vault.Add(credentails);
                }
                Settings.AuthToken = result.AuthToken;
                Settings.Username = username;
            }
            return succes;
        }

        public void LogOut()
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            var appCredentials = vault.RetrieveAll();
            // Remove login data for this app
            foreach (var credential in appCredentials)
            {
                vault.Remove(credential);
            }
            Settings.AuthToken = null;
            Settings.Username = null;
        }

        public void Register()
        {
            Debug.WriteLine("register called");
        }

        public async Task<bool> TryAutoLogin()
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            var credentials = vault.RetrieveAll();
            if (credentials.Any())
            {
                var user = credentials.FirstOrDefault();
                if (user != null)
                {
                    user.RetrievePassword();
                    return await Login(user.UserName, user.Password);
                }
            }
            return false;
        }
    }

    public interface IAuthenticationService
    {
        Task<bool> Login(string username, string password);
        Task<bool> TryAutoLogin();
        void LogOut();
        void Register();
    }
}
