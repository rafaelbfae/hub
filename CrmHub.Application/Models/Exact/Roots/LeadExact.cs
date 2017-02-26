using CrmHub.Application.Models.Exact.Roots.Base;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class LeadExact : BaseExact<LeadExact>
    {
        public Lead Lead { get; set; }
    }
}
