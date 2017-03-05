using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Address")]
    public class Endereco : Base<Endereco>
    {
        [Required]
        [Crm(eCrmName.ZOHOCRM, "Street")]
        public string Rua { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Zip Code")]
        public string CEP { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Country")]
        public string Pais { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "State")]
        public string Estado { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "City")]
        public string Cidade { get; set; }

        public string Maps { get; set; }

        public string Complemento { get; set; }
    }
}
