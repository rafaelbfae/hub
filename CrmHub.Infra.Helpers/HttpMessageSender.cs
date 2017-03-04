using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CrmHub.Infra.Helpers.Interfaces;

namespace CrmHub.Infra.Helpers
{
    public class HttpMessageSender : IHttpMessageSender
    {
        public static bool SendRequest(dynamic crm, Func<object, bool> function)
        {
            SendRequest(string.Empty, function).Wait();
            return true;
        }

        public static async Task SendRequest(string url, Func<object, bool> function)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(url);
                    var response = await client.GetAsync("getRecords");
                    response.EnsureSuccessStatusCode();
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject(stringResponse);

                    function(result);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }

        #region Public Methods

        public bool SendRequestGet(string url, object value, Func<string, object, bool> loadResponse)
        {
            return SendRequestGetAsync(url, value, loadResponse).Result;
        }

        public bool SendRequestPost(string url, string content, object value, Func<string, object, bool> getResponse)
        {
            return SendRequestPostAsync(url, content, value, getResponse).Result;
        }

        public bool SendRequestDelete(string url, object value, Func<string, object, bool> loadResponse)
        {
            return SendRequestDeleteAsync(url, value, loadResponse).Result;
        }

        #endregion

        #region Protected Methods

        protected async Task<bool> SendRequestGetAsync(string url, object value, Func<string, object, bool> loadResponse)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                return loadResponse(await response.Content.ReadAsStringAsync(), value);
            }
        }

        protected async Task<bool> SendRequestDeleteAsync(string url, object value, Func<string, object, bool> loadResponse)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                return loadResponse(await response.Content.ReadAsStringAsync(), value);
            }
        }

        #endregion

        #region Private Methods

        private async Task<bool> SendRequestPostAsync(string url, string content, object value, Func<string, object, bool> getResponse)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent xmlQuery = new StringContent(content);
                var response = await httpClient.PostAsync(new Uri(url), xmlQuery);
                response.EnsureSuccessStatusCode();
                return getResponse(await response.Content.ReadAsStringAsync(), value);
            }
        }

        #endregion
    }
}
