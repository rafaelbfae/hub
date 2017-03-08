using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Context;
using System.Linq;

namespace CrmHub.Infra.Data.Configuration
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            if (context.Crm.Any())
            {
                return;
            }

            var crms = new Crm[]
            {
                new Crm {Name = "ZOHOCRM",   UrlService = "https://crm.zoho.com/crm/private/", UrlAccount = "https://accounts.zoho.com/apiauthtoken/nb/", Environment= "Development" },
                new Crm {Name = "PIPEDRIVE", UrlService = "https://api.pipedrive.com/v1/", UrlAccount = "https://api.pipedrive.com/v1", Environment= "Development" },
                new Crm {Name = "NECTARCRM", UrlService = "http://app.nectarcrm.com.br/crm/api/1/", UrlAccount = "http://app.nectarcrm.com.br/crm/api/1/", Environment= "Development" },
                new Crm {Name = "HUBSPOT",   UrlService = "https://api.hubapi.com/", UrlAccount = "https://api.hubapi.com/", Environment= "Development" },
                new Crm {Name = "RDSTATION", UrlService = "https://www.rdstation.com.br/api/", UrlAccount = "https://www.rdstation.com.br/api/", Environment= "Development" },
                new Crm {Name = "SALESFORCE",UrlService = "https://{0}.salesforce.com/services/data/v39.0/", UrlAccount = "https://{0}.salesforce.com/services/data/v39.0/", Environment= "Development" },
                new Crm {Name = "AGENDOR",   UrlService = " ", UrlAccount = " ", Environment= "Development" },
                new Crm {Name = "MOSKITCRM", UrlService = " ", UrlAccount = " ", Environment= "Development" },
                new Crm {Name = "VTIGER",    UrlService = " ", UrlAccount = " ", Environment= "Development" },

                new Crm {Name = "ZOHOCRM",   UrlService = "https://crm.zoho.com/crm/private/", UrlAccount = "https://accounts.zoho.com/apiauthtoken/nb/", Environment= "Production" },
                new Crm {Name = "PIPEDRIVE", UrlService = "https://api.pipedrive.com/v1/", UrlAccount = "https://api.pipedrive.com/v1", Environment= "Production" },
                new Crm {Name = "NECTARCRM", UrlService = "http://app.nectarcrm.com.br/crm/api/1/", UrlAccount = "http://app.nectarcrm.com.br/crm/api/1/", Environment= "Production" },
                new Crm {Name = "HUBSPOT",   UrlService = "https://api.hubapi.com/", UrlAccount = "https://api.hubapi.com/", Environment= "Production" },
                new Crm {Name = "RDSTATION", UrlService = "https://www.rdstation.com.br/api/", UrlAccount = "https://www.rdstation.com.br/api/", Environment= "Production" },
                new Crm {Name = "SALESFORCE",UrlService = "https://{0}.salesforce.com/services/data/v39.0/", UrlAccount = "https://{0}.salesforce.com/services/data/v39.0/", Environment= "Production" },
                new Crm {Name = "AGENDOR",   UrlService = " ", UrlAccount = " ", Environment= "Production" },
                new Crm {Name = "MOSKITCRM", UrlService = " ", UrlAccount = " ", Environment= "Production" },
                new Crm {Name = "VTIGER",    UrlService = " ", UrlAccount = " ", Environment= "Production" },
            };


            foreach (Crm crm in crms)
            {
                context.Crm.Add(crm);
            }

            context.SaveChanges();

        }
    }
}
