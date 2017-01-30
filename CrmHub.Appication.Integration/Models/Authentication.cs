using CrmHub.Application.Integration.Enuns;

namespace CrmHub.Application.Integration.Models
{
    public class Authentication
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserToken { get; set; }
        public eCrmName Crm { get; set; }
    }
}
