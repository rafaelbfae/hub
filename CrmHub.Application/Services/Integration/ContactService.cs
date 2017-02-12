using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Services.Integration
{
    public class ContactService : IContactService
    {
        private readonly IHubService _service;

        public ContactService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(ContatoExact value)
        {
            return _service.ContactUpdate(value);
        }

        public bool Register(ContatoExact value)
        {
            return _service.ContactRegister(value);
        }

        public bool Delete(ContatoExact value)
        {
            return _service.ContactDelete(value);
        }
    }
}
