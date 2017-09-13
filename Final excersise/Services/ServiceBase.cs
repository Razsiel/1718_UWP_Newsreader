using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Final_excersise.Services
{
    public abstract class ServiceBase
    {
        protected abstract string ServiceUri { get; }

        protected readonly HttpClient Client;

        protected ServiceBase()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("http://inhollandbackend.azurewebsites.net")
            };
        }

        protected async Task<T> GetJsonResultAsync<T>(string uri)
        {
            var response = await Client.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
