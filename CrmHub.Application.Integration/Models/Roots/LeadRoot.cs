using CrmHub.Application.Integration.Models.Roots.Base;

namespace CrmHub.Application.Integration.Models.Roots
{
    public class LeadRoot : BaseRoot
    {
        public Lead Lead { get; set; }

        public override string GetId() { return Lead.Id; }
    }
}
