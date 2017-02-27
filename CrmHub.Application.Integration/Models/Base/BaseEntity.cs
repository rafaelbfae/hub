

using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Base
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> MappingFields { get; set; }
    }
}
