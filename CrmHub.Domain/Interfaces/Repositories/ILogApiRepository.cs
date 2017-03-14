using CrmHub.Domain.Interfaces.Filters;
using CrmHub.Domain.Models;
using System.Collections.Generic;

namespace CrmHub.Domain.Interfaces.Repositories
{
    public interface ILogApiRepository : IRepository<LogApi>
    {
        IEnumerable<LogApi> GetList(IDataTableFilter filter);
    }
}
