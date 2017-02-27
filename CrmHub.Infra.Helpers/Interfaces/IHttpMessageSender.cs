using System;

namespace CrmHub.Infra.Helpers.Interfaces
{
    public interface IHttpMessageSender
    {
        bool SendRequestGet(string url, object value, Func<string, object, bool> loadResponse);
        bool SendRequestPost(string url, string content, object value, Func<string, object, bool> getResponse);
        bool SendRequestDelete(string url, object value, Func<string, object, bool> loadResponse);
    }
}
