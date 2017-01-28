using CrmHub.Infra.Data.Repositories.Base;
using CrmHub.Infra.Data.Context;
using CrmHub.Domain.Models;
using CrmHub.Domain.Interfaces.Repositories;

namespace CrmHub.Infra.Data.Repositories
{
    public class FieldMappingValueRepository : Repository<FieldMappingValue>, IFieldMappingValueRepository
    {
        public FieldMappingValueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
