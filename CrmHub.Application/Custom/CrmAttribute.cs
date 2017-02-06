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

        public string[] Mappings { get; }
        public eCrmName CrmName { get; }
    }
}
