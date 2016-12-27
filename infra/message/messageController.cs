using System.Linq;
using System.Collections.Generic;

namespace CrmHub.Infra.Message
{
    public class MessageController
    {
        #region Attributes

        private List<Message> _listMessage = new List<Message>();

        #endregion

        #region Properties

        protected List<Message> listMessage
        {
            get { return this._listMessage; }
            set { this._listMessage = value; }
        }

        #endregion

        #region Public Methods

        public void AddMessage(Message value) 
        {
            listMessage.Add(value);
        }

        public void Clear()
        {
            listMessage.Clear();
        }

        public List<Message> GetAllMessage() => listMessage;

        public List<Message> GetMessageError() => listMessage.Where(w => w.typeMessage.Equals(Message.TYPE.ERROR)).ToList();

        public List<Message> GetMessageSuccess() => listMessage.Where(w => w.typeMessage.Equals(Message.TYPE.SUCCESS)).ToList();
        
        public List<Message> GetMessageWaring() => listMessage.Where(w => w.typeMessage.Equals(Message.TYPE.WARING)).ToList();
        
        #endregion
    }
}