using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Integration.Interfaces.Base
{
    public interface ICrmIntegration
    {
        IMessageController MessageController { get; }

        bool Schedule(ScheduleRoot value);
        bool ReSchedule(ScheduleRoot value);

        bool LeadUpdate(LeadRoot value);
        bool LeadDelete(LeadRoot value);
        bool LeadRegister(LeadRoot value);
        bool LeadGetFields(BaseRoot value);

        bool ContactUpdate(ContactRoot value);
        bool ContactDelete(ContactRoot value);
        bool ContactRegister(ContactRoot value);
        bool ContactGetFields(BaseRoot value);

        bool EventUpdate(EventRoot value);
        bool EventDelete(EventRoot value);
        bool EventRegister(EventRoot value);
        bool EventGetFields(BaseRoot value);

        bool CompanyUpdate(CompanyRoot value);
        bool CompanyDelete(CompanyRoot value);
        bool CompanyRegister(CompanyRoot value);
        bool CompanyGetFields(BaseRoot value);
    }
}
