using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Interfaces.Base
{
    public interface IServiceBase<T> where T : class
    {
        void Add(T entity);
        void AddOrUpdate(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Remove(int id);
        void Remove(T entity);
        void Dispose();
    }
}
