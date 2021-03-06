﻿using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Interfaces.Base;
using CrmHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Interfaces
{
    public interface ICrmService : IServiceBase<Crm>
    {
        Crm GetByName(eCrmName name, string environment);
    }
}
