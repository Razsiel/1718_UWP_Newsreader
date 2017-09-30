using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Final_excersise.Services
{
    public abstract class ServiceBase
    {
        protected string BaseAddress;

        protected abstract string ServiceUri { get; }

        protected readonly HttpClient Client;

        protected ServiceBase()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("http://inhollandbackend.azurewebsites.net")
            };
        }

        /// <summary>
        /// Gets a response object from a given uri
        /// </summary>
        /// <typeparam name="T">The type to desrialize into</typeparam>
        /// <param name="uri">The relative uri from the api base</param>
        /// <param name="httpMethod">The HttpMethod. Default: GET</param>
        /// <param name="parameters">OPTIONAL: Any uri paramters</param>
        /// <param name="xauthtoken">OPTIONAL: The authentication token provided by the backend after logging in</param>
        /// <param name="failedCallback">OPTIONAL: A callback that is triggered on failure of the request or serialisation</param>
        /// <returns></returns>
        protected async Task<T> GetJsonResultAsync<T>(string uri, HttpMethod httpMethod = null, Dictionary<string, string> parameters = null, string xauthtoken = null)
        {
            if (httpMethod == null) httpMethod = HttpMethod.Get;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://inhollandbackend.azurewebsites.net/");
                try
                {
                    if (parameters == null) parameters = new Dictionary<string, string>();
                    using (var content = new FormUrlEncodedContent(parameters))
                    {
                        using (var request = new HttpRequestMessage(httpMethod, uri))
                        {
                            request.Content = content;
                            if (!string.IsNullOrWhiteSpace(xauthtoken))
                            {
                                request.Headers.Add("x-authtoken", xauthtoken);
                            }

                            using (var response = await client.SendAsync(request))
                            {
                                var json = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<T>(json);
                                return result;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
