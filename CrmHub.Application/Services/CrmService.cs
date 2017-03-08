using CrmHub.Application.Interfaces;
using System;
using CrmHub.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using CrmHub.Application.Integration.Enuns;
using CrmHub.Domain.Interfaces.Repositories;

namespace CrmHub.Application.Services
{
    public class CrmService : ICrmService
    {
        private readonly ICrmRepository _repository;

        public CrmService(ICrmRepository repository)
        {
            _repository = repository;
        }

        public void Add(Crm entity)
        {
            _repository.Add(entity);
        }

        public void AddOrUpdate(Crm entity)
        {
            _repository.AddOrUpdate(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Crm GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Crm GetByName(eCrmName name, string environment)
        {
            var crm = _repository.List(x => x.Name.Equals(name.ToString()) && x.Environment.Equals(environment)).SingleOrDefault();
            return crm;
        }

        public IEnumerable<Crm> GetAll()
        {
            return _repository.List();
        }

        public void Remove(Crm entity)
        {
            _repository.Delete(entity);
        }

        public void Remove(int id)
        {
            _repository.Delete(new Crm() { Id = id});
        }

        public void Update(Crm entity)
        {
            _repository.Update(entity);
        }
    }
}
