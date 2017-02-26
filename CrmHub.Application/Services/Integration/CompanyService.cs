using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Services.Integration
{
    public class CompanyService : ICompanyService
    {
        private readonly IHubService _service;

        public CompanyService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(EmpresaExact value)
        {
            return _service.CompanyUpdate(value);
        }

        public bool Register(EmpresaExact value)
        {
            return _service.CompanyRegister(value);
        }

        public bool Delete(string id, Autenticacao value)
        {
            return _service.CompanyDelete(id, value);
        }

        public bool Fields(Autenticacao value)
        {
            return _service.CompanyGetFields(value);
        }
    }
}
