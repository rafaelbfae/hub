using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Application.Profiles.Exact
{
    public class CustomFieldsProfile : Profile
    {
        public CustomFieldsProfile()
        {
            this.CreateMap<CamposPersonalizados, CustomFields>();
        }
    }
}
