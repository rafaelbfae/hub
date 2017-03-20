using CrmHub.Application.Models.Exact.Roots.Base;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class LeadExact : BaseExact<LeadExact>
    {
        [Required]
        public Lead Lead { get; set; }

        public override string GetId() { return Lead.Id; }
    }
}
