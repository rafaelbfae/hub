using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IMessageService
    {
        IMessageController MessageController();
    }
}
