using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmHub.Domain.Models
{
    public class Crm : EntityBase
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }
    }
}
