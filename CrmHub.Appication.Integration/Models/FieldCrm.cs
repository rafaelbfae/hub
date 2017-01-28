using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Appication.Integration.Models
{
    public class FieldCrm
    {
        public FieldCrm()
        {
            Label = string.Empty;
            Type = string.Empty;
        }

        public bool Customfield { get; set; }
        public int Maxlength { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
    }
}
