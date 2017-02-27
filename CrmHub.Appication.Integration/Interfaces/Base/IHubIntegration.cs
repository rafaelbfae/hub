using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Integration.Interfaces.Base
{
    public interface IHubIntegration
    {
        IMessageController MessageController { get; }

        bool Schedule(ScheduleRoot value);
        bool ReSchedule(ScheduleRoot value);
        bool CancelSchedule(string id, Authentication value);
        
        bool LeadUpdate(LeadRoot value);
        bool LeadRegister(LeadRoot value);
        bool LeadDelete(string  id, Authentication value);

        bool LeadGetFields(BaseRoot value);
        bool ContactUpdate(ContactRoot value);
        bool ContactRegister(ContactRoot value);
        bool ContactGetFields(BaseRoot value);
        bool ContactDelete(string email, Authentication value);

        bool EventUpdate(EventRoot value);
        bool EventRegister(EventRoot value);
        bool EventGetFields(BaseRoot value);
        bool EventDelete(string id, Authentication value);

        bool AccountUpdate(AccountRoot value);
        bool AccountRegister(AccountRoot value);
        bool AccountGetFields(BaseRoot value);
        bool AccountDelete(string id, Authentication value);
    }
}
