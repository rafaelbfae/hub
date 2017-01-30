using AutoMapper;
using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Linq;

namespace CrmHub.Application
{

    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            this.CreateMap<LeadExact, LeadRoot>()
                 .ForMember(s => s.EntityName, i => i.MapFrom(o => "Lead"))
                 //.ForMember(s => s.Lead.Name, i => i.MapFrom(o => o.Lead.Nome))
                 //.ForMember(s => s.Lead.Site, i => i.MapFrom(o => o.Lead.Site))
                 //.ForMember(s => s.Lead.Link, i => i.MapFrom(o => o.Lead.LinkExact))
                 //.ForMember(s => s.Lead.Note, i => i.MapFrom(o => o.Lead.Nome))
                 //.ForMember(s => s.Lead.Data, i => i.MapFrom(o => o.Lead.DataCadastro))
                 //.ForMember(s => s.Lead.Source, i => i.MapFrom(o => o.Lead.Origem))
                 //.ForMember(s => s.Lead.Market, i => i.MapFrom(o => o.Lead.Mercado))
                 //.ForMember(s => s.Lead.Phones, i => i.MapFrom(o => o.Lead.Telefone))
                 //.ForMember(s => s.Lead.Vendor, i => i.MapFrom(o => o.Lead.PreVendedor))
                 //.ForMember(s => s.Lead.Product, i => i.MapFrom(o => o.Lead.Produto))
                 //.ForMember(s => s.Lead.SubSource, i => i.MapFrom(o => o.Lead.SubOrigem))
                 //.ForMember(s => s.Lead.Diagnosis, i => i.MapFrom(o => o.Lead.Diagnostico))
                 .AfterMap((service, integration) => integration.Authentication = new Authentication
                 {
                     Crm = (eCrmName)Enum.Parse(typeof(eCrmName), service.Autenticacao.TipoCRM),
                     Token = service.Autenticacao.TokenCRM,
                     User = service.Autenticacao.UsuarioCRM,
                     UserToken = service.Autenticacao.UsuarioToken,
                     Email = service.Autenticacao.EmailVendedor
                 })
                .AfterMap((service, integration) => integration.MappingFields = service.MapeamentoCampos.Where(x => x.TipoEntidadeExact.Equals("Lead")).Select(x => new MappingFields
                {
                    CrmField = x.CampoCRM,
                    CrmEntity = x.TipoEntidadeCRM,
                    ClientField = x.CampoExact,
                    ClientEntity = "Lead"
                }).ToList());

            this.CreateMap<Models.Exact.Lead, Lead>()
                 .ForMember(s => s.Name, i => i.MapFrom(o => o.Nome))
                 .ForMember(s => s.Site, i => i.MapFrom(o => o.Site))
                 .ForMember(s => s.Link, i => i.MapFrom(o => o.LinkExact))
                 .ForMember(s => s.Note, i => i.MapFrom(o => o.Nome))
                 .ForMember(s => s.Data, i => i.MapFrom(o => o.DataCadastro))
                 .ForMember(s => s.Vendor, i => i.MapFrom(o => o.PreVendedor))
                 .ForMember(s => s.Source, i => i.MapFrom(o => o.Origem))
                 .ForMember(s => s.Market, i => i.MapFrom(o => o.Mercado))
                 .ForMember(s => s.Phones, i => i.MapFrom(o => o.Telefone))
                 .ForMember(s => s.Product, i => i.MapFrom(o => o.Produto))
                 .ForMember(s => s.SubSource, i => i.MapFrom(o => o.SubOrigem))
                 .ForMember(s => s.Diagnosis, i => i.MapFrom(o => o.Diagnostico))
            ;
        }
    }
}
