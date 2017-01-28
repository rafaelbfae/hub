using CrmHub.Appication.Integration.Enuns;

namespace CrmHub.Appication.Integration.Models
{
    public class Crm
    {

        public Crm(eCrmName name)
        {
            this.Name = name;
        }

        public eCrmName Name { get; private set; }

        public string UrlService { get; set; }

        public string UrlAccount { get; set; }
    }
}
