using CrmHub.Application.Integration.Models.Base;
using System;

namespace CrmHub.Application.Integration.Models
{
    public class Schedule : BaseEntity
    {
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public string TimeZone { get; set; }
        public string Reference { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
    }
}
