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
            var feeds = _feedService.GetFeeds().Result;
            Feeds = new List<SettingFeed>();
        }

        public List<SettingFeed> Feeds { get; set; }
    }

    public class SettingFeed
    {
        public bool IsChecked { get; set; } = true;
        public string Name { get; set; } = "";
    }
}
