using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Repositories.Base;
using CrmHub.Infra.Data.Context;
using CrmHub.Domain.Interfaces.Repositories;

namespace CrmHub.Infra.Data.Repositories
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
