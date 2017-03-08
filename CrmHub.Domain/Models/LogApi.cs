using CrmHub.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Domain.Models
{
    public class LogApi : EntityBase
    {
        [Required]
        public string Crm { get; set; }

        [Required]
        public string Send { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Entity { get; set; }

        [Required]
        public string Method { get; set; }

        public string Parameters { get; set; }

        public string Response { get; set; }
    }
}
