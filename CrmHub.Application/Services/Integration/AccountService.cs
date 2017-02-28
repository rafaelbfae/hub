using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Services.Integration
{
    public class AccountService : IAccountService
    {
        private readonly IHubService _service;

        public AccountService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(EmpresaExact value)
        {
            return _service.AccountUpdate(value);
        }

        public bool Register(EmpresaExact value)
        {
            return _service.AccountRegister(value);
        }

        public bool Delete(string id, Autenticacao value)
        {
            return _service.AccountDelete(id, value);
        }

        public bool Fields(Autenticacao value)
        {
            return _service.AccountGetFields(value);
        }
    }
}
