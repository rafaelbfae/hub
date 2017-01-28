using System.Linq;
using System.Collections.Generic;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;

namespace CrmHub.Infra.Messages
{
    public class MessageController : IMessageController
    {
        #region Constructor

        public MessageController()
        {
            ListMessage = new List<MessageType>();
        }

        #endregion

        #region Properties

        protected List<MessageType> ListMessage { get; set; }

        #endregion

        #region Public Methods

        public void AddMessage(MessageType value)
        {
            ListMessage.Add(value);
        }

        public void AddErrorMessage(string value)
        {
            ListMessage.Add(
                new MessageType(MessageType.TYPE.ERROR)
                {
                    Message = value
                }
            );
        }

        public void AddSuccessMessage(string value)
        {
            ListMessage.Add(
                new MessageType(MessageType.TYPE.SUCCESS)
                {
                    Message = value
                }
            );
        }

        public void Clear()
        {
            ListMessage.Clear();
        }

        public List<MessageType> GetAllMessage() => ListMessage;

        public List<MessageType> GetMessageError() => ListMessage.Where(w => w.Type.Equals(MessageType.TYPE.ERROR)).ToList();

        public List<MessageType> GetMessageSuccess() => ListMessage.Where(w => w.Type.Equals(MessageType.TYPE.SUCCESS)).ToList();

        public List<MessageType> GetMessageWaring() => ListMessage.Where(w => w.Type.Equals(MessageType.TYPE.WARING)).ToList();

        #endregion
    }
}