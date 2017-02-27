using CrmHub.Application.Integration.Models.Roots.Base;

namespace CrmHub.Application.Integration.Models.Roots
{
    public class ContactRoot : BaseRoot
    {
        public Contact Contact { get; set; }

        public override string GetId() { return Contact.Id; }
    }
}
