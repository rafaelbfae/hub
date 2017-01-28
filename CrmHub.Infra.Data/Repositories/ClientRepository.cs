using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Context;
using CrmHub.Infra.Data.Repositories.Base;

namespace CrmHub.Infra.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
