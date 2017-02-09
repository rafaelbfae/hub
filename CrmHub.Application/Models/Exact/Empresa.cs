using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Company")]
    public class Empresa : Base<Empresa>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Account Name")]
        public string Nome { get; set; }
        
    }
}
