using System.Linq;
using System.Collections.Generic;

namespace CrmHub.Infra.Messages
{
    public class MessageController
    {

        public MessageController()
        {
            ListMessage = new List<MessageType>();
        }

        #region Properties

        protected List<MessageType> ListMessage { get; set; }

        #endregion

        #region Public Methods

        public void AddMessage(MessageType value)
        {
            ListMessage.Add(value);
        }

        public void Clear()
        {
            ListMessage.Clear();
        }

        public List<MessageType> GetAllMessage() => ListMessage;

        public List<MessageType> GetMessageError() => ListMessage.Where(w => w.TypeMessage.Equals(MessageType.TYPE.ERROR)).ToList();

        public List<MessageType> GetMessageSuccess() => ListMessage.Where(w => w.TypeMessage.Equals(MessageType.TYPE.SUCCESS)).ToList();

        public List<MessageType> GetMessageWaring() => ListMessage.Where(w => w.TypeMessage.Equals(MessageType.TYPE.WARING)).ToList();

        #endregion
    }
}