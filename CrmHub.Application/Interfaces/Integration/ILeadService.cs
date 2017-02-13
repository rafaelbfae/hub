using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface ILeadService
    {
        bool Register(LeadExact value);
        bool Update(LeadExact value);
        bool Delete(LeadExact value);
        bool Fields(Autenticacao value);
        IMessageController MessageController();
    }
}
