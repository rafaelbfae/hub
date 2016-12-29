using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrmHub.Infra.Helpers
{
    public class HttpMessageSender
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
    }
}
