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
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Contact"))
                .ForMember(s => s.Contact, i => i.MapFrom(o => o.Contato))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.MapeamentoCampos))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                ;

            this.CreateMap<Contato, Contact>()
                .ForMember(s => s.Name, i => i.MapFrom(o => o.Nome))
                .ForMember(s => s.Role, i => i.MapFrom(o => o.Cargo))
                .ForMember(s => s.Email, i => i.MapFrom(o => o.Email))
                .ForMember(s => s.Phones, i => i.MapFrom(o => o.Telefone))
                .ForMember(s => s.MessengerId, i => i.MapFrom(o => o.IdMensageiro))
                .ForMember(s => s.MessengerType, i => i.MapFrom(o => o.TipoMensageiro))
                ;

        }
    }
}
