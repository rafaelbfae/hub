using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface ILeadService : IMessageService
    {
        bool Register(LeadExact value);
        bool Update(LeadExact value);
        bool Fields(Autenticacao value);
        bool Delete(string id, Autenticacao value);
    }
}
