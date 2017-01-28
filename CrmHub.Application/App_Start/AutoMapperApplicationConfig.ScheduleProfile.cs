using AutoMapper;
using CrmHub.Appication.Integration.Models;
using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application
{
    internal partial class AutoMapperApplicationConfig
    {
        internal sealed class ScheduleProfile : Profile
        {
            public ScheduleProfile()
            {
                this.CreateMap<ScheduleHub, Schedule>();
            }
        }
    }
}
