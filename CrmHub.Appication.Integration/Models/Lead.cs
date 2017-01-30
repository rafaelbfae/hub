using CrmHub.Application.Integration.Models.Base;
using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models
{
    public class Lead : BaseEntity
    {
        public string Name { get; set; }
        public string Site { get; set; }
        public string Note { get; set; }
        public string Data { get; set; }
        public string Source { get; set; }
        public string Market { get; set; }
        public string Vendor { get; set; }
        public string Product { get; set; }
        public string SubSource { get; set; }
        public string Diagnosis { get; set; }
        public string Link { get; set; }
        public List<string> Phones { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
