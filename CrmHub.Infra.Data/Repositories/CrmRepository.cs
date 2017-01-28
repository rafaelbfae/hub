using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Repositories.Base;
using CrmHub.Infra.Data.Context;
using CrmHub.Domain.Interfaces.Repositories;

namespace CrmHub.Infra.Data.Repositories
{
    public class CrmRepository : Repository<Crm>, ICrmRepository
    {
        public CrmRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
