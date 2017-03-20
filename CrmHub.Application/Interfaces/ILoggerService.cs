using CrmHub.Application.Interfaces.Base;
using CrmHub.Domain.Models;

namespace CrmHub.Application.Interfaces
{
    public interface ILoggerService : IServiceBase<LogApi>
    {
        bool Resent(string id, LogApi entity);
    }
}
