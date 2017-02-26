using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IContactService : IMessageService
    {
        bool Register(ContatoExact value);
        bool Update(ContatoExact value);
        bool Fields(Autenticacao value);
        bool Delete(string email, Autenticacao value);
    }
}
