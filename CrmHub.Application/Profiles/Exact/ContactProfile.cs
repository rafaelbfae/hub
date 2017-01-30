using AutoMapper;
using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Linq;

namespace CrmHub.Application
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            this.CreateMap<ContatoExact, ContactRoot>()
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Contact"))
                //.ForMember(s => s.Contact.Name, i => i.MapFrom(o => o.Contato.Nome))
                //.ForMember(s => s.Contact.Email, i => i.MapFrom(o => o.Contato.Email))
                //.ForMember(s => s.Contact.Role, i => i.MapFrom(o => o.Contato.Cargo))
                //.ForMember(s => s.Contact.MessengerId, i => i.MapFrom(o => o.Contato.IdMensageiro))
                //.ForMember(s => s.Contact.MessengerType, i => i.MapFrom(o => o.Contato.TipoMensageiro))
                //.ForMember(s => s.Contact.Phones, i => i.MapFrom(o => o.Contato.Telefone))
                .AfterMap((service, integration) => integration.Authentication = new Authentication
                {
                    Crm = (eCrmName)Enum.Parse(typeof(eCrmName), service.Autenticacao.TipoCRM),
                    Token = service.Autenticacao.TokenCRM,
                    User = service.Autenticacao.UsuarioCRM,
                    UserToken = service.Autenticacao.UsuarioToken,
                    Email = service.Autenticacao.EmailVendedor
                })
                .AfterMap((service, integration) => integration.MappingFields = service.MapeamentoCampos.Where(x => x.TipoEntidadeExact.Equals("Contato")).Select(x => new MappingFields
                {
                    CrmField = x.CampoCRM,
                    CrmEntity = x.TipoEntidadeCRM,
                    ClientField = x.CampoExact,
                    ClientEntity = "Contact"
                }).ToList());

            this.CreateMap<Contato, Contact>()
                .ForMember(s => s.Name, i => i.MapFrom(o => o.Nome))
                .ForMember(s => s.Email, i => i.MapFrom(o => o.Email))
                .ForMember(s => s.Role, i => i.MapFrom(o => o.Cargo))
                .ForMember(s => s.MessengerId, i => i.MapFrom(o => o.IdMensageiro))
                .ForMember(s => s.MessengerType, i => i.MapFrom(o => o.TipoMensageiro))
                .ForMember(s => s.Phones, i => i.MapFrom(o => o.Telefone));

        }
    }
}
