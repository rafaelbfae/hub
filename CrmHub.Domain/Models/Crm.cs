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

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string UrlService { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string UrlAccount { get; set; }

        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Environment { get; set; }
    }
}
