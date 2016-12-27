namespace CrmHub.Infra.Message 
{
    public class Message 
    {
        public enum TYPE { SUCCESS, ERROR, WARING, NONE }

        #region Attributes

        private TYPE _typeMessage;

        private string _message;

        #endregion

        #region Constructor

        public Message()
        {
            this._typeMessage = TYPE.NONE;
            this._message = string.Empty;
        }

        #endregion

        #region Properties

        public TYPE typeMessage
        {
            get { return this._typeMessage; }
            set { this._typeMessage = value; }
        }

        public string message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        #endregion
    }
}