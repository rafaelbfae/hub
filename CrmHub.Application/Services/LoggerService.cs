using CrmHub.Application.Interfaces;
using CrmHub.Domain.Interfaces.Repositories;
using CrmHub.Domain.Models;
using System;
using System.Collections.Generic;
using CrmHub.Domain.Interfaces.Filters;
using CrmHub.Application.Interfaces.Integration;
using Newtonsoft.Json;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;

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

        public bool Resent(string id, LogApi log)
        {
            if (log.Entity.Equals("EmpresaExact") || log.Entity.Equals("Company"))
            {
                return ResendEmpresaExact(id, log);
            }

            if (log.Entity.Equals("ContatoExact") || log.Entity.Equals("Contact"))
            {
                return ResendContatoExact(id, log);
            }

            if (log.Entity.Equals("EventoExact") || log.Entity.Equals("Event"))
            {
                return ResendEventoExact(id, log);
            }

            if (log.Entity.Equals("LeadExact") || log.Entity.Equals("Lead"))
            {
                return ResendLeadExact(id, log);
            }

            if (log.Entity.Equals("ReuniaoExact") || log.Entity.Equals("Schedule"))
            {
                return ResendReuniaoExact(id, log);
            }

            return false;
        }

        private bool Execute<E>(LogApi log, E value, Func<IHubService, E, bool> function)
        {
            bool success = function(_service, value);
            log.Type = success ? "Success" : "Error";
            log.Response = _service.MessageController().GetAllMessageToJson();

            if (success)
            {
                _repository.Update(log);
            }

            return success;
        }

        private bool Execute(LogApi log, string id, Autenticacao value, Func<IHubService, string, Autenticacao, bool> function)
        {
            bool success = function(_service, id, value);
            log.Type = success ? "Success" : "Error";
            log.Response = _service.MessageController().GetAllMessageToJson();

            if (success)
            {
                _repository.Update(log);
            }

            return success;
        }

        private bool ResendEmpresaExact(string id, LogApi log)
        {
            var _value = JsonConvert.DeserializeObject<EmpresaExact>(log.Send);
            if (log.Method.Equals("Post"))
            {
                var resp = Execute(log, _value, (c, v) => c.AccountRegister(_value));
                return resp;
            }

            if (log.Method.Equals("Put"))
            {
                _value.Empresa.Id = log.Parameters;
                var resp = Execute(log, _value, (c, v) => c.AccountUpdate(_value));
                return resp;
            }

            if (log.Method.Equals("Delete"))
            {
                var aut = JsonConvert.DeserializeObject<Autenticacao>(log.Send);
                var resp = Execute(log, _value, (c, v) => c.AccountDelete(log.Parameters, aut));
                return resp;
            }

            return false;
        }

        private bool ResendContatoExact(string id, LogApi log)
        {
            var _value = JsonConvert.DeserializeObject<ContatoExact>(log.Send);
            if (log.Method.Equals("Post"))
            {
                var resp = Execute(log, _value, (c, v) => c.ContactRegister(_value));
                return resp;
            }

            if (log.Method.Equals("Put"))
            {
                _value.Contato.Id = log.Parameters;
                var resp = Execute(log, _value, (c, v) => c.ContactUpdate(_value));
                return resp;
            }

            if (log.Method.Equals("Delete"))
            {
                var aut = JsonConvert.DeserializeObject<Autenticacao>(log.Send);
                var resp = Execute(log, _value, (c, v) => c.ContactDelete(log.Parameters, aut));
                return resp;
            }

            return false;
        }

        private bool ResendEventoExact(string id, LogApi log)
        {
            var _value = JsonConvert.DeserializeObject<EventoExact>(log.Send);
            if (log.Method.Equals("Post"))
            {
                var resp = Execute(log, _value, (c, v) => c.EventRegister(_value));
                return resp;
            }

            if (log.Method.Equals("Put"))
            {
                _value.Reuniao.Id = log.Parameters;
                var resp = Execute(log, _value, (c, v) => c.EventUpdate(_value));
                return resp;
            }

            if (log.Method.Equals("Delete"))
            {
                var aut = JsonConvert.DeserializeObject<Autenticacao>(log.Send);
                var resp = Execute(log, _value, (c, v) => c.EventDelete(log.Parameters, aut));
                return resp;
            }

            return false;
        }

        private bool ResendLeadExact(string id, LogApi log)
        {
            var _value = JsonConvert.DeserializeObject<LeadExact>(log.Send);
            if (log.Method.Equals("Post"))
            {
                var resp = Execute(log, _value, (c, v) => c.LeadRegister(_value));
                return resp;
            }

            if (log.Method.Equals("Put"))
            {
                _value.Lead.Id = log.Parameters;
                var resp = Execute(log, _value, (c, v) => c.LeadUpdate(_value));
                return resp;
            }

            if (log.Method.Equals("Delete"))
            {
                var aut = JsonConvert.DeserializeObject<Autenticacao>(log.Send);
                var resp = Execute(log, _value, (c, v) => c.LeadDelete(log.Parameters, aut));
                return resp;
            }

            return false;
        }

        private bool ResendReuniaoExact(string id, LogApi log)
        {
            var _value = JsonConvert.DeserializeObject<ReuniaoExact>(log.Send);
            if (log.Method.Equals("Post"))
            {
                var resp = Execute(log, _value, (c, v) => c.ScheduleRegister(_value));
                return resp;
            }

            if (log.Method.Equals("Put"))
            {
                _value.Reuniao.Id = log.Parameters;
                var resp = Execute(log, _value, (c, v) => c.ScheduleUpdate(_value));
                return resp;
            }

            if (log.Method.Equals("Delete"))
            {
                var aut = JsonConvert.DeserializeObject<Autenticacao>(log.Send);
                var resp = Execute(log, _value, (c, v) => c.ScheduleDelete(log.Parameters, aut));
                return resp;
            }

            return false;
        }

        


    }
}
