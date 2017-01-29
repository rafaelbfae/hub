using AutoMapper;
using CrmHub.Appication.Integration.Enuns;
using CrmHub.Appication.Integration.Models;
using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application
{

    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            this.CreateMap<LeadHub, Lead>()
                 .ForMember(s => s.EntityName, i => i.MapFrom(o => "Lead"))
                 .AfterMap((service, integration) => integration.User = new User
                 {
                     Crm = new Crm((eCrmName)Enum.Parse(typeof(eCrmName), service.Autenticacao.TipoCRM)),
                     Token = service.Autenticacao.TokenCRM
                 })

                ;
        }
    }
}
