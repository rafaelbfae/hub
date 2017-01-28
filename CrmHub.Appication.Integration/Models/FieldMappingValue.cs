using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Models
{
    public class FieldMappingValue
    {
        public FieldMappingValue()
        {
            this.PropertyNameHub = string.Empty;
            this.PropertyNameCrm = string.Empty;
        }

        public string PropertyNameHub { get; set; }

        public string PropertyNameCrm { get; set; }
    }
}
