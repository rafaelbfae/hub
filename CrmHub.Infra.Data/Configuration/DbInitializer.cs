using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Infra.Data.Configuration
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            if (context.Company.Any())
            {
                return;
            }

            var company = new Company
            {
                Name = "ExactSales",
                Phone = "554830257750",
                Email = "froman@exactsales.com.br",
                Contact = "Felipe Roman"
            };

            context.SaveChanges();

            var attributeEntities = new AttributeEntity[]
            {
                new AttributeEntity("Lead", "Nome", "Nome", company),
                new AttributeEntity("Lead", "Tel1", "Telefone 1", company),
                new AttributeEntity("Lead", "Site", "Site", company),
                new AttributeEntity("Lead", "Obs", "Observação", company),
                new AttributeEntity("Lead", "LinkRD", "Link RD", company),
                new AttributeEntity("PreVendedor", "NomeCompleto", "Nome", company),
                new AttributeEntity("Vendedor", "NomeCompleto", "Nome", company),
                new AttributeEntity("Contato", "Nome", "Nome", company),
                new AttributeEntity("Contato", "Tel1", "Telefone 1", company),
                new AttributeEntity("Contato", "Tel2", "Telefone 2", company),
                new AttributeEntity("Contato", "Cargo", "Cargo", company),
                new AttributeEntity("Contato", "Email", "Email", company),
                new AttributeEntity("Cidade", "Nome", "Nome Cidade", company),
                new AttributeEntity("Estado", "Nome", "Nome Estado", company),
                new AttributeEntity("Pais", "Nome", "Nome País", company),
                new AttributeEntity("Endereco", "Rua", "Rua", company),
                new AttributeEntity("Endereco", "Numero", "Número", company),
                new AttributeEntity("Endereco", "Complemento", "Complemento", company),
                new AttributeEntity("Endereco", "CEP", "CEP", company),
                new AttributeEntity("Endereco", "Maps", "Local da Pesquisa no Maps", company),
                new AttributeEntity("Mercado", "DeMercado", "Descrição do Mercado", company),
                new AttributeEntity("Origem", "DeOrigem", "Descrição Origem", company),
                new AttributeEntity("SubOrigemLead", "DeSubOrigem", "Descrição Sub-Origem", company),
                new AttributeEntity("FaseLead", "Nome", "Nome", company)
            };

            foreach (AttributeEntity attr in attributeEntities)
            {
                context.AttributeEntity.Add(attr);
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
