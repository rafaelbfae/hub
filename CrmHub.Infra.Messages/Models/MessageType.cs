using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrmHub.Infra.Messages.Models
{
    public class MessageType
    {
        #region Enum

        public enum TYPE { INFO, SUCCESS, WARING, ERROR }

        public enum ENTITY { LEAD, REUNIAO, CONTATO, EMPRESA, NONE }

        #endregion

        #region Constructor

        public MessageType(TYPE type)
        {
            Type = type;
            Message = string.Empty;
            Entity = ENTITY.NONE;
        }

        public MessageType(TYPE type, ENTITY entity)
        {
            Type = type;
            Message = string.Empty;
            Entity = entity;
        }

        #endregion

        #region Properties

        [JsonConverter(typeof(StringEnumConverter))]
        public TYPE Type { get; set; }

        public string Message { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ENTITY Entity { get; set; } 

        public object Data { get; set; }

        #endregion
    }
}