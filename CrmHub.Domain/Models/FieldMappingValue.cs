using CrmHub.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Domain.Models
{
    public class FieldMappingValue : EntityBase
    {
        public FieldMappingValue()
        {
            this.NameHub = string.Empty;
            this.NameCrm = string.Empty;
            this.LabelHub = string.Empty;
            this.LabelCrm = string.Empty;
        }

        [Column(TypeName = "varchar(150)")]
        public string NameHub { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string LabelHub { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string EntityHub { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string NameCrm { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string LabelCrm { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string EntityCrm { get; set; }
        
        public virtual Client Client { get; set; }

        public virtual Company Company { get; set; }

    }
}