using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Services;

namespace Final_excersise.Models
{
    public class Settings
    {
        public static Settings SingleInstance { get; } = new Settings();

        private readonly IFeedService _feedService;

        private Settings()
        {
            _feedService = FeedService.SingleInstance;
        }

        public List<Feed> Feeds { get; set; }

        public string AuthToken { get; set; }

        public string Username { get; set; }

        public async Task Init()
        {
            Feeds = await _feedService.GetFeeds();
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AuthToken);
            }
        } 
    }
}
