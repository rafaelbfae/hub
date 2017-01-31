﻿using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact.Roots;
using System.Collections.Generic;

namespace CrmHub.Application
{

    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            this.CreateMap<LeadExact, LeadRoot>()
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Lead"))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.MapeamentoCampos))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                ;

            this.CreateMap<Models.Exact.Lead, Lead>()
                 .ForMember(s => s.Name, i => i.MapFrom(o => o.Nome))
                 .ForMember(s => s.Site, i => i.MapFrom(o => o.Site))
                 .ForMember(s => s.Link, i => i.MapFrom(o => o.LinkExact))
                 .ForMember(s => s.Note, i => i.MapFrom(o => o.Observacao))
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
