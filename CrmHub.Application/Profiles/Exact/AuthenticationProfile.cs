using AutoMapper;
using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;
using System;

namespace CrmHub.Application.Profiles.Exact
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            this.CreateMap<Autenticacao, Authentication>()
                .ForMember(s => s.Token, i => i.MapFrom(o => o.TokenCRM))
                .ForMember(s => s.User, i => i.MapFrom(o => o.UsuarioCRM))
                .ForMember(s => s.UserToken, i => i.MapFrom(o => o.UsuarioToken))
                .ForMember(s => s.Email, i => i.MapFrom(o => o.EmailVendedor))
                .ForMember(s => s.Crm, i => i.MapFrom(o => (eCrmName)Enum.Parse(typeof(eCrmName), o.TipoCRM)))
                ;
        }
    }
}
