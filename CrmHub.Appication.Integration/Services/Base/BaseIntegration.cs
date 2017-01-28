using CrmHub.Appication.Integration.Interfaces.Base;
using CrmHub.Appication.Integration.Models;
using CrmHub.Appication.Integration.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Services.Base
{
    public abstract class BaseIntegration
    {
        protected static string GetFieldValue(FieldMappingValue mapping, EntityBase value)
        {
            Type t = value.GetType();
            PropertyInfo[] props = t.GetProperties();
            try
            {
                PropertyInfo prop = props.Where(w => w.Name.Equals(mapping.PropertyNameHub)).First();
                if (prop.PropertyType.Name.Equals("DateTime"))
                {
                    DateTime date = DateTime.Parse(prop.GetValue(value).ToString());
                    return date.ToString("yyyy-MM-dd hh:mm:ss");
                }
                else
                    return prop.GetValue(value).ToString();
            }
            catch (Exception e) { return string.Empty; }
        }

        public static async Task<bool> SendRequestCreateAsync(ICrmIntegration controller, string url, string content)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(new Uri(url), new StringContent(content));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                controller.MessageController.Clear();
                controller.MessageController.AddSuccessMessage(responseBody);
                return true;
            }
        }

        public static async Task<bool> SendRequestUpdateAsync(ICrmIntegration controller, string url, string content)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent xmlQuery = new StringContent(content);
                var response = await httpClient.PutAsync(new Uri(url), xmlQuery);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                controller.MessageController.Clear();
                controller.MessageController.AddSuccessMessage(responseBody);
                return true;
            }
        }

        public static async Task<bool> SendRequestDeleteAsync(ICrmIntegration controller, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                controller.MessageController.Clear();
                controller.MessageController.AddSuccessMessage(responseBody);
                return true;
            }
        }
    }
}
