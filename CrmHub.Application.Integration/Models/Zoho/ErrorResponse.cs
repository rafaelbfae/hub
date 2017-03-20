

namespace CrmHub.Application.Integration.Models.Zoho
{
    public class ErrorResponse
    {
        public Response response { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Response
    {
        public Error error { get; set; }
        public string uri { get; set; }
    }

}
