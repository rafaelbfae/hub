﻿using CrmHub.Application.Integration.Models.Roots.Base;

namespace CrmHub.Application.Integration.Models.Roots
{
    public class CompanyRoot : BaseRoot
    {
        public string Id { get; set; }

        public override string GetId() { return Id; }
    }
}