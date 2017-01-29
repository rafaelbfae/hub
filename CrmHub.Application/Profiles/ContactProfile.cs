using AutoMapper;
using CrmHub.Appication.Integration.Models;
using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            this.CreateMap<ContactHub, Contact>();
        }
    }
}
