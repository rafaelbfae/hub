using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact.Roots;
using System.Collections.Generic;

namespace CrmHub.Application
{

    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            this.CreateMap<LeadExact, LeadRoot>()
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Lead"))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.MapeamentoCampos))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                ;

            this.CreateMap<Models.Exact.Lead, Lead>();
        }

    }
}
