using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            this.CreateMap<ContatoExact, ContactRoot>()
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.GetFieldsByMapping()))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Contato.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.EntidadeCampoValor))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                .ForMember(s => s.CustomFields, i => i.MapFrom(o => o.CamposPersonalizados))
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Contact"))
                .ForMember(s => s.Contact, i => i.MapFrom(o => o.Contato))
            ;

            this.CreateMap<Contato, Contact>();

        }
    }
}
