using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using System.Collections.Generic;

namespace CrmHub.Application
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            this.CreateMap<ReuniaoExact, ScheduleRoot>()
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Schedule"))
                .ForMember(s => s.Address, i => i.MapFrom(o => o.Endereco))
                .ForMember(s => s.Schedule, i => i.MapFrom(o => o.Reuniao))
                .ForMember(s => s.Contacts, i => i.MapFrom(o => o.Contatos))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.MapeamentoCampos))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                ;

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
