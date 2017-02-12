using CrmHub.Infra.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface ICompanyService
    {
        IMessageController MessageController();
    }
}
