

namespace CrmHub.Application.Integration.Models
{
    public class Company
    {
        public Company()
        {
            this.Name = string.Empty;
            this.WebSite = string.Empty;
            this.Phone = string.Empty;
        }

        public string Name { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
    }
}
