using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IHubService : IMessageService
    {
        bool ScheduleRegister(ReuniaoExact value);
        bool ScheduleUpdate(ReuniaoExact value);
        bool ScheduleGetFields(Autenticacao value);
        bool ScheduleDelete(string id, Autenticacao value);

        bool LeadRegister(LeadExact value);
        bool LeadUpdate(LeadExact value);
        bool LeadGetFields(Autenticacao value);
        bool LeadDelete(string id, Autenticacao value);

        bool ContactRegister(ContatoExact value);
        bool ContactUpdate(ContatoExact value);
        bool ContactGetFields(Autenticacao value);
        bool ContactDelete(string email, Autenticacao value);

        bool AccountRegister(EmpresaExact value);
        bool AccountUpdate(EmpresaExact value);
        bool AccountGetFields(Autenticacao value);
        bool AccountDelete(string id, Autenticacao value);
    }
}