using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IHubService
    {
        IMessageController MessageController();

        bool ScheduleRegister(ReuniaoExact value);
        bool ScheduleUpdate(ReuniaoExact value);
        bool ScheduleGetFields(Autenticacao value);

        bool LeadRegister(LeadExact value);
        bool LeadUpdate(LeadExact value);
        bool LeadDelete(LeadExact value);
        bool LeadGetFields(Autenticacao value);

        bool ContactRegister(ContatoExact value);
        bool ContactUpdate(ContatoExact value);
        bool ContactDelete(ContatoExact value);
        bool ContactGetFields(Autenticacao value);
    }
}