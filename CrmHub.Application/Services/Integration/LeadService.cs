using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;


namespace CrmHub.Application.Services.Integration
{
    public class LeadService : ILeadService
    {
        private readonly IHubService _service;

        public LeadService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(LeadExact value)
        {
            return _service.LeadUpdate(value);
        }

        public bool Register(LeadExact value)
        {
            return _service.LeadRegister(value);
        }

        public bool Delete(LeadExact value)
        {
            return _service.LeadDelete(value);
        }

        public bool Fields(Autenticacao value)
        {
            return _service.LeadGetFields(value);
        }
    }
}
