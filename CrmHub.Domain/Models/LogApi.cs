using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmHub.Domain.Models
{
    public class LogApi : EntityBase
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Crm { get; set; }

        [Required]
        public string Send { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string User { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Entity { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Method { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Empresa { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Parameters { get; set; }

        public string Response { get; set; }
    }
}
