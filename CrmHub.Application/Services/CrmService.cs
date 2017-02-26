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
            throw new NotImplementedException();
        }

        public void AddOrUpdate(Crm entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Crm GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Crm GetByName(eCrmName name, string environment)
        {
            var crm = _repository.List(x => x.Name.Equals(name.ToString()) && x.Environment.Equals(environment)).SingleOrDefault();
            return crm;
        }

        public List<Crm> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Crm entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Crm entity)
        {
            throw new NotImplementedException();
        }
    }
}
