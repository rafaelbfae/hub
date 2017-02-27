using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Roots.Base
{
    public class BaseRoot : object
    {
        public string EntityName { get; set; }
        public Authentication Authentication { get; set; }
        public List<CustomFields> CustomFields { get; set; }
        public List<MappingFields> MappingFields { get; set; }
        public virtual string GetId() { return string.Empty; }
    }
}
