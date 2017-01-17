using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmHub.Domain.Models
{
    public class Vendor : EntityBase
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }

        [Required]
        [MaxLength(65)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string User { get; set; }

        [MaxLength(256)]
        public string Token { get; set; }

        [Required]
        public virtual Crm CRM { get; set; }

        [Required]
        public virtual Company Company { get; set; }
    }
}
