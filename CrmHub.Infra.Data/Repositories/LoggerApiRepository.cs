using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Context;
using CrmHub.Infra.Data.Repositories.Base;

namespace CrmHub.Infra.Data.Repositories
{
    public class LoggerApiRepository : Repository<LogApi>, ILogApiRepository
    {
        public LoggerApiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
