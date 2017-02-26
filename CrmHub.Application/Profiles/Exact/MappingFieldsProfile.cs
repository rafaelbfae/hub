using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;
using System.Collections.Generic;

namespace CrmHub.Application.Profiles.Exact
{
    public class MappingFieldsProfile : Profile
    {
        public MappingFieldsProfile()
        {
            this.CreateMap<MapeamentoCampos, MappingFields>()
                .ForMember(s => s.Field, i => i.MapFrom(o => o.CampoCRM))
                .ForMember(s => s.Entity, i => i.MapFrom(o => o.TipoEntidadeCRM))
                .ForMember(s => s.Value, i => i.MapFrom(o => o.Valor))
                ;
        }
    }
}
