using CrmHub.Application.Integration.Enuns;
using System;

namespace CrmHub.Application.Custom
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class CrmAttribute : Attribute
    {
        public CrmAttribute(eCrmName crmName, params string[] mappings)
        {
            this.Mappings = mappings;
            this.CrmName = crmName;
        }

        public CrmAttribute(eCrmName crmName, string entity, bool hasEntity, params string[] mappings)
        {
            this.HasEntity = hasEntity;
            this.Entity = entity;
            this.Mappings = mappings;
            this.CrmName = crmName;
        }

        public bool HasEntity { get; }
        public string Entity { get; }
        public string[] Mappings { get; }
        public eCrmName CrmName { get; }
    }
}
