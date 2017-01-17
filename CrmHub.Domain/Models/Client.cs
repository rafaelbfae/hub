using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmHub.Domain.Models
{
    public class Client : EntityBase
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(14)")]
        public string CNPJ { get; set; }

        public int Code { get; set; }

        [Required]
        public virtual Company Company {get; set;}
    }
}
