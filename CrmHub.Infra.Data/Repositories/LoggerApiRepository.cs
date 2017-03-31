using CrmHub.Domain.Interfaces.Filters;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using CrmHub.Infra.Data.Context;
using CrmHub.Infra.Data.Query;
using CrmHub.Infra.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace CrmHub.Infra.Data.Repositories
{
    public class LoggerApiRepository : Repository<LogApi>, ILogApiRepository
    {
        enum Orders { Id, Crm, Entity, Method, Type, CreatedAt, UpdatedAt };

        public LoggerApiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public virtual IEnumerable<LogApi> GetList(IDataTableFilter filter)
        {
            var predicate = PredicateBuilder.True<LogApi>();
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                predicate = predicate
                    .And(x => x.Entity.Contains(filter.Search) 
                    || x.Type.Contains(filter.Search)
                    || x.Method.Contains(filter.Search)
                    || x.Crm.Contains(filter.Search));
            }

            filter.Total = _dbContext.LogApi.Where(predicate).Count();

            var orderBy = (Orders)filter.Order;
            return (_dbContext.LogApi.Where(predicate)
                    .OrderBy(orderBy.ToString(), filter.Dir.Equals("asc"))
                    .Skip(filter.Start)
                    .Take(filter.Length)
                    .Select(x => new LogApi()
                    {
                        Id = x.Id,
                        Crm = x.Crm,
                        Type = x.Type,
                        Entity = x.Entity,
                        Method = x.Method,
                        Empresa = x.Empresa,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                ).ToList();
        }
    }
}
