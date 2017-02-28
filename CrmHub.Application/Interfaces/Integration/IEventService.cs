using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IEventService : IMessageService
    {
        bool Register(ReuniaoExact value);
        bool Update(ReuniaoExact value);
        bool Fields(Autenticacao value);
        bool Delete(string id, Autenticacao value);
    }
}
