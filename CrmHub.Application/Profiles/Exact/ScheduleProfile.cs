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
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            this.CreateMap<ReuniaoExact, ScheduleRoot>()
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Schedule"))
                .ForMember(s => s.Schedule, i => i.MapFrom(o => o.Reuniao))
                .ForMember(s => s.Address, i => i.MapFrom(o => o.Endereco))
                .ForMember(s => s.Contacts, i => i.MapFrom(o => o.Contatos))
                .AfterMap((service, integration) => integration.Authentication = new Authentication
                {
                    Crm = (eCrmName)Enum.Parse(typeof(eCrmName), service.Autenticacao.TipoCRM),
                    Token = service.Autenticacao.TokenCRM,
                    User = service.Autenticacao.UsuarioCRM,
                    UserToken = service.Autenticacao.UsuarioToken,
                    Email = service.Autenticacao.EmailVendedor
                })
                .AfterMap((service, integration) => integration.MappingFields = service.MapeamentoCampos.Where(x => x.TipoEntidadeExact.Equals("Reuniao")).Select(x => new MappingFields
                {
                    CrmField = x.CampoCRM,
                    CrmEntity = x.TipoEntidadeCRM,
                    ClientField = x.CampoExact,
                    ClientEntity = "Schedule"
                }).ToList());

            this.CreateMap<Reuniao, Schedule>()
                .ForMember(s => s.End, i => i.MapFrom(o => o.DataFim))
                .ForMember(s => s.Type, i => i.MapFrom(o => o.TipoReuniao))
                .ForMember(s => s.Start, i => i.MapFrom(o => o.DataIni))
                .ForMember(s => s.Subject, i => i.MapFrom(o => o.Referencia))
                .ForMember(s => s.Address, i => i.MapFrom(o => o.Endereco))
                .ForMember(s => s.TimeZone, i => i.MapFrom(o => o.TimeZone))
            ;
        }
    }
}
