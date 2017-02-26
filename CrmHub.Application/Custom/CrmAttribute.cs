using CrmHub.Application.Integration.Enuns;
using System;

namespace CrmHub.Application.Custom
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class CrmAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmName"></param>
        /// <param name="mappings"></param>
        public CrmAttribute(eCrmName crmName, string[] mappings)
        {
            this.Mappings = mappings;
            this.CrmName = crmName;
        }

        /// <summary>
        /// Apply format in value field
        /// </summary>
        /// <param name="crmName"></param>
        /// <param name="mapping"></param>
        /// <param name="format"></param>
        public CrmAttribute(eCrmName crmName, string mapping, string format = null)
        {
            this.Mappings = new string[] { mapping };
            this.CrmName = crmName;
            this.Format = format;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmName"></param>
        /// <param name="entity"></param>
        /// <param name="mappings"></param>
        public CrmAttribute(eCrmName crmName, string entity, string[] mappings)
        {
            this.HasEntity = !string.IsNullOrEmpty(entity);
            this.Entity = entity;
            this.Mappings = mappings;
            this.CrmName = crmName;
        }

        public bool HasEntity { get; }
        public string Entity { get; }
        public string Format { get; }
        public string[] Mappings { get; }
        public eCrmName CrmName { get; }
    }
}
