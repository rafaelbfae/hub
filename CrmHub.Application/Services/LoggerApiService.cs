using CrmHub.Application.Interfaces;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using System;
using System.Collections.Generic;
using CrmHub.Domain.Interfaces.Filters;

namespace LogApiHub.Application.Services
{
    public class LoggerApiService : ILoggerApiService
    {
        private readonly ILogApiRepository _repository;

        public LoggerApiService(ILogApiRepository repository)
        {
            _repository = repository;
        }

        public void Add(LogApi entity)
        {
            _repository.Add(entity);
        }

        public void AddOrUpdate(LogApi entity)
        {
            _repository.AddOrUpdate(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public LogApi GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<LogApi> GetAll()
        {
            return _repository.List();
        }

        public IEnumerable<LogApi> GetList(IDataTableFilter filter)
        {
            return _repository.GetList(filter);
        }

        public void Remove(LogApi entity)
        {
            _repository.Delete(entity);
        }

        public void Remove(int id)
        {
            _repository.Delete(new LogApi() { Id = id});
        }

        public void Update(LogApi entity)
        {
            _repository.Update(entity);
        }
    }
}
