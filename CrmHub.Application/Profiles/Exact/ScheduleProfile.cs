using AutoMapper;
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
            Action<ReuniaoExact, ScheduleRoot> loadContato = (s, i) =>
            {
                int id = 0;
                s.Contatos.ForEach(c => c.Ordem = c.Email.Equals(s.Lead.Email) ? 0 : 1);
                s.Contatos.OrderBy(c => c.Ordem).ToList()
                    .ForEach(x => s.EntidadeCampoValor.AddRange(x.GetFieldsByAttribute(id++, s.Autenticacao.Crm())));
            };

            this.CreateMap<ReuniaoExact, ScheduleRoot>()
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.GetFieldsByMapping()))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Reuniao.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Endereco.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Lead.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .BeforeMap((s, i) => loadContato(s, i))
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Event"))
                .ForMember(s => s.Lead, i => i.MapFrom(o => o.Lead))
                .ForMember(s => s.Schedule, i => i.MapFrom(o => o.Reuniao))
                .ForMember(s => s.CustomFields, i => i.MapFrom(o => o.CamposPersonalizados))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.EntidadeCampoValor))
                .ForMember(s => s.Contacts, i => i.MapFrom(o => o.Contatos))
                .ForMember(s => s.Account, i => i.MapFrom(o => o.Empresa))
            ;

            this.CreateMap<Reuniao, Events>();
        }
    }
}
