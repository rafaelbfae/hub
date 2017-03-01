using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application
{

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            this.CreateMap<EventoExact, EventRoot>()
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.GetFieldsByMapping()))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Reuniao.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.EntidadeCampoValor))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                .ForMember(s => s.CustomFields, i => i.MapFrom(o => o.CamposPersonalizados))
                .ForMember(s => s.Schedule, i => i.MapFrom(o => o.Reuniao))
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Event"))
                ;

            this.CreateMap<Models.Exact.Reuniao, Events>();
        }

    }
}
