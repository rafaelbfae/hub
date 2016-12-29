namespace CrmHub.Infra.Messages
{
    public class MessageType
    {
        public enum TYPE { SUCCESS, ERROR, WARING, NONE }

        #region Constructor

        public MessageType()
        {
            TypeMessage = TYPE.NONE;
            Message = string.Empty;
        }

        #endregion

        #region Properties

        public TYPE TypeMessage { get; set; }

        public string Message { get; set; }

        #endregion
    }
}