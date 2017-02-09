using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IHubService
    {
        IMessageController MessageController();

        bool Schedule(ReuniaoExact value);
        bool ReSchedule(ReuniaoExact value);
        
        bool LeadRegister(LeadExact value);
        bool LeadUpdate(LeadExact value);
        bool LeadDelete(LeadExact value);

        bool ContactRegister(ContatoExact value);
        bool ContactUpdate(ContatoExact value);
        bool ContactDelete(ContatoExact value);
    }
}
