//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

namespace CrmHub.Infra.Messages.Models
{
    public class MessageType
    {
        public enum TYPE { INFO, SUCCESS, WARING, ERROR }

        #region Constructor

        public MessageType(TYPE type)
        {
            Type = type;
            Message = string.Empty;
        }

        #endregion

        #region Properties

        public TYPE Type { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        #endregion
    }
}