using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using System.Collections.Generic;

namespace CrmHub.Application
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            this.CreateMap<ContatoExact, ContactRoot>()
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.MapeamentoCampos))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                .AfterMap((s, i) => s.MapeamentoCampos.AddRange(s.Contato.GetFieldsByAttribute(0, i.Authentication.Crm)))
            ;

            this.CreateMap<Contato, Contact>();

        }
    }
}
