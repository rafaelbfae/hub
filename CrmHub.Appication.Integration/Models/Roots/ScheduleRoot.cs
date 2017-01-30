using CrmHub.Application.Integration.Models.Roots.Base;
using System.Collections.Generic;

namespace CrmHub.Application.Integration.Models.Roots
{
    public class ScheduleRoot : BaseRoot
    {
        public Lead Lead { get; set; }
        public Address Address { get; set; }
        public Schedule Schedule { get; set; }
        public List<Contact> Contacts { get; set; }

        public override string GetId() { return string.Empty; }
    }
}
