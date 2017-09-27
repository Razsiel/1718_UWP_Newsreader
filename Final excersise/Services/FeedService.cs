using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;
using Final_excersise.Services.Results;

namespace Final_excersise.Services
{
    public class FeedService : ServiceBase, IFeedService
    {
        public static FeedService SingleInstance { get; } = new FeedService();

        private FeedService()
        {
            
        }

        protected override string ServiceUri => "api/feeds";

        public async Task<List<Feed>> GetFeeds()
        {
            var uri = ServiceUri;
            var response = await GetJsonResultAsync<List<Feed>>(uri);
            return response;
        }
    }

    public interface IFeedService
    {
        Task<List<Feed>> GetFeeds();
    }
}
