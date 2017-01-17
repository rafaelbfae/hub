using CrmHub.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Domain.Models
{
    public class AttributeEntity : EntityBase
    {

        public AttributeEntity(string entity, string attribute, string label, Company company)
        {
            this.Entity = entity;
            this.Attribute = attribute;
            this.Label = label;
            this.Company = company;
        }

        [Column(TypeName = "varchar(150)")]
        public string Entity { get; private set; }

        [Column(TypeName = "varchar(150)")]
        public string Attribute { get; private set; }

        [Column(TypeName = "varchar(150)")]
        public string Label { get; private set; }
        
        public virtual Company Company { get; private set; }
    }
}
