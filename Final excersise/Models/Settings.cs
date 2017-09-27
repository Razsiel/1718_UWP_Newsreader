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
        public static Settings SingleInstance { get; } = new Settings();

        private Settings()
        {
            
        }

        public List<Feed> Feeds { get; set; }

        public string AuthToken { get; set; }
        public string Username { get; set; }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AuthToken);
            }
        } 
    }
}
