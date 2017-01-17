using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmHub.Domain.Models
{
    public class Company : EntityBase
    {
        [Required]
        [MaxLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Contact { get; set; }

        [Required]
        [Phone]
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }
    }
}
