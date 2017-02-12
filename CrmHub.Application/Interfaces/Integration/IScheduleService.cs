using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IScheduleService
    {
        IMessageController MessageController();
        bool Register(ReuniaoExact value);
        bool Update(ReuniaoExact value);
    }
}
