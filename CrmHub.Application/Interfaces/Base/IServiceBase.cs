using CrmHub.Domain.Interfaces.Filters;
using System.Collections.Generic;

namespace CrmHub.Application.Interfaces.Base
{
    public interface IServiceBase<T> where T : class
    {
        void Add(T entity);
        void AddOrUpdate(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList(IDataTableFilter filter);
        void Update(T entity);
        void Remove(int id);
        void Remove(T entity);
        void Dispose();
    }
}
