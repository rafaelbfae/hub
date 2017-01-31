using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;
using System.Collections.Generic;

namespace CrmHub.Application.Profiles.Exact
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Endereco, Address>()
                .ForMember(s => s.City, i => i.MapFrom(o => o.Cidade))
                .ForMember(s => s.Maps, i => i.MapFrom(o => o.Maps))
                .ForMember(s => s.State, i => i.MapFrom(o => o.Estado))
                .ForMember(s => s.Street, i => i.MapFrom(o => o.Rua))
                .ForMember(s => s.Country, i => i.MapFrom(o => o.Pais))
                .ForMember(s => s.ZipCode, i => i.MapFrom(o => o.CEP))
                .ForMember(s => s.Complement, i => i.MapFrom(o => o.Complemento))
                ;
       }
    }
}
