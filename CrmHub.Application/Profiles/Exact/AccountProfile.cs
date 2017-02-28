using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<EmpresaExact, AccountRoot>()
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.GetFieldsByMapping()))
                .BeforeMap((s, i) => s.EntidadeCampoValor.AddRange(s.Empresa.GetFieldsByAttribute(0, s.Autenticacao.Crm())))
                .ForMember(s => s.MappingFields, i => i.MapFrom(o => o.EntidadeCampoValor))
                .ForMember(s => s.Authentication, i => i.MapFrom(o => o.Autenticacao))
                .ForMember(s => s.CustomFields, i => i.MapFrom(o => o.CamposPersonalizados))
                .ForMember(s => s.EntityName, i => i.MapFrom(o => "Account"))
                .ForMember(s => s.Id, i => i.MapFrom(o => o.Empresa.Id))
            ;

            this.CreateMap<Empresa, Account>();

        }
    }
}
