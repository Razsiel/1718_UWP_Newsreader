using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Services;
using Library.Model;

namespace Final_excersise.Models
{
    public class Settings : BindableBase
    {
        private string _authToken;
        private string _username;

        public static Settings SingleInstance { get; } = new Settings();

        public string AuthToken
        {
            get { return _authToken; }
            set
            {
                SetProperty(ref _authToken, value);
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AuthToken);
            }
        }

        private Settings()
        {
            
        }
    }
}
