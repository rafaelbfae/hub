using CrmHub.Infra.Messages.Models;
using System.Collections.Generic;

namespace CrmHub.Infra.Messages.Interfaces
{
    public interface IMessageController
    {
        void AddMessage(MessageType value);

        void AddErrorMessage(string value);

        void AddSuccessMessage(string value);

        void Clear();

        List<MessageType> GetAllMessage();

        List<MessageType> GetMessageError();

        List<MessageType> GetMessageSuccess();

        List<MessageType> GetMessageWaring();

        string GetAllMessageToJson();
    }
}
