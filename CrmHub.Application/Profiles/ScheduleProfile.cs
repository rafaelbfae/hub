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
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            this.CreateMap<ScheduleHub, Schedule>()
                .ForMember(s => s.FlNotification, i => i.Ignore())
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Schedule"))
                .ForMember(s => s.Start, i => i.MapFrom(o => o.Reuniao.DataIni))
                .ForMember(s => s.End, i => i.MapFrom(o => o.Reuniao.DataFim))
                .ForMember(s => s.Subject, i => i.MapFrom(o => o.Reuniao.Referencia))
                .ForMember(s => s.Venue, i => i.MapFrom(o => o.Reuniao.Endereco))
                .ForMember(s => s.Lead, i => i.MapFrom(o => new Lead()))
                .AfterMap((service, integration) => integration.User = new User
                {
                    Crm = new Crm((eCrmName)Enum.Parse(typeof(eCrmName), service.Autenticacao.TipoCRM)),
                    Token = service.Autenticacao.TokenCRM
                })
                .AfterMap((service, integration) => integration.Lead.Mapping = service.MapeamentoCampos.Select(x => new FieldMappingValue
                {
                    PropertyNameCrm = x.CampoCRM,
                    PropertyNameHub = x.CampoCRM
                }).ToList());
            ;
        }
    }
}
