using CrmHub.Application.Interfaces;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using System;
using System.Collections.Generic;
using CrmHub.Domain.Interfaces.Filters;
using CrmHub.Application.Interfaces.Integration;
using Newtonsoft.Json;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogApiRepository _repository;
        private readonly IHubService _service;

        public LoggerService(IHubService service, ILogApiRepository repository)
        {
            this._service = service;
            this._repository = repository;
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
            _repository.Delete(new LogApi() { Id = id });
        }

        public void Update(LogApi entity)
        {
            _repository.Update(entity);
        }

        public bool Resent(int id, LogApi log)
        {
            if (log.Entity.Equals("EmpresaExact"))
            {
                var _value = JsonConvert.DeserializeObject<EmpresaExact>(log.Send);
                if (log.Method.Equals("Post"))
                {
                    var resp = Execute<EmpresaExact>(log, _value, (c, v) => c.AccountRegister(_value));
                    return resp;
                }
            }

            return false;
        }

        private bool Execute<E>(LogApi log, E value, Func<IHubService, E, bool> function)
        {
            bool success = function(_service, value);
            log.Type = success ? "Success" : "Error";
            log.Response = _service.MessageController().GetAllMessageToJson();
            return success;
        }
    }
}
