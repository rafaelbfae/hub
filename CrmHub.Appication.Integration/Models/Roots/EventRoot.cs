using CrmHub.Application.Integration.Models.Roots.Base;

namespace CrmHub.Application.Integration.Models.Roots
{
    public class EventRoot : BaseRoot
    {
        public Events Schedule { get; set; }

        public override string GetId() { return Schedule.Id; }
    }
}
