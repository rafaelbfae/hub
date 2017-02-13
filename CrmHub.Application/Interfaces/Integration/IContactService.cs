using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IContactService
    {
        bool Register(ContatoExact value);
        bool Update(ContatoExact value);
        bool Delete(ContatoExact value);
        bool Fields(Autenticacao value);
        IMessageController MessageController();
    }
}
