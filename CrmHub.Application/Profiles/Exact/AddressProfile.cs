﻿using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Application.Profiles.Exact
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Endereco, Address>();
       }
    }
}
